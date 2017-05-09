using Halsign.DWM.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;

namespace Halsign.DWM.WlbConfig
{
  internal class Program
  {
    private static int ERROR = 1;
    private static string _wlbUsername;
    private static string _wlbPassword;
    private static string _dbUsername;
    private static SecureString _dbPassword;
    private static int SUCCESS = 0;

    private static int Main(string[] args)
    {
      Environment.SetEnvironmentVariable("ProcName", "WlbConfig", EnvironmentVariableTarget.Process);
      Console.WriteLine(Messages.BANNER_TOP);
      bool flag1 = false;
      if (args.Length == 0)
      {
        Logger.ConsoleLogEnabled = false;
        if (!Program.AreDBCredentialsValid())
        {
          Console.WriteLine(Messages.ERROR_CONNECTING_DB);
          return Program.ERROR;
        }
        string dbUsername;
        SecureString dbPassword;
        Program.ReadDBCredentials(out dbUsername, out dbPassword);
        Console.WriteLine(string.Format(Messages.BANNER_SERVER, (object) "PostgreSQL"));
        string str1;
        string reason;
        while (true)
        {
          Console.Write(Messages.CHANGING_PASSWORD, (object) dbUsername);
          str1 = Program.ReadPassword();
          if (str1.Length > 0)
          {
            if (!Validation.IsPasswordValid(str1, out reason))
            {
              Console.WriteLine(reason + Messages.TRY_AGAIN);
            }
            else
            {
              Console.Write(Messages.CONFIRM);
              string str2 = Program.ReadPassword();
              if (str1 != str2)
                Console.WriteLine(Messages.ERROR_PASS_MISMATCH + Messages.TRY_AGAIN);
              else
                break;
            }
          }
          else
            goto label_16;
        }
        try
        {
          using (DBAccess dbAccess = new DBAccess())
          {
            string[] strArray = new string[5];
            int index1 = 0;
            string str2 = "ALTER USER ";
            strArray[index1] = str2;
            int index2 = 1;
            string str3 = dbUsername;
            strArray[index2] = str3;
            int index3 = 2;
            string str4 = " WITH PASSWORD '";
            strArray[index3] = str4;
            int index4 = 3;
            string str5 = str1;
            strArray[index4] = str5;
            int index5 = 4;
            string str6 = "';";
            strArray[index5] = str6;
            string sqlStatement = string.Concat(strArray);
            dbAccess.ExecuteSql(sqlStatement);
          }
          string baseString = Crypto.Encrypt(str1);
          Program.WriteDBCredentials(dbUsername, Program.StringToSecureString(baseString));
          string fmt = "PostgreSQL password changed for user {0}.";
          object[] objArray = new object[1];
          int index = 0;
          string str7 = dbUsername;
          objArray[index] = (object) str7;
          Logger.Trace(fmt, objArray);
        }
        catch (Exception ex)
        {
          Logger.LogException(ex);
          Console.Error.WriteLine(Messages.ERROR_CONNECTING_DB);
          return Program.ERROR;
        }
label_16:
        Console.WriteLine();
        Console.WriteLine(string.Format(Messages.BANNER_SERVER, (object) "Workload Balancing"));
        string wlbUsername;
        string wlbPassword;
        Program.ReadWLBCredentials(out wlbUsername, out wlbPassword);
        bool flag2 = false;
        bool flag3 = false;
        string str8;
        while (true)
        {
          Console.Write(Messages.CHANGING_USERNAME, (object) wlbUsername);
          str8 = Console.ReadLine().ToLower();
          if (!string.IsNullOrEmpty(str8))
          {
            if (!Validation.IsUserNameValid(str8, out reason))
              Console.Error.WriteLine(reason + Messages.TRY_AGAIN);
            else
              goto label_21;
          }
          else
            break;
        }
        str8 = wlbUsername;
        goto label_22;
label_21:
        flag2 = true;
label_22:
        string str9;
        while (true)
        {
          while (flag2)
          {
            Console.Write("Enter new password for {0}: ", (object) str8);
            str9 = Program.ReadPassword();
            if (string.IsNullOrEmpty(str9))
              Console.Error.WriteLine("Password cannot be empty.");
            else
              goto label_27;
          }
          Console.Write(Messages.CHANGING_PASSWORD, (object) str8);
          str9 = Program.ReadPassword();
          if (string.IsNullOrEmpty(str9))
            break;
label_27:
          if (!Validation.IsPasswordValid(str9, out reason))
          {
            Console.Error.WriteLine(reason + Messages.TRY_AGAIN);
          }
          else
          {
            Console.Write(Messages.CONFIRM);
            string str2 = Program.ReadPassword();
            if (str9 != str2)
              Console.Error.WriteLine(Messages.ERROR_PASS_MISMATCH + Messages.TRY_AGAIN);
            else
              goto label_31;
          }
        }
        str9 = wlbPassword;
        goto label_32;
label_31:
        flag3 = true;
label_32:
        if (flag2 || flag3)
        {
          Program.WriteWLBCredentials(str8, Crypto.HashPassword(str9, str8));
          string fmt1 = "WLB username now is {0}. Username was changed? {1}";
          object[] objArray1 = new object[2];
          int index1 = 0;
          string str2 = str8;
          objArray1[index1] = (object) str2;
          int index2 = 1;
          string str3 = !(dbUsername == str8) ? "Yes" : "No";
          objArray1[index2] = (object) str3;
          Logger.Trace(fmt1, objArray1);
          string fmt2 = "WLB password was changed? {0}";
          object[] objArray2 = new object[1];
          int index3 = 0;
          string str4 = !(wlbPassword == str9) ? "Yes" : "No";
          objArray2[index3] = (object) str4;
          Logger.Trace(fmt2, objArray2);
          Console.WriteLine(Messages.CREDS_SAVED);
        }
        return Program.SUCCESS;
      }
      if (args.Length == 4)
      {
        for (int index1 = 0; index1 < args.Length; ++index1)
        {
          string str = args[index1];
          string[] strArray = str.Split('=');
          if (strArray.Length != 2)
          {
            flag1 = true;
            break;
          }
          string key = strArray[0].ToLower();
          if (key != null)
          {
            Dictionary<string, int> d = new Dictionary<string, int>(8)
              {
                {
                  "wlbusername",
                  0
                },
                {
                  "wu",
                  0
                },
                {
                  "wlbpassword",
                  1
                },
                {
                  "wp",
                  1
                },
                {
                  "dbusername",
                  2
                },
                {
                  "du",
                  2
                },
                {
                  "dbpassword",
                  3
                },
                {
                  "dp",
                  3
                }
              };
            int num2;
            if (d.TryGetValue(key, out num2))
            {
              switch (num2)
              {
                case 0:
                  Program._wlbUsername = strArray[1].ToLower();
                  continue;
                case 1:
                  Program._wlbPassword = Crypto.HashPassword(strArray[1], Program._wlbUsername);
                  continue;
                case 2:
                  Program._dbUsername = strArray[1];
                  continue;
                case 3:
                  Program._dbPassword = Program.StringToSecureString(Crypto.Encrypt(strArray[1]));
                  continue;
                default:
                  continue;
              }
            }
          }
        }
        if (Program._wlbUsername == null && Program._dbUsername == null || Program._wlbUsername != null && Program._wlbPassword == null || Program._dbUsername != null && Program._dbPassword == null)
          flag1 = true;
        if (!flag1)
        {
          string dbUsername;
          SecureString dbPassword;
          Program.ReadDBCredentials(out dbUsername, out dbPassword);
          Program.WriteDBCredentials(Program._dbUsername, Program._dbPassword);
          if (!Program.AreDBCredentialsValid())
          {
            if (dbUsername != null && dbPassword != null)
              Program.WriteDBCredentials(dbUsername, dbPassword);
            Logger.Trace("The supplied database credentials are invalid.");
            throw new Exception("The supplied database credentials are invalid.");
          }
          Program.WriteWLBCredentials(Program._wlbUsername, Program._wlbPassword);
        }
        return Program.SUCCESS;
      }
      if (args.Length == 2)
      {
          for (int index1 = 0; index1 < args.Length; ++index1)
          {
              string str = args[index1];
              string[] strArray = str.Split('=');
              if (strArray.Length != 2)
              {
                  flag1 = true;
                  break;
              }
              string key = strArray[0].ToLower();
              if (key != null)
              {
                  Dictionary<string, int> d = new Dictionary<string, int>(4)
                          {
                            {
                              "wlbusername",
                              0
                            },
                            {
                              "wu",
                              0
                            },
                            {
                              "wlbpassword",
                              1
                            },
                            {
                              "wp",
                              1
                            }
                          };
                  int num2;
                  if (d.TryGetValue(key, out num2))
                  {
                      switch (num2)
                      {
                          case 0:
                              Program._wlbUsername = strArray[1].ToLower();
                              continue;
                          case 1:
                              Program._wlbPassword = Crypto.HashPassword(strArray[1], Program._wlbUsername);
                              continue;
                      }
                  }
              }
          }
          if (Program._wlbUsername == null || Program._wlbPassword == null)
              flag1 = true;
          if (!flag1)
          {
              Program.WriteWLBCredentials(Program._wlbUsername, Program._wlbPassword);
          }
          return Program.SUCCESS;
      }
      Program.ShowHelp();
      return Program.ERROR;
    }

