using System;
using MySql.Data.MySqlClient;

/// <summary>
/// Summary description for Class1
/// </summary>
public class SqlHandler
{
    //Skapar en ny anslutning mot databasen
    private MySqlConnection conn = new MySqlConnection(Text.Connection);
    private HashAndSalt crypto = new HashAndSalt();

    public SqlHandler()
    {
    }

    private string GetDate(string username)
    {
        MySqlCommand select = new MySqlCommand("SELECT create_time FROM user WHERE username = @username", conn);
        select.Parameters.AddWithValue("@username", username);
        string date = Convert.ToDateTime(select.ExecuteScalar()).ToString("yyyy-MM-dd HH:mm:ss");

        return date;
    }

    //Kollar user table i databasen efter angivet användarnamn och lösenord
    public bool Login(string username, string password)
    {
        bool check;
        int count;

        conn.Open();
        string date = GetDate(username);
        string hashedPassword = crypto.Hash(password, date);

        MySqlCommand select = new MySqlCommand("SELECT COUNT(*) FROM user WHERE username = @username AND password = @password", conn);
        select.Parameters.AddWithValue("@username", username);
        select.Parameters.AddWithValue("@password", hashedPassword);
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
        string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string hashedPassword = crypto.Hash(password, date);

        MySqlCommand insert = new MySqlCommand("INSERT INTO user(username, email, password) VALUES(@username, @email, @password)", conn);
        insert.Parameters.AddWithValue("@username", username);
        insert.Parameters.AddWithValue("@password", hashedPassword);
        insert.Parameters.AddWithValue("@email", email);
        insert.ExecuteNonQuery();
        Commit();
        conn.Close();
    }

    public bool CheckDuplicateFile(UserFile file, string username)
    {
        bool check;
        int count;

        conn.Open();
        MySqlCommand select = new MySqlCommand("SELECT COUNT(*) FROM files WHERE filename = @filename AND username = @username", conn);
        select.Parameters.AddWithValue("@filename", file.GetFileName);
        select.Parameters.AddWithValue("@username", username);
        count = Convert.ToInt32(select.ExecuteScalar());
        conn.Close();

        if (count >= 1)
        {
            check = false;
        }
        else
        {
            check = true;
        }

        return check;
    }

