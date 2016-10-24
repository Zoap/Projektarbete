using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Klassen som hämtar och hanterar mapparna för den angivna användaren.
/// Den angivna användaren ska vara den som ligger i Session["Username"]
/// </summary>
public class FolderHandler
{
    private SqlHandler _sqlHandler = new SqlHandler();
    private Dictionary<int, UserFolder> _folderList = new Dictionary<int, UserFolder>();
    private string _username;

    /// <summary>
    /// Skapar en instans av mapp hanteraren och kallar på metoderna för att hämta användarens mappar.
    /// </summary>
    /// <param name="username">Användaren mapparna ska hämtas ifrån</param>
    public FolderHandler(string username)
    {
        _username = username;
        GetFolders();
        GetUnsorted();
    }

    /// <summary>
    /// Kallar på SQLHandler för att hämta mapp informationen som är lagrad på MySQL databasen.
    /// Splittar sedan den returnerade datan och skapar UserFolder instanser för varje mapp.
    /// </summary>
    private void GetFolders()
    {
        string[] folderData = _sqlHandler.GetFolders(_username).Split('|');
        if(folderData[0] != "")
            foreach (string folderString in folderData)
            {
                _folderList.Add(
                    Int32.Parse(folderString.Split(',')[0]),            //mapp ID
                    new UserFolder(folderString, _username, false)      //mapp objekt
                    );
            }
    }
    /// <summary>
    /// Skapar en mapp för filer som är osorterad (inte har en tilldelad mapp).
    /// Databasen lagrar osorterade filer med mapp ID 0.
    /// </summary>
    private void GetUnsorted()
    {
        string folderInfo = "0,Unsorted";
        _folderList.Add(0, new UserFolder(folderInfo, _username, false));
    }
    
    /// <summary>
    /// Returnerar Dictionary med användarens mappar.
    /// </summary>
    public Dictionary<int, UserFolder> Folders
    {
        get
        {
            return _folderList;
        }
    }
}