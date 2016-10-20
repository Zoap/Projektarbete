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


    public bool login(string un, string pw)
    {
        bool check;
        int count;

        conn.Open();
        MySqlCommand select = new MySqlCommand("SELECT COUNT(*) FROM user WHERE username = @username AND password = @password", conn);
        select.Parameters.AddWithValue("@username", un);
        select.Parameters.AddWithValue("@password", pw);
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

    public bool checkDuplicate(string un)
    {
        bool check;
        int count;

        conn.Open();
        MySqlCommand select = new MySqlCommand("SELECT COUNT(*) FROM user WHERE username = @username", conn);
        select.Parameters.AddWithValue("@username", un);
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

    public void Register(string un, string pw)
    {
        conn.Open();
        MySqlCommand insert = new MySqlCommand("INSERT INTO user(username, password) VALUES(@username, @password)", conn);
        insert.Parameters.AddWithValue("@username", un);
        insert.Parameters.AddWithValue("@password", pw);
        insert.ExecuteNonQuery();
        commit();
        conn.Close();
    }

    private void commit()
    {
        MySqlCommand insert = new MySqlCommand("COMMIT", conn);
        insert.ExecuteNonQuery();
    }
}