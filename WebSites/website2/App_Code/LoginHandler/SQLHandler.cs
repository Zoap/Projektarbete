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
    private MySqlConnection conn = new MySqlConnection(@"server=localhost;userid=root;password=rootpassword;database=projekt;");

    public SQLHandler()
    {   
    }
    
    private string getQueryResult(string cmd)
    {
        
        conn.Open();
        MySqlCommand query = new MySqlCommand(cmd, conn);
        string text = Convert.ToString(query.ExecuteScalar());
        conn.Close();

        return text;
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
        string command = string.Format("SELECT username FROM user where username = '{0}'", un);
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

    public void Register(string un, string pw)
    {
        string command = string.Format("INSERT INTO user(username, password) VALUES('{0}', '{1}')", un, pw);
        executeCommand(command);
    }
}