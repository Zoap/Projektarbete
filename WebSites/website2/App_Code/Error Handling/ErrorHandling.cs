using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

/// <summary>
/// Summary description for ErrorHandling
/// </summary>
public class ErrorHandling
{
    private SQLHandler sql = new SQLHandler();

    //private SQLHandler sql = new SQLHandler();
    private string _color = string.Empty;
    public string color
    {
        get
        {
            return _color;
        }
    }

    private bool _state = false;
    public bool state
    {
        get
        {
            return _state;
        }
    }


    public ErrorHandling()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //Kollar username efter andra tecken än 0-9, A-Z, a-z
    private bool checkUsername(string un)
    {
        bool check = true;

        foreach (char x in un)
        {
            if (!(x <= 122 && x >= 97) && !(x <= 90 && x >= 65) && !(x <= 57 && x >= 48))
            {
                check = false;
                break;
            }
        }
        return check;
    }

    public string registration(string username, string password, string passwordRepeat)
    {
        string message;
        string red = "leftEventLabelFail";
        string green = "leftEventLabelSuccess";

        //Lite felhantering (borde kollas efter specifika chars osv.) orkarde inte regex
        if (username != "")
        {
            if (!checkUsername(username))
            {
                message = "*Användarnamnet får endast innehålla karaktärerna 0-9, A-Z, a-z";
                _color = red;
            }
            else if (sql.checkDuplicate(username))
            {
                message = "*Användarnamnet är upptaget";
                _color = red;
            }
            else if (password == passwordRepeat && password != "")
            {
                _state = true;
                message = "Registration successfull!";
                _color = green;
            }
            else
            {
                message = "*Lösenordet måste matcha";
                _color = red;
            }
        }
        else
        {
            message = "*Användarnamnet är inte giltigt.";
            _color = red;
        }
        return message;
    }
    public string login(string username, string password)
    {
        string message = string.Empty;
        string pass = sql.login(username);

        if (password == pass && !string.IsNullOrEmpty(pass))
        {
            _state = true;
        }
        else
        {
            message = "*Felaktigt användarnamn/lösenord";
        }

        return message;
    }
}