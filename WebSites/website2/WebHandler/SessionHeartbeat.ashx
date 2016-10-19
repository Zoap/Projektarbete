<%@ WebHandler Language="C#" Class="SessionHeartbeat" %>

using System;
using System.Web;


/// <summary>
/// Credits till http://stackoverflow.com/questions/1431733/keeping-asp-net-session-open-alive
/// </summary>

public class SessionHeartbeat : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        context.Session["Heartbeat"] = DateTime.Now;
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}