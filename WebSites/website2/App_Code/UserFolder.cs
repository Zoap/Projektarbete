using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Användarens mappar
/// </summary>
public class UserFolder
{
    SQLHandler SQLHandler = new SQLHandler();
    private List<UserFile> uFiles = new List<UserFile>();
    private string userName, folderName;
    private int folderID;

    public UserFolder()
    {

    }
    public UserFolder(string SQLData, string username)
    {
        userName = username;
        folderID = Int32.Parse(SQLData.Split(',')[0]);
        folderName = SQLData.Split(',')[1];
        populateFiles();
    }

    private void populateFiles()
    {
        string[] fileData = SQLHandler.GetFiles(userName, folderID).Split('|');
        if (fileData[0] != "")
            foreach (string singleFile in fileData)
            {
                string[] splitData = singleFile.Split(',');
                string fileName = splitData[0];
                string filePath = splitData[1];
                int fileSize = Int32.Parse(splitData[2]);
                DateTime time = DateTime.Parse(splitData[3]);

                uFiles.Add(new UserFile(userName, fileName, filePath, fileSize, time));
            }
    }

    public string FolderName { get { return folderName; } }
    public int FolderID { get { return folderID; } }
    public List<UserFile> Files { get { return uFiles; } }
}