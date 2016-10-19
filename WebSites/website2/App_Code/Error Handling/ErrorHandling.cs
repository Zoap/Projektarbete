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
    public string color = string.Empty;

    public ErrorHandling()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    //Kollar username efter andra tecken än 0-9, A-Z, a-z
    public bool checkUsername(string un)
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
        string message = string.Empty;


        //Lite felhantering (borde kollas efter specifika chars osv.) orkarde inte regex
        if (username != "")
        {
            if (!checkUsername(username))
            {
                message = "*Användarnamnet får endast innehålla karaktärerna 0-9, A-Z, a-z";
                color = "leftEventLabelFail";
            }
            else if (sql.checkDuplicate(username))
            {
                message = "*Användarnamnet är upptaget";
                color = "leftEventLabelFail";
            }
            else if (password == passwordRepeat && password != "")
            {
                sql.Register(username, password);
                message = "Registration successfull!";
                color = "leftEventLabelSuccess";
            }
            else
            {
                message = "*Lösenordet måste matcha";
                color = "leftEventLabelFail";
            }
        }
        else
        {
            message = "*Användarnamnet är inte giltigt.";
            color = "leftEventLabelFail";
        }
        
            return message;
    }

}