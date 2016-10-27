using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Text
/// </summary>
public class Text
{
    public Text()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //private static string _diskpath = "/var/www/projectdrop.se/data/"; // Linux
    private static string _diskpath = "c:/uploads/"; // Windows
    //private static string _connection = @"server=localhost;userid=projektuser;password=yM6vsHoDVQj2EPNE#;database=projekt;"; // Linux
    private static string _connection = @"server=localhost;userid=root;password=rootpassword;database=projekt;"; // Windows

    public static string Diskpath { get { return _diskpath; } }
    public static string Connection { get { return _connection; } }
}