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

    //Gör en insert mot filer table i databasen
    public void upload(string username, string filename, string filepath)
    {
        conn.Open();
        MySqlCommand insert = new MySqlCommand("INSERT INTO filer(username, filename, filepath) VALUES(@username, @filename, @filepath)", conn);
        insert.Parameters.AddWithValue("@username", username);
        insert.Parameters.AddWithValue("@filename", filename);
        insert.Parameters.AddWithValue("@filpath", filepath);
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
}