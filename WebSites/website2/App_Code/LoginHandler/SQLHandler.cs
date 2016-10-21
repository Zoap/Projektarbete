using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

/// <summary>
/// Summary description for Class1
/// </summary>
public class SQLHandler
{
    private MySqlConnection conn = new MySqlConnection(@"server=localhost;userid=root;password=rootpassword;database=projekt;");

    public SQLHandler()
    {
       
    }
    private string getQueryResult(string cmd)
    {
        string returnData = "";
        MySqlCommand query = new MySqlCommand(cmd, conn);
        conn.Open();
        MySqlDataReader sqlReader = query.ExecuteReader();
        try
        {
            while(sqlReader.Read())
            {
                returnData += sqlReader.GetString(0);
            }
        }
        finally
        {
            sqlReader.Close();
            conn.Close();
        }
        return returnData;
    }
    private string getQueryResultMultiple(string cmd, string[] names)
    {
        string returnData = "";
        MySqlCommand query = new MySqlCommand(cmd, conn);
        conn.Open();
        MySqlDataReader sqlReader = query.ExecuteReader();
        try
        {
            while (sqlReader.Read())
            {
                if(returnData != "")
                    returnData += "|";
                foreach (string name in names)
                {
                    returnData += sqlReader[name].ToString() + ",";
                }
            }
        }
        finally
        {
            sqlReader.Close();
            conn.Close();
        }
        return returnData;
    }

    private void executeCommand(string cmd)
    {
        conn.Open();
        MySqlCommand query = new MySqlCommand(cmd, conn);
        query.ExecuteNonQuery();
        conn.Close();
    }

    public string Login(string un)
    {
        string command = string.Format("SELECT password FROM user where username = '{0}'", un);
        string text = getQueryResult(command);

        return text;
    }

    public bool checkDuplicate(string un)
    {
        bool check;
        string command = string.Format("SELECT username FROM user where username = '{0}'", un.ToLower());
        string text = getQueryResult(command);

        if (un == text)
        {
            check = true;
        }
        else
        {
            check = false;
        }
        return check;
    }

    public bool Register(string un, string pw)
    {
        string command = string.Format("INSERT INTO user(username, password) VALUES('{0}', '{1}')", un, pw);
        executeCommand(command);

        return true;
    }

    public void FileUpload(UserFile file)
    {
        string command = string.Format("INSERT INTO files(username, filename, filepath, filesize, upload_time, folder_id) "+
                                       "VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')", file.getUser, file.getFileName, file.getFilePath, file.getSizeB, file.getTimeStamp, null);
        executeCommand(command);
    }

    public string GetFolders(string username)
    {
        string command = string.Format("SELECT folder_id,folder_name FROM folders WHERE owner = '{0}'", username);
        string[] names = { "folder_id", "folder_name" };
        string returnData = getQueryResultMultiple(command, names);
        return returnData;
    }
    public string GetUnsorted(string username)
    {
        string command = string.Format("SELECT filename,filepath, filesize, upload_time FROM files " +
                                       "WHERE username = '{0}' AND folder_id = '{1}'", username, "0");
        string[] names = { "filename", "filepath", "filesize", "upload_time" };
        string returnData = getQueryResultMultiple(command, names);
        return returnData;
    }

    public string GetFiles(string username, int folderID)
    {
        string command = string.Format("SELECT filename,filepath, filesize, upload_time FROM files "+
                                       "WHERE username = '{0}' AND folder_id = '{1}'", username, folderID.ToString());
        string[] names = { "filename", "filepath", "filesize", "upload_time" };
        string returnData = getQueryResultMultiple(command, names);
        return returnData;
    }

    public void DeleteFile(string username, int folderID, string fileName)
    {
        string command = string.Format("DELETE FROM files " +
            "WHERE username = '{0}' AND folder_id = '{1}' AND filename = '{2}'" +
            "LIMIT 1",username, folderID, fileName);
        executeCommand(command);
    }
}