    private static string ReadPassword()
    {
      string str = string.Empty;
      ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
      while (consoleKeyInfo.Key != ConsoleKey.Enter)
      {
        if (consoleKeyInfo.Key != ConsoleKey.Backspace)
        {
          str += consoleKeyInfo.KeyChar;
          consoleKeyInfo = Console.ReadKey(true);
        }
        else if (consoleKeyInfo.Key == ConsoleKey.Backspace)
        {
          if (!string.IsNullOrEmpty(str))
            str = str.Substring(0, str.Length - 1);
          consoleKeyInfo = Console.ReadKey(true);
        }
      }
      Console.WriteLine();
      return str;
    }

    private static bool AreDBCredentialsValid()
    {
      bool flag = true;
      try
      {
        Configuration.ReloadConfiguration();
      }
      catch
      {
        flag = false;
      }
      return flag;
    }

    private static void ReadWLBCredentials(out string wlbUsername, out string wlbPassword)
    {
      wlbUsername = Configuration.GetValueAsString(ConfigItems.WlbUsername);
      wlbPassword = Configuration.GetValueAsString(ConfigItems.WlbPassword);
    }

    private static void WriteWLBCredentials(string wlbUsername, string wlbPassword)
    {
      Configuration.SetValue(ConfigItems.WlbUsername, wlbUsername);
      Configuration.SetValue(ConfigItems.WlbPassword, wlbPassword);
    }

