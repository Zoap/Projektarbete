using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

/// <summary>
/// Denna klassen skapar fil objekt för hantering av data i programmet.
/// Objekten används för att spara ner filer & hitta rätt data i SQL.
/// </summary>
public class UserFile
{
    //Filinformation
    private string username, fileName, filePath;
    private double fileSize;
    private DateTime timeStamp;

    public UserFile() { }
    public UserFile(string username, string fileName, string filePath, int fileSizeBytes)
    {
        this.username = username;
        this.fileName = fileName;
        this.filePath = filePath;
        fileSize = fileSizeBytes;
        timeStamp = DateTime.Now;
    }
    public UserFile(string username, string fileName, string filePath, int fileSizeBytes, DateTime dateTime)
    {
        this.username = username;
        this.fileName = fileName;
        this.filePath = filePath;
        fileSize = fileSizeBytes;
        timeStamp = dateTime;
    }

    public DateTime GetTimeStamp { get { return timeStamp; } }
    public string GetUser { get { return username; } }
    public string GetFileName { get { return fileName; } }
    public string GetFilePath { get { return filePath; } }
    public int GetSizeB { get { return (int)fileSize; } }
    public double GetSizeKB
    {
        get
        {
            return Math.Round(fileSize / 1024, 2);
        }
    }
    public double GetSizeMB
    {
        get
        {
            return Math.Round((fileSize / 1024) / 1024, 2);
        }
    }

    public void Delete()
    {
        if (File.Exists(filePath + fileName))
        {
            File.Delete(filePath + fileName);
        }
    }
}