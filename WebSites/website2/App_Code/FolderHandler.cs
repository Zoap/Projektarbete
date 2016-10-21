using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Class1
/// </summary>
public class FolderHandler
{
    private SQLHandler sqlHandler = new SQLHandler();
    private Dictionary<int,UserFolder> folderList = new Dictionary<int,UserFolder>();

    public FolderHandler(string username)
    {
        getFolders(username);
        getUnsorted(username);
    }

    private void getFolders(string username)
    {
        string[] folderData = sqlHandler.GetFolders(username).Split('|');
        foreach(string folderString in folderData)
        {
            folderList.Add(
                Int32.Parse(folderString.Split(',')[0]),    //mapp ID
                new UserFolder(folderString, username)      //mapp objekt
                );
        }
    }
    private void getUnsorted(string username)
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