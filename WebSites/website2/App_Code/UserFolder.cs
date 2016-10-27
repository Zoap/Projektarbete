using System;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// Hanteraren för användarens mappar.
/// </summary>
public class UserFolder
{
    private SqlHandler _SQLHandler = new SqlHandler();
    private List<UserFile> _uFiles = new List<UserFile>();
    private string _userName, _folderName;
    private int _folderID;

    
    /// <summary>
    /// Skapar en instans av mappen och splittar SQLData in i korrekta variabler.
    /// Kallar sedan på metoden populateFiles för att hämta filerna som tillhör mappen.
    /// </summary>
    /// <param name="data">SQLData om isNew = true, namnet på mapp om isNew = false</param>
    /// <param name="username">Den aktiva användaren</param>
    /// <param name="isNew">True = ny mapp, false = hämta användarmappar</param>
    public UserFolder(string data, string username, bool isNew)
    {
        if(!isNew)
        { 
            _userName = username;
            _folderID = Int32.Parse(data.Split(',')[0]);
            _folderName = data.Split(',')[1];
            populateFiles();
        }
        else
        {
            _userName = username;
            _folderName = data;
            _SQLHandler.CreateFolder(this);
        }
    }

    /// <summary>
    /// Kallar på SQLHandler för att hämta och bearbeta data för filerna som tillhör mappen.
    /// Skapar sedan en UserFile instans och lägger till det i _uFiles listan
    /// </summary>
    private void populateFiles()
    {
        string[] fileData = _SQLHandler.GetFiles(_userName, _folderID).Split('|');
        if (fileData[0] != "")
            foreach (string singleFile in fileData)
            {
                string[] splitData = singleFile.Split(',');
                string fileName = splitData[0];
                string filePath = splitData[1];
                int fileSize = Int32.Parse(splitData[2]);
                DateTime time = DateTime.Parse(splitData[3]);

                _uFiles.Add(new UserFile(_userName, fileName, filePath, fileSize, time));
            }
    }

    /// <summary>
    /// Returnerar namnet på ägaren av mappen
    /// </summary>
    public string FolderOwner { get { return _userName; } }
    /// <summary>
    /// Returnerare namnet på mappen
    /// </summary>
    public string FolderName { get { return _folderName; } }
    /// <summary>
    /// Returnerar ID på mappen
    /// </summary>
    public int FolderID { get { return _folderID; } }
    /// <summary>
    /// Returnerar listan med filer
    /// </summary>
    public List<UserFile> Files { get { return _uFiles; } }

    /// <summary>
    /// Raderar mappen
    /// </summary>
    public void Delete()
    {
        string diskPath = Text.Diskpath + _userName + "/" + FolderName + "/";
        if(Directory.Exists(diskPath))
        {
            Directory.Delete(diskPath);
        }
    }
}