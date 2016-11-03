using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class LoggedIN : System.Web.UI.Page
{
    private FolderHandler _userFolders;
    private ErrorHandling error = new ErrorHandling();

    protected void Page_Load(object sender, EventArgs e)
    {
        leftEventLabel.Visible = false;

        //Kollar om Username finns i den aktiva sessionen
        if (String.IsNullOrEmpty((string)Session["Username"]))
        {
            string test = (string)Session["Username"];

            Response.Redirect("Default.aspx?SessionActive=false", false);
        }
        else
        {
            if (Session["userFolders"] == null)
            {
                getUserFiles(Session["Username"].ToString());
            } 
            else
            {
                _userFolders = (FolderHandler)Session["userFolders"];
                populateFolders();
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
        if (!string.IsNullOrEmpty((string)Session["message"]))
        {
            leftEventLabel.CssClass = (string)Session["color"];
            leftEventLabel.Text = (string)Session["message"];
            leftEventLabel.Visible = true;
        }  
    }

    /// <summary>
    /// Skapar en ny instans av _userFolders, används när filträdet behöver förnyas
    /// </summary>
    /// <param name="user">Den aktiva användaren</param>
    private void getUserFiles(string user)
    {
        _userFolders = new FolderHandler(user);
        populateFolders();
        Session["userFolders"] = _userFolders;
    }
    /// <summary>
    /// Skapar listan med mappar som tillhör användaren
    /// </summary>
    private void populateFolders()
    {
        folderSelectionExisting.Controls.Clear();
        fileSelection.Style.Add("display", "none");

        foreach (UserFolder folder in _userFolders.Folders.Values)
        {
            HtmlGenericControl folderWrapper = new HtmlGenericControl("DIV");
            
            //Radera mapp
            HtmlGenericControl deleteFolder = new HtmlGenericControl("DIV");
            deleteFolder.ID = "delete_folder_" + folder.FolderID;
            deleteFolder.Attributes["class"] = "folderGraphics";
            deleteFolder.Attributes.Add("onclick",
            Page.ClientScript.GetPostBackEventReference(deleteFolder, deleteFolder.ID));
            deleteFolder.InnerHtml = "<img src='/Images/delete.png' height='15' width='15'>";
            folderWrapper.Controls.Add(deleteFolder);

            //Välja mapp
            HtmlGenericControl viewFolder = new HtmlGenericControl("DIV");
            viewFolder.ID = "folder_" + folder.FolderID;
            viewFolder.Attributes["class"] = "folderGraphics";
            viewFolder.Attributes.Add("onclick",
            Page.ClientScript.GetPostBackEventReference(viewFolder, viewFolder.ID));
            viewFolder.InnerHtml = "<img src='/Images/folder.png' height='15' width='15'><p>" + folder.FolderName + "</p>";
            folderWrapper.Controls.Add(viewFolder);

            folderSelectionExisting.Controls.Add(folderWrapper);
        }

        //Mappen "Unsorted" som standard val
        if (string.IsNullOrEmpty((string)Session["activeFolder"]))
            GetFolderFiles("0");
        else
            GetFolderFiles(Session["activeFolder"].ToString().Split('_')[1]);

    }

    /// <summary>
    /// Innehåller:
    /// *Grafikfix för highligt av den aktiva mappen
    /// *Skapandet av tabellen som visar filerna i filträdet
    /// </summary>
    /// <param name="selectedFolder">Den valda mappen</param>
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
        Session["activeFolderName"] = _userFolders.Folders[folderKey].FolderName;
        fileSelection.Controls.Clear();

        //Skapa filträdet
        HtmlTable fileTable = new HtmlTable();
        fileTable.ID = "fileTable";

        //header
        fileTable.Rows.Add(getTableRow(new string[8] {"","Filename", "Filesize", "Upload date", "Folder", "", "",""}, null));
        //Filer
        foreach(UserFile file in _userFolders.Folders[folderKey].Files)
        {
            fileTable.Rows.Add(getTableRow(new string[8] {
                "<div id='file_" + file.GetFileName + "' class='fileGraphics'>",
                file.GetFileName,
                file.GetSizeB.ToString(),
                file.GetTimeStamp.ToShortDateString(),
                "selectFolder",
                "<img src='/Images/download.png' height='15' width='15' onclick='__doPostBack(\"" + file.GetFileName + "\",\"download_" + file.GetFileName + "\")'>",
                "<img src='/Images/delete.png' height='13' width='13' onclick='__doPostBack(\"" + file.GetFileName + "\",\"delete_file_" + file.GetFileName + "\")'>",
                "</div>"
            }, file));
        }
        fileSelection.Controls.Add(fileTable);
    }
    /// <summary>
    /// Skapar tabell rader för filträdet
    /// </summary>
    /// <param name="tableData">Datan som varje cell ska innehålla</param>
    /// <param name="file">Filen cellen tillhör</param>
    /// <returns>En färdig row</returns>
    private HtmlTableRow getTableRow(string[] tableData, UserFile file)
    {
        HtmlTableRow tableRow = new HtmlTableRow();
        foreach (string cellData in tableData)
        {
            HtmlTableCell tableCell = new HtmlTableCell();
            if (cellData == "selectFolder")
            {
                tableCell.Controls.Add(getDropDown(file));
            }
            else
            {
                tableCell.InnerHtml = cellData;
            }
            tableRow.Cells.Add(tableCell);
        }

        return tableRow;
    }
    
    /// <summary>
    /// Skapar och returnerar dropdownlistan med mappar
    /// </summary>
    /// <param name="file">Filen som listan ska tillhöra</param>
    /// <returns>Färdig ddList</returns>
    private DropDownList getDropDown(UserFile file)
    {
        DropDownList ddList = new DropDownList();
        string activeFolder = Session["activeFolder"].ToString().Split('_')[1];
        ddList.ID = "ddList_" + file.GetFileName;
        foreach(UserFolder folder in _userFolders.Folders.Values)
        {
            ListItem item = new ListItem();
            item.Text = folder.FolderName;
            item.Value = folder.FolderID.ToString();

            if (activeFolder == folder.FolderID.ToString())
                item.Selected = true;

            ddList.Items.Add(item);
        }
        ddList.SelectedIndexChanged += DdList_SelectedIndexChanged;
        ddList.AutoPostBack = true;

        return ddList;
    }

    private void DdList_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList list = (DropDownList)sender;
        SqlHandler SQLHandler = new SqlHandler();

        string[] parts = list.ID.Split('_').Skip(1).ToArray();
        string fileName = string.Join("_", parts);
        int currFolderID = Int32.Parse(Session["activeFolder"].ToString().Split('_')[1]);
        int destFolderID = Int32.Parse(list.SelectedValue);
        UserFolder currFolder = _userFolders.Folders[currFolderID];
        UserFolder destFolder = _userFolders.Folders[destFolderID];
        UserFile fileToMove = currFolder.Files.Find(file => file.GetFileName == fileName);

        if (currFolder.FolderID != destFolder.FolderID)
        {
            string currLocation = fileToMove.GetFilePath;
            string destLocation = Text.Diskpath + destFolder.FolderOwner + "/" + destFolder.FolderName + "/";
            if (File.Exists(fileToMove.GetFilePath + fileToMove.GetFileName))
            {
                File.Move(currLocation + fileToMove.GetFileName, destLocation + fileToMove.GetFileName);
                SQLHandler.MoveFile(destFolder.FolderOwner, currFolder.FolderID, fileToMove.GetFileName, destLocation, destFolder.FolderID);
            }
            else
            {
                //Filen finns inte
            }
        }
        else
        {
            //Går inte att flytta till samma mapp
        }
        getUserFiles(Session["Username"].ToString());
    }

    /// <summary>
    /// Ladda ner en fil
    /// </summary>
    /// <param name="selectedFile">Namnet på filen som ska raderas</param>
    private void downloadSelectedFile(string selectedFile)
    {
        int activeFolderID = Int32.Parse(folderSelectionExisting.FindControl(Session["activeFolder"].ToString()).ID.Split('_')[1]);
        UserFile fileInfo = _userFolders.Folders[activeFolderID].Files.Find(file => file.GetFileName == selectedFile);

        if (File.Exists(fileInfo.GetFilePath + fileInfo.GetFileName))
        {
            Response.Clear();
            Response.ClearHeaders();
            Response.ClearContent();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + fileInfo.GetFileName);
            Response.AddHeader("Content-Length", fileInfo.GetSizeB.ToString());
            Response.ContentType = "text/plain";
            Response.TransmitFile(fileInfo.GetFilePath + fileInfo.GetFileName);
            Response.End();
        }
    }
    /// <summary>
    /// Radera en fil
    /// </summary>
    /// <param name="selectedFile">Namnet på filen som ska raderas</param>
    private void deleteSelectedFile(string selectedFile)
    {
        //Hämtar rätt fil
        int activeFolderID = Int32.Parse(folderSelectionExisting.FindControl(Session["activeFolder"].ToString()).ID.Split('_')[1]);
        UserFile fileToDelete = _userFolders.Folders[activeFolderID].Files.Find(file => file.GetFileName == selectedFile);

        //Raderar filen
        SqlHandler sqlHandler = new SqlHandler();
        sqlHandler.DeleteFile(Session["Username"].ToString(), activeFolderID, fileToDelete.GetFileName);
        fileToDelete.Delete();

        //Uppdaterar filträdet
        getUserFiles(Session["Username"].ToString());
    }
    /// <summary>
    /// Radera en mapp
    /// </summary>
    /// <param name="selectedFolder">ID på mappen som ska raderas</param>
    private void deleteSelectedFolder(string selectedFolder)
    {
        error.Delete(selectedFolder);

        if (!error.State)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Otillåten Handling", "alert('" + error.Message + "')", true);
        }
        else
        {
            //Hämtar mappen
            int activeFolderID = Int32.Parse(selectedFolder);
            UserFolder folderToDelete = _userFolders.Folders[activeFolderID];
            SqlHandler sqlHandler = new SqlHandler();

            //Tar bort alla filer som tillhör mappen
            foreach (UserFile file in folderToDelete.Files)
            {
                sqlHandler.DeleteFile(Session["Username"].ToString(), activeFolderID, file.GetFileName);
                file.Delete();
            }
            //Raderar mappen
            sqlHandler.DeleteFolder(folderToDelete.FolderOwner, folderToDelete.FolderID, folderToDelete.FolderName);
            folderToDelete.Delete();
            _userFolders.Folders.Remove(activeFolderID);

            //Ändrar den aktiva mappen om den borttagna var aktiv
            int activeFolder = Int32.Parse(Session["activeFolder"].ToString().Split('_')[1]);
            if (activeFolder == folderToDelete.FolderID)
                Session["activeFolder"] = "folder_0";
            //Uppdaterar mappträdet
            populateFolders();
        }
    }

    protected void btnCreateFolder_Click(object sender, EventArgs e)
    {
        string path = Text.Diskpath + Session["Username"].ToString() + "/";
        if (!Directory.Exists(path + createFolderName.Text))
        {
            UserFolder folder = new UserFolder(createFolderName.Text, Session["Username"].ToString(), true);
            Directory.CreateDirectory(path + folder.FolderName);
        }
        else
        {
            //Mappen finns redan
        }
        createFolderName.Text = string.Empty;
        getUserFiles(Session["Username"].ToString());
    }

    protected void btnLogOut_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("Default.aspx?Logout=true", true);
    }

    /// <summary>
    /// Eventhanterare för ändringar i filträdet / uppladdning av fil
    /// </summary>
    /// <param name="eArg">Identifierare av eventet</param>
    public void RaisePostBackEvent(string eArg)
    {
        //Uppladdnings event
        if (eArg == "uploadSuccess")
        {
            getUserFiles(Session["Username"].ToString());

            leftEventLabel.Visible = false;
        }
            
        //Click event i filhanteraren
        string senderType = eArg.Split('_')[0];

        if (senderType == "folder") //Val av mapp
        {
            GetFolderFiles(eArg.Split('_')[1]);
        }
        else if (senderType == "download") //Ladda ner en fil
        {
            string[] parts = eArg.Split('_').Skip(1).ToArray();
            string fileName = string.Join("_", parts);
            downloadSelectedFile(fileName);
        }
        else if (senderType == "delete")    //Borttag -
        {
            if (eArg.Split('_')[1] == "file") // - av fil
            {
                string[] parts = eArg.Split('_').Skip(2).ToArray();
                string fileName = string.Join("_", parts);
                deleteSelectedFile(fileName);
            }
            else if (eArg.Split('_')[1] == "folder") // - av mapp
            {
                string[] parts = eArg.Split('_').Skip(2).ToArray();
                string folderName = string.Join("_", parts);
                deleteSelectedFolder(folderName);
            }
        }
    }
}
 