    private static void ReadDBCredentials(out string dbUsername, out SecureString dbPassword)
    {
      dbUsername = Configuration.GetValueAsString(ConfigItems.DBUsername);
      dbPassword = Program.StringToSecureString(Configuration.GetValueAsString(ConfigItems.DBPassword));
    }

    private static void WriteDBCredentials(string dbUsername, SecureString dbPassword)
    {
      Configuration.SetValue(ConfigItems.DBUsername, dbUsername);
      Configuration.SetValue(ConfigItems.DBPassword, Marshal.PtrToStringBSTR(Marshal.SecureStringToBSTR(dbPassword)));
    }

    private static SecureString StringToSecureString(string baseString)
    {
      SecureString secureString = new SecureString();
      foreach (char c in baseString.ToCharArray())
        secureString.AppendChar(c);
      secureString.MakeReadOnly();
      return secureString;
    }

    private static void ShowHelp()
    {
      Console.WriteLine("Usage:\n\n\tWlbConfig.exe wu=[WLB Username] wp=[WLB Password] du=[Database Username] dp=[Database Password]\n\n\twu (required): username used to connect to the WLB web service\n\twp (required): password used to connect to the WLB web service\n\tdu (required): username used to connect to the PostgreSQL database\n\tdp (required): password used to connect to the PostgreSQL database\n\n\n");
    }
  }
}
