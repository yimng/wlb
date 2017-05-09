// Decompiled with JetBrains decompiler
// Type: XenAPI.HTTP
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

namespace XenAPI
{
  public class HTTP
  {
    public const int BUFFER_SIZE = 32768;
    public const int MAX_REDIRECTS = 10;
    public const int DEFAULT_HTTPS_PORT = 443;

    private static void WriteLine(string txt, Stream stream)
    {
      byte[] bytes = Encoding.ASCII.GetBytes(string.Format("{0}\r\n", (object) txt));
      stream.Write(bytes, 0, bytes.Length);
    }

    private static void WriteLine(Stream stream)
    {
      HTTP.WriteLine("", stream);
    }

    private static string ReadLine(Stream stream)
    {
      StringBuilder stringBuilder = new StringBuilder();
      char ch;
      do
      {
        int num = stream.ReadByte();
        if (num == -1)
          throw new EndOfStreamException();
        ch = Convert.ToChar(num);
        stringBuilder.Append(ch);
      }
      while ((int) ch != 10);
      return stringBuilder.ToString();
    }

    private static bool ReadHttpHeaders(ref Stream stream, IWebProxy proxy, bool nodelay, int timeout_ms)
    {
      string line = HTTP.ReadLine(stream);
      switch (HTTP.getResultCode(line))
      {
        case 200:
          do
            ;
          while (!Regex.Match(HTTP.ReadLine(stream), "^\\s*$").Success);
          return false;
        case 302:
          string str1 = "";
          string str2;
          do
          {
            str2 = HTTP.ReadLine(stream);
            if (str2.StartsWith("Location: "))
              str1 = str2.Substring(10);
          }
          while (!str2.Equals("\r\n") && !str2.Equals(""));
          Uri uri = new Uri(str1.Trim());
          stream.Close();
          stream = HTTP.ConnectStream(uri, proxy, nodelay, timeout_ms);
          return true;
        default:
          stream.Close();
          throw new HTTP.BadServerResponseException(string.Format("Received error code {0} from the server", (object) line));
      }
    }

    public static int getResultCode(string line)
    {
      string[] strArray = line.Split(' ');
      if (strArray.Length >= 2)
        return int.Parse(strArray[1]);
      return 0;
    }

    public static bool UseSSL(Uri uri)
    {
      if (!(uri.Scheme == "https"))
        return uri.Port == 443;
      return true;
    }

    private static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
      return true;
    }

    public static long CopyStream(Stream inStream, Stream outStream, HTTP.DataCopiedDelegate progressDelegate, HTTP.FuncBool cancellingDelegate)
    {
      long bytes = 0L;
      byte[] buffer = new byte[32768];
      DateTime now = DateTime.Now;
      while (cancellingDelegate == null || !cancellingDelegate())
      {
        int count = inStream.Read(buffer, 0, buffer.Length);
        if (count != 0)
        {
          outStream.Write(buffer, 0, count);
          bytes += (long) count;
          if (progressDelegate != null && DateTime.Now - now > TimeSpan.FromMilliseconds(500.0))
          {
            progressDelegate(bytes);
            now = DateTime.Now;
          }
        }
        else
          break;
      }
      if (cancellingDelegate != null && cancellingDelegate())
        throw new HTTP.CancelledException();
      if (progressDelegate != null)
        progressDelegate(bytes);
      return bytes;
    }

    public static Uri BuildUri(string hostname, string path, params object[] args)
    {
      List<object> list = new List<object>();
      foreach (object obj in args)
      {
        if (obj is IEnumerable<object>)
          list.AddRange((IEnumerable<object>) obj);
        else
          list.Add(obj);
      }
      UriBuilder uriBuilder = new UriBuilder();
      uriBuilder.Scheme = "https";
      uriBuilder.Port = 443;
      uriBuilder.Host = hostname;
      uriBuilder.Path = path;
      StringBuilder stringBuilder = new StringBuilder();
      int index = 0;
      while (index < list.Count - 1)
      {
        if (list[index + 1] != null)
        {
          string str;
          if (list[index + 1] is bool)
          {
            if ((bool) list[index + 1])
              str = (string) list[index] + (object) "=true";
            else
              goto label_15;
          }
          else
            str = (string) list[index] + (object) "=" + Uri.EscapeDataString(list[index + 1].ToString());
          if (stringBuilder.Length != 0)
            stringBuilder.Append('&');
          stringBuilder.Append(str);
        }
label_15:
        index += 2;
      }
      uriBuilder.Query = stringBuilder.ToString();
      return uriBuilder.Uri;
    }

