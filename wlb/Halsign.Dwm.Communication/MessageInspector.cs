using Halsign.DWM.Framework;
using System;
using System.IO;
using System.Security;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Xml;
namespace Halsign.DWM.Communication
{
	public class MessageInspector : IEndpointBehavior, IDispatchMessageInspector
	{
		public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
		{
			if (!endpointDispatcher.DispatchRuntime.MessageInspectors.Contains(this))
			{
				endpointDispatcher.DispatchRuntime.MessageInspectors.Add(this);
			}
		}
		public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
		{
			this.AuthenticateIncomingMessage();
			request = this.FixIncomingLineBreaks(request);
			return null;
		}
		public void BeforeSendReply(ref Message reply, object correlationState)
		{
			reply = this.RemoveDefaultResponseBodies(reply);
		}
		private Message RemoveDefaultResponseBodies(Message outgoingMessage)
		{
			MessageBuffer messageBuffer = outgoingMessage.CreateBufferedCopy(2147483647);
			string text = messageBuffer.CreateMessage().ToString();
			StringBuilder stringBuilder = new StringBuilder(text);
			messageBuffer.Close();
			if (text.Contains("<ResultCode>0</ResultCode>") && (text.Contains("<RemoveXenServerResponse") || text.Contains("<SetXenPoolConfigurationResponse") || (text.Contains("<GetOptimizationRecommendationsResponse") && text.Contains("<Recommendations i:nil=\"true\" />"))))
			{
				stringBuilder.Replace("<ErrorMessage i:nil=\"true\" />", string.Empty);
				stringBuilder.Replace("<ResultCode>0</ResultCode>", string.Empty);
				stringBuilder.Replace("<Id>0</Id>", string.Empty);
				stringBuilder.Replace("<OptimizationId>0</OptimizationId>", string.Empty);
				stringBuilder.Replace("<Recommendations i:nil=\"true\" />", string.Empty);
				stringBuilder.Replace("<Severity>None</Severity>", string.Empty);
			}
			if (text.Contains("<AddXenServerResponse") && text.Contains("<Id>0</Id>"))
			{
				stringBuilder.Replace("<Id>0</Id>", string.Empty);
			}
			if (text.Contains("HostGetRecommendationsResponse") && !text.Contains("<ResultCode>0</ResultCode>"))
			{
				stringBuilder.Replace("<Recommendations i:nil=\"true\" />", string.Empty);
				stringBuilder.Replace("<CanPlaceAllVMs>false</CanPlaceAllVMs>", string.Empty);
			}
			stringBuilder.Replace("<s:Envelope xmlns:s=\"http://schemas.xmlsoap.org/soap/envelope/\">", string.Empty);
			stringBuilder.Replace("<s:Body>", string.Empty);
			stringBuilder.Replace("</s:Body>", string.Empty);
			stringBuilder.Replace("</s:Envelope>", string.Empty);
			XmlReader body = XmlReader.Create(new StringReader(stringBuilder.ToString()));
			Message message = Message.CreateMessage(outgoingMessage.Version, null, body);
			message.Headers.CopyHeadersFrom(outgoingMessage);
			message.Properties.CopyProperties(outgoingMessage.Properties);
			if (Configuration.GetValueAsBool(ConfigItems.SoapDataTrace))
			{
				XmlReader body2 = XmlReader.Create(new StringReader(stringBuilder.ToString()));
				Message message2 = Message.CreateMessage(outgoingMessage.Version, null, body2);
				message2.Headers.CopyHeadersFrom(outgoingMessage);
				message2.Properties.CopyProperties(outgoingMessage.Properties);
				Logger.Trace("Response body:\n{0}\n", new object[]
				{
					message2.ToString()
				});
			}
			return message;
		}
		private Message FixIncomingLineBreaks(Message incomingMessage)
		{
			MessageBuffer messageBuffer = incomingMessage.CreateBufferedCopy(2147483647);
			StringBuilder stringBuilder = new StringBuilder(messageBuffer.CreateMessage().ToString());
			messageBuffer.Close();
			if (Configuration.GetValueAsBool(ConfigItems.SoapDataTrace))
			{
				Logger.Trace("Request body:\n{0}\n", new object[]
				{
					stringBuilder.ToString()
				});
			}
			stringBuilder.Replace("\n", string.Empty);
			stringBuilder.Replace("\r", string.Empty);
			XmlReader envelopeReader = XmlReader.Create(new StringReader(stringBuilder.ToString()));
			return Message.CreateMessage(envelopeReader, 2147483647, incomingMessage.Version);
		}
		private void AuthenticateIncomingMessage()
		{
			string text = string.Empty;
			string text2 = string.Empty;
			string password = string.Empty;
			string username = string.Empty;
			object obj;
			if (!OperationContext.Current.IncomingMessageProperties.TryGetValue(HttpRequestMessageProperty.Name, out obj))
			{
				Logger.Trace("AfterReceiveRequest: Unable to fetch HttpRequestMessageProperty");
				throw new Exception();
			}
			object obj2;
			if (OperationContext.Current.IncomingMessageProperties.TryGetValue("WLBAuthenticated", out obj2))
			{
				Logger.Trace("AfterReceiveRequest: Authentication element already present in incoming message");
				throw new SecurityException("Authentication element already present in incoming Message!");
			}
			HttpRequestMessageProperty httpRequestMessageProperty = (HttpRequestMessageProperty)obj;
			text = httpRequestMessageProperty.Headers["Authorization"];
			text = text.Remove(0, 6);
			byte[] bytes = Convert.FromBase64String(text);
			text2 = Encoding.UTF8.GetString(bytes);
			string[] array = text2.Split(new char[]
			{
				':'
			});
			username = array[0];
			password = array[1];
			Authentication.Validate(username, password);
			OperationContext.Current.IncomingMessageProperties.Add("WLBAuthenticated", true);
		}
		public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
		{
		}
		public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
		{
		}
		public void Validate(ServiceEndpoint endpoint)
		{
		}
	}
}