    //Kollar om användarnamnet redan finns i user table i databasen
    public bool CheckDuplicateUser(string username)
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
            check = false;
        }
        else
        {
            check = true;
        }

        return check;
    }

    // Kollar om email finns i databasen
    public bool CheckDuplicateEmail(string email)
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
            check = false;
        }
        else
        {
            check = true;
        }

        return check;
    }

    public double CheckTotalSpaceUsed(string username)
    {
        double total;
        string result;

        conn.Open();
        MySqlCommand select = new MySqlCommand("SELECT SUM(filesize) FROM files WHERE username = @username", conn);
        select.Parameters.AddWithValue("@username", username);
        result = Convert.ToString(select.ExecuteScalar());
        conn.Close();
        if (string.IsNullOrEmpty(result))
        {
            total = 0;
        }
        else
        {
            total = Convert.ToDouble(result);
        }

        if (!double.IsNaN(total) || !total.Equals(0))
        {
            return total / 1000000000;
        }
        else
        {
            return total;
        } 
    }

    //Utför en commit mot databasen
    private void Commit()
    {
        MySqlCommand insert = new MySqlCommand("COMMIT", conn);
        insert.ExecuteNonQuery();
    }

    /// <summary>
    /// Gör en insert i files
    /// </summary>
    /// <param name="file">Filen som ska skapas</param>
    /// <param name="activeFolderID">ID på mappen filen ska tillhöra</param>
    public void FileUpload(UserFile file, int activeFolderID)
    {
        conn.Open();
        MySqlCommand insert = new MySqlCommand("INSERT INTO files(username, filename, filepath, filesize, folder_id) " +
                                                "VALUES(@username, @filename, @filpath, @filesize, @folder_id)", conn);
        insert.Parameters.AddWithValue("@username", file.GetUser);
        insert.Parameters.AddWithValue("@filename", file.GetFileName);
        insert.Parameters.AddWithValue("@filpath", file.GetFilePath);
        insert.Parameters.AddWithValue("@filesize", file.GetSizeB);
        insert.Parameters.AddWithValue("@folder_id", activeFolderID);
        insert.ExecuteNonQuery();
        Commit();
        conn.Close();
    }
    /// <summary>
    /// Gör en insert i folders
    /// </summary>
    /// <param name="folder">Mappen som ska skapas</param>
    public void CreateFolder(UserFolder folder)
    {
        conn.Open();
        MySqlCommand insert = new MySqlCommand("INSERT INTO folders(folder_name, owner) " +
                                                "VALUES(@folder_name, @owner)", conn);
        insert.Parameters.AddWithValue("@folder_name", folder.FolderName);
        insert.Parameters.AddWithValue("@owner", folder.FolderOwner);
        insert.ExecuteNonQuery();
        Commit();
        conn.Close();
    }

    /// <summary>
    /// Hämtar mappar & returnerar mappar data
    /// </summary>
    /// <param name="username">Användaren som ska ha mappar</param>
    /// <returns>Mapp data</returns>
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

    /// <summary>
    /// Hämtar filer & returnerar fil data
    /// </summary>
    /// <param name="username">Användaren som ska ha filerna</param>
    /// <param name="folderID">Mappen som filerna ligger i</param>
    /// <returns></returns>
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

    /// <summary>
    /// Raderar en fil från DB
    /// </summary>
    /// <param name="username">Ägaren av filen</param>
    /// <param name="folderID">ID på mappen filen tillhör</param>
    /// <param name="fileName">Filens namn</param>
    public void DeleteFile(string username, int folderID, string fileName)
    {
        conn.Open();
        MySqlCommand delete = new MySqlCommand("DELETE FROM files WHERE username = @username AND folder_id = @folder_id AND filename = @filename" +
                                                " LIMIT 1", conn);
        delete.Parameters.AddWithValue("@username", username);
        delete.Parameters.AddWithValue("@folder_id", folderID);
        delete.Parameters.AddWithValue("@filename", fileName);
        delete.ExecuteNonQuery();
        Commit();
        conn.Close();
    }
    /// <summary>
    /// Raderar en mapp från DB
    /// </summary>
    /// <param name="username">Ägaren av mappen</param>
    /// <param name="folderID">Mappens ID</param>
    /// <param name="folderName">Mappens namn</param>
    public void DeleteFolder(string username, int folderID, string folderName)
    {
        conn.Open();
        MySqlCommand delete = new MySqlCommand("DELETE FROM folders WHERE folder_id = @folderID AND folder_name = @folderName " +
                                                "AND owner = @username LIMIT 1", conn);
        delete.Parameters.AddWithValue("@folderID", folderID);
        delete.Parameters.AddWithValue("@folderName", folderName);
        delete.Parameters.AddWithValue("@username", username);
        delete.ExecuteNonQuery();
        Commit();
        conn.Close();
    }

    /// <summary>
    /// Uppdaterar data på en fil som ska flyttas
    /// </summary>
    /// <param name="username">Ägaren av filen</param>
    /// <param name="folderIDOld">Den gamla mappens ID</param>
    /// <param name="fileName">Filens namn</param>
    /// <param name="newPath">Den nya vägen till filen</param>
    /// <param name="folderIDNew">Den nya mappens ID</param>
    public void MoveFile(string username, int folderIDOld, string fileName, string newPath, int folderIDNew)
    {
        conn.Open();
        MySqlCommand move = new MySqlCommand("UPDATE files SET filepath = @filepath, folder_id = @newID " +
                                             "WHERE username = @username AND folder_id = @oldID AND filename = @filename", conn);

        //UPDATE files SET filepath = "C:/uploads/Tivor/Pelle/", folder_id = 7
        //WHERE username = "Tivor" AND folder_id = 0 AND filename = "Dump20161019.sql"
        move.Parameters.AddWithValue("@filepath", newPath);
        move.Parameters.AddWithValue("@newID", folderIDNew);
        move.Parameters.AddWithValue("@username", username);
        move.Parameters.AddWithValue("@oldID", folderIDOld);
        move.Parameters.AddWithValue("@filename", fileName);
        move.ExecuteNonQuery();
        Commit();
        conn.Close();
    }
}