    private static NetworkStream ConnectSocket(Uri uri, bool nodelay, int timeout_ms)
    {
      Socket socket = new Socket(uri.HostNameType == UriHostNameType.IPv6 ? AddressFamily.InterNetworkV6 : AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
      socket.NoDelay = nodelay;
      socket.ReceiveTimeout = timeout_ms;
      socket.SendTimeout = timeout_ms;
      socket.Connect(uri.Host, uri.Port);
      return new NetworkStream(socket, true);
    }

    public static Stream ConnectStream(Uri uri, IWebProxy proxy, bool nodelay, int timeout_ms)
    {
      IMockWebProxy mockWebProxy = proxy != null ? proxy as IMockWebProxy : (IMockWebProxy) null;
      if (mockWebProxy != null)
        return mockWebProxy.GetStream(uri);
      bool flag = proxy != null && !proxy.IsBypassed(uri);
      Stream stream = !flag ? (Stream) HTTP.ConnectSocket(uri, nodelay, timeout_ms) : (Stream) HTTP.ConnectSocket(proxy.GetProxy(uri), nodelay, timeout_ms);
      try
      {
        if (flag)
        {
          HTTP.WriteLine(string.Format("CONNECT {0}:{1} HTTP/1.0", (object) uri.Host, (object) uri.Port), stream);
          HTTP.WriteLine(stream);
          HTTP.ReadHttpHeaders(ref stream, proxy, nodelay, timeout_ms);
        }
        if (HTTP.UseSSL(uri))
        {
          SslStream sslStream = new SslStream(stream, false, new RemoteCertificateValidationCallback(HTTP.ValidateServerCertificate), (LocalCertificateSelectionCallback) null);
          sslStream.AuthenticateAsClient("");
          stream = (Stream) sslStream;
        }
        return stream;
      }
      catch
      {
        stream.Close();
        throw;
      }
    }

    private static Stream DO_HTTP(Uri uri, IWebProxy proxy, bool nodelay, int timeout_ms, params string[] headers)
    {
      Stream stream = HTTP.ConnectStream(uri, proxy, nodelay, timeout_ms);
      int redirect = 0;
      while (redirect <= 10)
      {
        ++redirect;
        foreach (string txt in headers)
          HTTP.WriteLine(txt, stream);
        HTTP.WriteLine(stream);
        stream.Flush();
        if (!HTTP.ReadHttpHeaders(ref stream, proxy, nodelay, timeout_ms))
          return stream;
      }
      throw new HTTP.TooManyRedirectsException(redirect, uri);
    }

    public static Stream CONNECT(Uri uri, IWebProxy proxy, string session, int timeout_ms)
    {
      return HTTP.DO_HTTP(uri, proxy, 1 != 0, timeout_ms, string.Format("CONNECT {0} HTTP/1.0", (object) uri.PathAndQuery), string.Format("Host: {0}", (object) uri.Host), string.Format("Cookie: session_id={0}", (object) session));
    }

    public static Stream PUT(Uri uri, IWebProxy proxy, long ContentLength, int timeout_ms)
    {
      return HTTP.DO_HTTP(uri, proxy, 0 != 0, timeout_ms, string.Format("PUT {0} HTTP/1.0", (object) uri.PathAndQuery), string.Format("Content-Length: {0}", (object) ContentLength));
    }

    public static Stream GET(Uri uri, IWebProxy proxy, int timeout_ms)
    {
      return HTTP.DO_HTTP(uri, proxy, 0 != 0, timeout_ms, string.Format("GET {0} HTTP/1.0", (object) uri.PathAndQuery));
    }

    public static void Put(HTTP.UpdateProgressDelegate progressDelegate, HTTP.FuncBool cancellingDelegate, Uri uri, IWebProxy proxy, string path, int timeout_ms)
    {
      using (Stream inStream = (Stream) new FileStream(path, FileMode.Open, FileAccess.Read))
      {
        using (Stream outStream = HTTP.PUT(uri, proxy, inStream.Length, timeout_ms))
        {
          long len = inStream.Length;
          HTTP.DataCopiedDelegate progressDelegate1 = (HTTP.DataCopiedDelegate) (bytes =>
          {
            if (progressDelegate == null || len <= 0L)
              return;
            progressDelegate((int) (bytes * 100L / len));
          });
          HTTP.CopyStream(inStream, outStream, progressDelegate1, cancellingDelegate);
        }
      }
    }

    public static void Get(HTTP.DataCopiedDelegate dataCopiedDelegate, HTTP.FuncBool cancellingDelegate, Uri uri, IWebProxy proxy, string path, int timeout_ms)
    {
      string tempFileName = Path.GetTempFileName();
      try
      {
        using (Stream outStream = (Stream) new FileStream(tempFileName, FileMode.Create, FileAccess.Write, FileShare.None))
        {
          using (Stream inStream = HTTP.GET(uri, proxy, timeout_ms))
          {
            HTTP.CopyStream(inStream, outStream, dataCopiedDelegate, cancellingDelegate);
            outStream.Flush();
          }
        }
        System.IO.File.Delete(path);
        System.IO.File.Move(tempFileName, path);
      }
      finally
      {
        System.IO.File.Delete(tempFileName);
      }
    }

    public class TooManyRedirectsException : Exception
    {
      private readonly int redirect;
      private readonly Uri uri;

      public TooManyRedirectsException(int redirect, Uri uri)
      {
        this.redirect = redirect;
        this.uri = uri;
      }
    }

    public class BadServerResponseException : Exception
    {
      public BadServerResponseException(string msg)
        : base(msg)
      {
      }
    }

    public class CancelledException : Exception
    {
    }

    public delegate bool FuncBool();

    public delegate void UpdateProgressDelegate(int percent);

    public delegate void DataCopiedDelegate(long bytes);
  }
}
