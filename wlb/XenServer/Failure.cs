// Decompiled with JetBrains decompiler
// Type: XenAPI.Failure
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using System;
using System.Collections.Generic;
using System.Resources;
using System.Text.RegularExpressions;
using System.Xml;

namespace XenAPI
{
  public class Failure : Exception
  {
    private static ResourceManager errorDescriptions = FriendlyErrorNames.ResourceManager;
    public const string INTERNAL_ERROR = "INTERNAL_ERROR";
    public const string MESSAGE_PARAMETER_COUNT_MISMATCH = "MESSAGE_PARAMETER_COUNT_MISMATCH";
    private readonly List<string> errorDescription;
    private string errorText;
    private string shortError;

    public List<string> ErrorDescription
    {
      get
      {
        return this.errorDescription;
      }
    }

    public string ShortMessage
    {
      get
      {
        return this.shortError;
      }
    }

    public override string Message
    {
      get
      {
        return this.errorText;
      }
    }

    public Failure(params string[] err)
      : this(new List<string>((IEnumerable<string>) err))
    {
    }

    public Failure(List<string> ErrorDescription)
    {
      this.errorDescription = ErrorDescription;
      this.Setup();
    }

    public void Setup()
    {
      if (this.ErrorDescription.Count <= 0)
        return;
      try
      {
        string format;
        try
        {
          format = Failure.errorDescriptions.GetString(this.ErrorDescription[0]);
        }
        catch
        {
          format = (string) null;
        }
        if (format == null)
        {
          List<string> list = new List<string>();
          foreach (string str in this.ErrorDescription)
          {
            if (str.Trim().Length > 0)
              list.Add(str.Trim());
          }
          this.errorText = string.Join(" - ", list.ToArray());
        }
        else
        {
          string[] strArray = new string[this.ErrorDescription.Count - 1];
          for (int index = 1; index < this.ErrorDescription.Count; ++index)
            strArray[index - 1] = this.ErrorDescription[index];
          this.errorText = string.Format(format, (object[]) strArray);
        }
      }
      catch (Exception ex)
      {
        this.errorText = this.ErrorDescription[0];
      }
      try
      {
        this.shortError = Failure.errorDescriptions.GetString(this.ErrorDescription[0] + "-SHORT");
      }
      catch (Exception ex)
      {
        this.shortError = this.errorText;
      }
      this.TryParseCslg();
    }

    private bool TryParseCslg()
    {
      if (this.ErrorDescription.Count > 2 && this.ErrorDescription[2] != null && (this.ErrorDescription[0] != null && this.ErrorDescription[0].StartsWith("SR_BACKEND_FAILURE")))
      {
        Match match = Regex.Match(this.ErrorDescription[2], "<StorageLinkServiceError>.*</StorageLinkServiceError>", RegexOptions.Singleline);
        if (match.Success)
        {
          XmlDocument xmlDocument = new XmlDocument();
          try
          {
            xmlDocument.LoadXml(match.Value);
          }
          catch (XmlException ex)
          {
            return false;
          }
          XmlNodeList xmlNodeList = xmlDocument.SelectNodes("/StorageLinkServiceError/Fault");
          if (xmlNodeList != null && xmlNodeList.Count > 0 && !string.IsNullOrEmpty(xmlNodeList[0].InnerText))
          {
            this.errorText = !string.IsNullOrEmpty(this.errorText) ? string.Format("{0} ({1})", (object) this.errorText, (object) xmlNodeList[0].InnerText) : xmlNodeList[0].InnerText;
            return true;
          }
        }
      }
      return false;
    }

    public override string ToString()
    {
      return this.Message;
    }
  }
}
