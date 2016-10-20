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

    public string login(string un)
    {
        conn.Open();
        MySqlCommand select = new MySqlCommand("SELECT password FROM user where username = @username", conn);
        select.Parameters.AddWithValue("@username", un);
        string text = Convert.ToString(select.ExecuteScalar());
        conn.Close();
                
        return text;
    }

    public bool checkDuplicate(string un)
    {
        bool check;
        conn.Open();
        MySqlCommand select = new MySqlCommand("SELECT username FROM user where username = @username", conn);
        select.Parameters.AddWithValue("@username", un);
        string text = Convert.ToString(select.ExecuteScalar());
        conn.Close();

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