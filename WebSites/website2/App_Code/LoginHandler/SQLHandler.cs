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
    private MySqlConnection conn = new MySqlConnection(@"server=localhost;userid=root;database=projekt;");

    public SQLHandler()
    {
       
    }

    public void Login(string un, string pw)
    {
        conn.Open();
        MySqlCommand query = new MySqlCommand("SELECT * FROM login", conn);
        //Liten ändring
    }
}