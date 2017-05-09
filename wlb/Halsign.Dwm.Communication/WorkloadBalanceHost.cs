using Halsign.DWM.Framework;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.Text;
using System.Threading;
namespace Halsign.DWM.Communication
{
	public class WorkloadBalanceHost : IDisposable
	{
		public const int DefaultPort = 8009;
		private const int StopEvent = 0;
		private bool _isRunning;
		private Thread _listenerThread;
		private WaitHandle[] _waitHandles = new WaitHandle[1];
		private AutoResetEvent _startEvent;
		private int _uriPort = 8009;
		public WorkloadBalanceHost()
		{
		}
		public WorkloadBalanceHost(int port)
		{
			this._uriPort = port;
		}
		private BasicHttpBinding BuildBasicHttpBinding(BasicHttpSecurityMode currentMode)
		{
			return new BasicHttpBinding
			{
				Name = "basic_binding_configuration",
				HostNameComparisonMode = HostNameComparisonMode.StrongWildcard,
				ReceiveTimeout = new TimeSpan(0, 10, 0),
				SendTimeout = new TimeSpan(0, 10, 0),
				OpenTimeout = new TimeSpan(0, 10, 0),
				CloseTimeout = new TimeSpan(0, 10, 0),
				MaxReceivedMessageSize = 262144L,
				MaxBufferSize = 262144,
				TransferMode = TransferMode.Buffered,
				MessageEncoding = WSMessageEncoding.Text,
				TextEncoding = Encoding.UTF8,
				BypassProxyOnLocal = false,
				UseDefaultWebProxy = true,
				Security = 
				{
					Mode = currentMode,
					Transport = 
					{
						ClientCredentialType = HttpClientCredentialType.None,
						ProxyCredentialType = HttpProxyCredentialType.None,
						Realm = string.Empty
					},
					Message = 
					{
						ClientCredentialType = BasicHttpMessageCredentialType.Certificate,
						AlgorithmSuite = SecurityAlgorithmSuite.Default
					}
				}
			};
		}
		public void Start()
		{
			this.Start(false);
		}
		public void Start(bool bWait)
		{
			if (!this._isRunning)
			{
				if (bWait)
				{
					this._startEvent = new AutoResetEvent(false);
				}
				else
				{
					this._startEvent = null;
				}
				this._waitHandles[0] = new AutoResetEvent(false);
				this._listenerThread = new Thread(new ThreadStart(this.ThreadProc));
				this._listenerThread.Name = "WorkloadBalanceHostListenerThread";
				this._listenerThread.IsBackground = true;
				this._listenerThread.Start();
				if (bWait)
				{
					this._startEvent.WaitOne(10000, false);
					this._startEvent.Close();
					this._startEvent = null;
				}
				this._isRunning = true;
			}
		}
		public void Stop()
		{
			if (this._isRunning)
			{
				((AutoResetEvent)this._waitHandles[0]).Set();
				this._isRunning = false;
			}
		}
		protected void ThreadProc()
		{
			ServiceHost serviceHost = null;
			bool valueAsBool = Configuration.GetValueAsBool(ConfigItems.WcfServiceUseSSL);
			int valueAsInt = Configuration.GetValueAsInt(ConfigItems.WcfServicePort);
			string valueAsString = Configuration.GetValueAsString(ConfigItems.WcfServiceUri);
			BasicHttpSecurityMode currentMode = (!valueAsBool) ? BasicHttpSecurityMode.None : BasicHttpSecurityMode.Transport;
			//string address = string.Format("{0}://{1}:{2}/Citrix.Dwm.WorkloadBalance/Service", (!valueAsBool) ? "http" : "https", valueAsString, valueAsInt);
            string address = string.Format("{0}://{1}:{2}/Halsign.Center.AutoResourceDispatcherServer/Service", (!valueAsBool) ? "http" : "https", valueAsString, valueAsInt);
			try
			{
				serviceHost = new ServiceHost(typeof(ARDServer), new Uri[0]);
				serviceHost.AddServiceEndpoint(typeof(IARDServer), this.BuildBasicHttpBinding(currentMode), address).Behaviors.Add(new MessageInspector());
				ServiceMetadataBehavior serviceMetadataBehavior = new ServiceMetadataBehavior();
				serviceMetadataBehavior.HttpGetEnabled = true;
				serviceHost.Description.Behaviors.Add(serviceMetadataBehavior);
				serviceHost.Open();
				if (this._startEvent != null)
				{
					this._startEvent.Set();
				}
				for (int i = 0; i < serviceHost.Description.Endpoints.Count; i++)
				{
					Logger.Trace("WorkloadBalanceHost:  Listening on {0}", new object[]
					{
						serviceHost.Description.Endpoints[i].ListenUri.AbsoluteUri
					});
				}
			}
			catch (CommunicationException ex)
			{
				Logger.LogException(ex);
				serviceHost.Abort();
				throw;
			}
			catch (InvalidOperationException ex2)
			{
				Logger.LogException(ex2);
				serviceHost.Abort();
				throw;
			}
			try
			{
				ARDServer.LoadCache();
			}
			catch (Exception ex3)
			{
				Logger.LogException(ex3);
			}
			int num = WaitHandle.WaitAny(this._waitHandles);
			serviceHost.Close();
			ARDServer.UnloadCache();
		}
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}
		private void Dispose(bool isDisposing)
		{
			if (isDisposing)
			{
				this.Stop();
				this._waitHandles[0].Close();
			}
		}
	}
}
