<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;

public class Handler : IHttpHandler {

    public void ProcessRequest (HttpContext context)
    {
        if (context.Request.Files.Count > 0)
        {
            HttpPostedFile file = context.Request.Files[0];
            string fileName = file.FileName;
            fileName = context.Server.MapPath("~/uploads/" + context.Session["Username"] + fileName);
            file.SaveAs(fileName);
        }
        context.Response.ContentType = "text/plain";
        context.Response.Write("File uploaded successfully!");
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}