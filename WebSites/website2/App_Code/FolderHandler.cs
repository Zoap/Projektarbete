using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Class1
/// </summary>
public class FolderHandler
{
    private SqlHandler sqlHandler = new SqlHandler();
    private Dictionary<int, UserFolder> folderList = new Dictionary<int, UserFolder>();

    public FolderHandler(string username)
    {
        GetFolders(username);
        GetUnsorted(username);
    }

    private void GetFolders(string username)
    {
        string[] folderData = sqlHandler.GetFolders(username).Split('|');
        if(folderData[0] != "")
            foreach (string folderString in folderData)
            {
                folderList.Add(
                    Int32.Parse(folderString.Split(',')[0]),    //mapp ID
                    new UserFolder(folderString, username)      //mapp objekt
                    );
            }
    }
    private void GetUnsorted(string username)
    {
        string folderInfo = "0,Unsorted";
        folderList.Add(0, new UserFolder(folderInfo, username));
    }

    public Dictionary<int, UserFolder> Folders
    {
        get
        {
            return folderList;
        }
    }
}