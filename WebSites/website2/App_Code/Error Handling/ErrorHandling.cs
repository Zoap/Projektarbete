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

    private string red = "leftEventLabelFail";
    private string green = "leftEventLabelSuccess";

    //styr vilken css klass label skall anta
    private string _color = string.Empty;
    public string color
    {
        get
        {
            return _color;
        }
    }

    //Används som variabel utanför ErrorHandling för att kolla om fel upptäckts
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

    private bool checkPassword(string password)
    {
        bool check = true;
        int upper = 0;
        int special = 0;
        int number = 0;

        //kollar om lösenordet är minst 6 karaktärer långt och innehåller minst en versal och en av följande karaktärer: ] [ ? / < ~ # ` ! @ $ % ^ & * ( ) + = } | : " ; ' , > { space
        foreach (char x in password)
        {
            if ((x <= 90 && x >= 65))
            {
                upper++;
            }
            else if ((x <= 43 && x >= 32) || (x == 47) || (x <= 64 && x >= 58) || (x <= 94 && x >= 93) || (x == 91) || (x <= 126 && x >= 123))
            {
                special++;
            }
            else if ((x <= 57 && x >= 48))
            {
                number++;
            }
        }

        if (password.Length < 6)
        {
            check = false;
        }
        else if (upper <= 0)
        {
            check = false;
        }
        else if (special <= 0)
        {
            check = false;
        }
        else if (number <= 0)
        {
            check = false;
        }

        return check;
    }

    //Felhantering i registreringsfasen
    public string registration(string username, string email, string password, string passwordRepeat)
    {
        string message;

        //Lite felhantering (borde kollas efter specifika chars osv.) orkarde inte regex
        if (!string.IsNullOrEmpty(username))
        {
            if (!checkUsername(username))
            {
                message = "*Användarnamnet får endast innehålla karaktärerna 0-9, A-Z, a-z";
                _color = red;
            }
            else if (sql.checkDuplicateUser(username))
            {
                message = "*Användarnamnet är upptaget";
                _color = red;

            }
            else if (string.IsNullOrEmpty(email))
            {
                message = "*En E-mailadress måste anges";
                _color = red;
            }
            else if (sql.checkDuplicateEmail(email))
            {
                message = "*E-mail adressen är redan knutet till ett konto";
                _color = red;
            }
            else if (!checkPassword(password))
            {
                message = "*Lösenordet måste vara minst 6 tecken långt, innehålla minst en versal, en siffra och ett av följande tecken: ] [ ? / < ~ # ` ! @ $ % ^ & * ( ) + = } | : \" ; ' , > { space";
                _color = red;
            }
            else if (password == passwordRepeat && !string.IsNullOrEmpty(password))
            {
                _state = true;
                message = "Registration successfull!";
                _color = green;
            }
            else if (password != passwordRepeat && !string.IsNullOrEmpty(password))
            {
                message = "*Lösenordet måste matcha";
                _color = red;
            }
            else
            {
                message = "*Lösenordsfältet kan inte vara tomt";
                _color = red;
            }
        }
        else
        {
            message = "*Användarnamnsfältet kan inte vara tomt";
            _color = red;
        }
        return message;
    }

    //Felhantering i login fasen
    public string login(string username, string password)
    {
        string message = string.Empty;

        if (sql.login(username, password))
        {
            _state = true;
        }
        else
        {
            message = "*Felaktigt användarnamn/lösenord";
            _color = red;
        }

        return message;
    }
}