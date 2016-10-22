﻿using System;
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
                if (returnData != "")
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
        string returnData = "";

        conn.Open();
        MySqlCommand select = new MySqlCommand("SELECT folder_id,folder_name FROM folders WHERE owner = @username", conn);
        select.Parameters.AddWithValue("@username", username);

        string[] names = { "folder_id", "folder_name" };

        MySqlDataReader sqlReader = select.ExecuteReader();

        try
        {
            while (sqlReader.Read())
            {
                if (returnData != "")
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
    public string GetUnsorted(string username)
    {
        string returnData = "";

        conn.Open();
        MySqlCommand select = new MySqlCommand("SELECT filename,filepath, filesize, upload_time FROM files " +
                                       "WHERE username = @username AND folder_id = @folder_id'", conn);
        select.Parameters.AddWithValue("@username", username);
        select.Parameters.AddWithValue("@folder_id", "0");

        string[] names = { "folder_id", "folder_name" };

        MySqlDataReader sqlReader = select.ExecuteReader();

        try
        {
            while (sqlReader.Read())
            {
                if (returnData != "")
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

    public string GetFiles(string username, int folderID)
    {
        
        
        string returnData = "";

        conn.Open();
        MySqlCommand select = new MySqlCommand("SELECT filename,filepath, filesize, upload_time FROM files " +
                                       "WHERE username = @username AND folder_id = @folder_id", conn);
        select.Parameters.AddWithValue("@username", username);
        select.Parameters.AddWithValue("@folder_id", folderID.ToString());

        string[] names = { "filename", "filepath", "filesize", "upload_time" };

        MySqlDataReader sqlReader = select.ExecuteReader();

        try
        {
            while (sqlReader.Read())
            {
                if (returnData != "")
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

    public void DeleteFile(string username, int folderID, string fileName)
    {
        conn.Open();
        MySqlCommand delete = new MySqlCommand("DELETE FROM files WHERE username = @username AND folder_id = @folder_id AND filename = @filename" +
                                                "LIMIT 1", conn);
        delete.Parameters.AddWithValue("@username", username);
        delete.Parameters.AddWithValue("@folder_id", folderID);
        delete.Parameters.AddWithValue("@filename", fileName);
        commit();
        conn.Close();
    }
}


