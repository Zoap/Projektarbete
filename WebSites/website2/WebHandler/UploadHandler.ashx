<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.IO;

/// <summary>
/// En handler som hanterar uppladdning av filer
/// </summary>
public class Handler : IHttpHandler, IRequiresSessionState {

    public void ProcessRequest (HttpContext context)
    {
        if (context.Request.Files.Count > 0)
        {
            SqlHandler updateDB = new SqlHandler();
            HttpPostedFile formFile = context.Request.Files[0];
            string diskPath = "C:/uploads/";
            string fullPath = diskPath + context.Session["Username"] + "/";
            UserFile file = new UserFile(context.Session["Username"].ToString(), formFile.FileName, fullPath, formFile.ContentLength);

            if (!Directory.Exists(diskPath))
                Directory.CreateDirectory(diskPath);
            if (!Directory.Exists(diskPath + context.Session["Username"]))
                Directory.CreateDirectory(diskPath + context.Session["Username"]);

            formFile.SaveAs(file.GetFilePath + file.GetFileName);
            updateDB.FileUpload(file);
        }
        context.Response.ContentType = "text/plain";
        context.Response.Write("Handler request successfull.");
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}