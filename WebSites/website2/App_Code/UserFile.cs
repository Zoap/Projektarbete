using System;
using System.IO;

/// <summary>
/// Denna klassen håller relevant information om filerna för hantering i användarens filträd.
/// </summary>
public class UserFile
{
    //Filinformation
    private string _username, _fileName, _filePath;
    private double _fileSize;
    private DateTime _timeStamp;
    
    public UserFile() { }
    /// <summary>
    /// Konstruktorn som används vid uppladdning av filer.
    /// </summary>
    /// <param name="username">Användaren som laddar upp</param>
    /// <param name="fileName">Namnet på filen</param>
    /// <param name="filePath">Var filen lagras</param>
    /// <param name="fileSizeBytes">Filens storlek i bytes</param>
    public UserFile(string username, string fileName, string filePath, int fileSizeBytes)
    {
        this._username = username;
        this._fileName = fileName;
        this._filePath = filePath;
        _fileSize = fileSizeBytes;
        _timeStamp = DateTime.Now;
    }
    /// <summary>
    /// Konstruktorn som används när FolderHandlern skapas för visning i filträdet.
    /// </summary>
    /// <param name="username">Ägaren av filen</param>
    /// <param name="fileName">Filnamnet</param>
    /// <param name="filePath">Var filen är</param>
    /// <param name="fileSizeBytes">Filens storlek i bytes</param>
    /// <param name="dateTime">När filen blev tilladg i MySQL</param>
    public UserFile(string username, string fileName, string filePath, int fileSizeBytes, DateTime dateTime)
    {
        this._username = username;
        this._fileName = fileName;
        this._filePath = filePath;
        _fileSize = fileSizeBytes;
        _timeStamp = dateTime;
    }

    /// <summary>
    /// Returnerar timestampen från MySQL DB:n
    /// </summary>
    public DateTime GetTimeStamp { get { return _timeStamp; } }
    /// <summary>
    /// Returnerar ägaren av filen
    /// </summary>
    public string GetUser { get { return _username; } }
    /// <summary>
    /// Returnerare filens namn
    /// </summary>
    public string GetFileName { get { return _fileName; } }
    /// <summary>
    /// Returnerare vägen till filen
    /// </summary>
    public string GetFilePath { get { return _filePath; } }
    /// <summary>
    /// Returnerare filens storlek i bytes
    /// </summary>
    public int GetSizeB { get { return (int)_fileSize; } }
    /// <summary>
    /// Returnerar filens storlek i kilo bytes
    /// </summary>
    public double GetSizeKB
    {
        get
        {
            return Math.Round(_fileSize / 1024, 2);
        }
    }
    /// <summary>
    /// Returnerar filens storlek i mega bytes
    /// </summary>
    public double GetSizeMB
    {
        get
        {
            return Math.Round((_fileSize / 1024) / 1024, 2);
        }
    }

    /// <summary>
    /// Raderar den fysiska filen om den finns på disken
    /// </summary>
    public void Delete()
    {
        if (File.Exists(_filePath + _fileName))
        {
            File.Delete(_filePath + _fileName);
        }
    }
}