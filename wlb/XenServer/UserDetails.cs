// Decompiled with JetBrains decompiler
// Type: XenAPI.UserDetails
// Assembly: XenServer, Version=6.1.0.1, Culture=neutral, PublicKeyToken=102be611e60e8ddc
// MVID: 11619AB1-8160-47E5-8915-DFF772C11F71
// Assembly location: C:\Users\ShawnWang\Desktop\wlb\XenServer.dll

using System.Collections.Generic;

namespace XenAPI
{
  public class UserDetails
  {
    private static readonly int MAX_GROUP_LOOKUP = 40;
    private static Dictionary<string, UserDetails> sid_To_UserDetails = new Dictionary<string, UserDetails>();
    private string userSid;
    private string userDisplayName;
    private string userName;
    private string[] groupMembershipNames;
    private string[] groupMembershipSids;

    public static Dictionary<string, UserDetails> Sid_To_UserDetails
    {
      get
      {
        lock (UserDetails.sid_To_UserDetails)
          return UserDetails.sid_To_UserDetails;
      }
    }

    public string UserSid
    {
      get
      {
        return this.userSid;
      }
    }

    public string UserDisplayName
    {
      get
      {
        return this.userDisplayName;
      }
    }

    public string UserName
    {
      get
      {
        return this.userName;
      }
    }

    public string[] GroupMembershipNames
    {
      get
      {
        return this.groupMembershipNames;
      }
    }

    public string[] GroupMembershipSids
    {
      get
      {
        return this.groupMembershipSids;
      }
    }

    private UserDetails(Session session)
    {
      this.userSid = session.UserSid;
      this.userDisplayName = this.GetDisplayName(session);
      this.userName = this.GetName(session);
      this.GetGroupMembership(session);
    }

    public static void UpdateDetails(string SID, Session session)
    {
      lock (UserDetails.sid_To_UserDetails)
      {
        UserDetails.sid_To_UserDetails.Remove(SID);
        UserDetails.sid_To_UserDetails.Add(SID, new UserDetails(session));
      }
    }

    private void GetGroupMembership(Session session)
    {
      try
      {
        this.groupMembershipSids = Auth.get_group_membership(session, this.userSid);
        if (this.groupMembershipSids.Length > UserDetails.MAX_GROUP_LOOKUP)
          return;
        string[] strArray = new string[this.groupMembershipSids.Length];
        for (int index = 0; index < this.groupMembershipSids.Length; ++index)
        {
          string _subject_identifier = this.groupMembershipSids[index];
          Dictionary<string, string> informationFromIdentifier = Auth.get_subject_information_from_identifier(session, _subject_identifier);
          string str = "";
          strArray[index] = !informationFromIdentifier.TryGetValue("subject-displayname", out str) ? (!informationFromIdentifier.TryGetValue("subject-name", out str) ? _subject_identifier : str) : str;
        }
        this.groupMembershipNames = strArray;
      }
      catch (Failure ex)
      {
      }
    }

    private string GetDisplayName(Session session)
    {
      try
      {
        return new Subject()
        {
          other_config = Auth.get_subject_information_from_identifier(session, this.userSid)
        }.DisplayName;
      }
      catch (Failure ex)
      {
        return (string) null;
      }
    }

    private string GetName(Session session)
    {
      try
      {
        return new Subject()
        {
          other_config = Auth.get_subject_information_from_identifier(session, this.userSid)
        }.SubjectName;
      }
      catch (Failure ex)
      {
        return (string) null;
      }
    }
  }
}
