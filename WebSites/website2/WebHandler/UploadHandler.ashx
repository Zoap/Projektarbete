<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.IO;
using System.Web.UI;

/// <summary>
/// En handler som hanterar uppladdning av filer
/// </summary>
public class Handler : IHttpHandler, IRequiresSessionState
{
    private ErrorHandling error = new ErrorHandling();

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
            {
                Directory.CreateDirectory(diskPath);
            }

            if (!Directory.Exists(diskPath + context.Session["Username"]))
            {
                Directory.CreateDirectory(diskPath + context.Session["Username"]);
            }

            // Utför felhantering på fil som står inför uppladdning
            error.Upload(file);
            
            // Om inga fel upptäckts -> ladda upp
            if (error.State)
            {
                context.Session["message"] = null;
                context.Session["color"] = null;
                formFile.SaveAs(file.GetFilePath + file.GetFileName);
                updateDB.FileUpload(file);
            }
            // Skicka tillbaka felbeskrinvning till LoggedIN.aspx
            else
            {
                context.Session["message"] = error.Message;
                context.Session["color"] = error.Color;
            }
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