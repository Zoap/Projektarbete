using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.IO;

public partial class LoggedIN : System.Web.UI.Page
{
    private FolderHandler userFolders;
    protected void Page_Load(object sender, EventArgs e)
    {
        //Kollar om Username finns i den aktiva sessionen
        if (String.IsNullOrEmpty((string)Session["Username"]))
        {
            Server.Transfer("Default.aspx?SessionActive=false", false);
        }
        else
        {
            if (Session["userFolders"] == null)
                GetUserFiles(Session["Username"].ToString());
            else
            {
                userFolders = (FolderHandler)Session["userFolders"];
                PopulateFolders();
            }

        }
        if (IsPostBack)
        {
            string eArg = Request["__EVENTARGUMENT"];
            if (!string.IsNullOrEmpty(eArg))
            {
                RaisePostBackEvent(eArg);
            }
        }

    }

    private void GetUserFiles(string user)
    {
        userFolders = new FolderHandler(user);
        PopulateFolders();
        Session["userFolders"] = userFolders;
    }
    private void PopulateFolders()
    {
        folderSelectionExisting.Controls.Clear();
        fileSelection.Style.Add("display", "none");
        foreach (UserFolder folder in userFolders.Folders.Values)
        {
            HtmlGenericControl folderDiv = new HtmlGenericControl("DIV");
            folderDiv.ID = "folder_" + folder.FolderID;
            folderDiv.Attributes["class"] = "folderGraphics";
            folderDiv.Attributes.Add("onclick",
            Page.ClientScript.GetPostBackEventReference(folderDiv, folderDiv.ID));
            folderDiv.InnerHtml = "<img src='/Images/folder.png' height='15' width='15'><p>" + folder.FolderName + "</p>";
            folderSelectionExisting.Controls.Add(folderDiv);
        }
    }
    protected void GetFolderFiles(string selectedFolder)
    {

        int folderKey = Int32.Parse(selectedFolder);

        //Grafik fix
        fileSelection.Style.Add("display", "block");
        HtmlGenericControl activeDiv;
        if (Session["activeFolder"] != null)
        {
            activeDiv = (HtmlGenericControl)folderSelectionExisting.FindControl(Session["activeFolder"].ToString());
            activeDiv.Attributes["class"] = "folderGraphics";
        }
        activeDiv = (HtmlGenericControl)folderSelectionExisting.FindControl("folder_" + selectedFolder);
        activeDiv.Attributes["class"] = "folderGraphicsActive";
        Session["activeFolder"] = "folder_" + selectedFolder;

        fileSelection.Controls.Clear();

        fileSelection.InnerHtml = "<table><tr><td>Filename</td><td>Filesize</td><td>Upload date</td><td></td><td></td></tr>";

        //Hämta filer
        foreach (UserFile file in userFolders.Folders[folderKey].Files)
        {
            HtmlGenericControl fileDiv = new HtmlGenericControl("DIV");
            fileDiv.ID = "file_" + file.GetFileName;
            fileDiv.Attributes["class"] = "fileGraphics";
            fileDiv.Attributes.Add("onclick",
            Page.ClientScript.GetPostBackEventReference(fileDiv, fileDiv.ID));
            fileDiv.InnerHtml = "<tr><td>" + file.GetFileName
                              + "</td><td>" + file.GetSizeB
                              + "</td><td>" + file.GetTimeStamp.Date.ToShortDateString()
                              + "</td><td><img src='/Images/download.png' height='15' width='15' onclick='__doPostBack(\"" + file.GetFileName + "\",\"download_" + file.GetFileName + "\")'>"
                              + "</td><td><img src='/Images/delete.png' height='13' width='13' onclick='__doPostBack(\"" + file.GetFileName + "\",\"delete_file_" + file.GetFileName + "\")'></td></tr>";
            fileSelection.Controls.Add(fileDiv);
        }

    }

    protected void DownloadSelectedFile(string selectedFile)
    {
        PopulateFolders();
    }
    protected void DeleteSelectedFile(string selectedFile)
    {
        int activeFolderID = Int32.Parse(folderSelectionExisting.FindControl(Session["activeFolder"].ToString()).ID.Split('_')[1]);
        UserFile fileToDelete = userFolders.Folders[activeFolderID].Files.Find(file => file.GetFileName == selectedFile);

        SqlHandler sqlHandler = new SqlHandler();
        sqlHandler.DeleteFile(Session["Username"].ToString(), activeFolderID, fileToDelete.GetFileName);
        fileToDelete.Delete();

        GetUserFiles(Session["Username"].ToString());
    }

    public void RaisePostBackEvent(string eArg)
    {
        if (eArg == "uploadSuccess")
        {
            GetUserFiles(Session["Username"].ToString());
        }
        string senderType = eArg.Split('_')[0];
        if (senderType == "folder")
        {
            GetFolderFiles(eArg.Split('_')[1]);
        }
        else if (senderType == "download")
        {
            DownloadSelectedFile(eArg.Split('_')[1]);
        }
        else if (senderType == "delete")
        {
            if (eArg.Split('_')[1] == "file")
            {
                string[] parts = eArg.Split('_').Skip(2).ToArray();
                string fileName = string.Join("_", parts);
                DeleteSelectedFile(fileName);
            }
            else if (eArg.Split('_')[1] == "folder")
            {

            }
        }
    }
}