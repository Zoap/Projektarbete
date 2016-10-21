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
    //Skapar en ny anslutning mot databasen
    private MySqlConnection conn = new MySqlConnection(@"server=localhost;userid=root;password=rootpassword;database=projekt;");

    public SQLHandler()
    {
    }

    //Kollar user table i databasen efter angivet användarnamn och lösenord
    public bool login(string username, string password)
    {
        bool check;
        int count;

        conn.Open();
        MySqlCommand select = new MySqlCommand("SELECT COUNT(*) FROM user WHERE username = @username AND password = @password", conn);
        select.Parameters.AddWithValue("@username", username);
        select.Parameters.AddWithValue("@password", password);
        count = Convert.ToInt32(select.ExecuteScalar());
        conn.Close();

        if (count >= 1)
        {
            check = true;
        }
        else
        {
            check = false;
        }

        return check;
    }

    //Gör en insert mot user table i databasen
    public void Register(string username, string email, string password)
    {
        conn.Open();
        MySqlCommand insert = new MySqlCommand("INSERT INTO user(username, email, password) VALUES(@username, @email, @password)", conn);
        insert.Parameters.AddWithValue("@username", username);
        insert.Parameters.AddWithValue("@password", password);
        insert.Parameters.AddWithValue("@email", email);
        insert.ExecuteNonQuery();
        commit();
        conn.Close();
    }

    //Kollar om användarnamnet redan finns i user table i databasen
    public bool checkDuplicateUser(string username)
    {
        bool check;
        int count;

        conn.Open();
        MySqlCommand select = new MySqlCommand("SELECT COUNT(*) FROM user WHERE username = @username", conn);
        select.Parameters.AddWithValue("@username", username);
        count = Convert.ToInt32(select.ExecuteScalar());
        conn.Close();

        if (count >= 1)
        {
            check = true;
        }
        else
        {
            check = false;
        }

        return check;
    }

    public bool checkDuplicateEmail(string email)
    {
        bool check;
        int count;

        conn.Open();
        MySqlCommand select = new MySqlCommand("SELECT COUNT(*) FROM user WHERE email = @email", conn);
        select.Parameters.AddWithValue("@email", email);
        count = Convert.ToInt32(select.ExecuteScalar());
        conn.Close();

        if (count >= 1)
        {
            check = true;
        }
        else
        {
            check = false;
        }

        return check;
    }

    //Utför en commit mot databasen
    private void commit()
    {
        MySqlCommand insert = new MySqlCommand("COMMIT", conn);
        insert.ExecuteNonQuery();
    }

    public void FileUpload(UserFile file)
    {
        conn.Open();
        MySqlCommand insert = new MySqlCommand("INSERT INTO files(username, filename, filepath, filesize) " +
                                                "VALUES(@username, @filename, @filpath, @filesize", conn);
        insert.Parameters.AddWithValue("@username", file.getUser);
        insert.Parameters.AddWithValue("@filename", file.getFileName);
        insert.Parameters.AddWithValue("@filpath", file.getFilePath);
        insert.Parameters.AddWithValue("@filesize", file.getSizeB);
        insert.ExecuteNonQuery();
        commit();
        conn.Close();
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
        string command = string.Format("SELECT filename,filepath, filesize, upload_time FROM files " +
                                       "WHERE username = '{0}' AND folder_id = '{1}'", username, folderID.ToString());
        string[] names = { "filename", "filepath", "filesize", "upload_time" };
        string returnData = getQueryResultMultiple(command, names);
        return returnData;
    }

    public void DeleteFile(string username, int folderID, string fileName)
    {
        string command = string.Format("DELETE FROM files " +
            "WHERE username = '{0}' AND folder_id = '{1}' AND filename = '{2}'" +
            "LIMIT 1", username, folderID, fileName);
        executeCommand(command);
    }
}


