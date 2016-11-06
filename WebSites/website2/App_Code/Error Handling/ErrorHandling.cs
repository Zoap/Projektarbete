using System.Text.RegularExpressions;

/// <summary>
/// Summary description for ErrorHandling
/// </summary>
public class ErrorHandling
{
    private SqlHandler sql = new SqlHandler();

    private string red = "leftEventLabelFail";
    private string green = "leftEventLabelSuccess";

    //styr vilken css klass label skall anta
    private string _color = string.Empty;
    /// <summary>
    /// Gives the error labels correct presentation
    /// </summary>
    public string Color
    {
        get
        {
            return _color;
        }
    }


    private string _message = string.Empty;
    /// <summary>
    /// Sets the error message
    /// </summary>
    public string Message
    {
        get
        {
            return _message;
        }
    }

    //Används som variabel utanför ErrorHandling för att kolla om fel upptäckts
    private bool _state = false;
    /// <summary>
    /// Controls operations after error control
    /// </summary>
    public bool State
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
    /// <summary>
    /// Checks the entered username for the set requirements
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    private bool CheckUsername(string username)
    {
        bool check = true;

        foreach (char x in username)
        {
            if (!(x <= 122 && x >= 97) && !(x <= 90 && x >= 65) && !(x <= 57 && x >= 48))
            {
                check = false;
                break;
            }
        }

        return check;
    }

    /// <summary>
    /// Simplifies the Regex method slightly
    /// </summary>
    /// <param name="source"></param>
    /// <param name="regex"></param>
    /// <returns></returns>
    private bool Match(string source, string regex)
    {
        return Regex.IsMatch(source, regex);
    }

    /// <summary>
    /// Checks if the entered email address is a valid email address
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    private bool CheckEmail(string email)
    {
        bool check;

        //Regex kommer från http://www.rhyous.com/2010/06/15/regular-expressions-in-cincluding-a-new-comprehensive-email-pattern/
        if (Match(email, @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$"))
        {
            check = true;
        }
        else
        {
            check = false;
        }

        return check;
    }

    /// <summary>
    /// Checks the password entered for the set requirements
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    private bool CheckPassword(string password)
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
    /// <summary>
    /// Performs error detection on the registration form
    /// </summary>
    /// <param name="username"></param>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <param name="passwordRepeat"></param>
    public void Registration(string username, string email, string password, string passwordRepeat)
    {
        //Lite felhantering (borde kollas efter specifika chars osv.) orkarde inte regex
        if (!string.IsNullOrEmpty(username))
        {
            if (!CheckUsername(username))
            {
                _message = "*Användarnamnet får endast innehålla karaktärerna 0-9, A-Z, a-z";
                _color = red;
            }
            else if (!sql.CheckDuplicateUser(username))
            {
                _message = "*Användarnamnet är upptaget";
                _color = red;

            }
            else if (string.IsNullOrEmpty(email))
            {
                _message = "*En E-mailadress måste anges";
                _color = red;
            }
            else if (!sql.CheckDuplicateEmail(email))
            {
                _message = "*E-mail adressen är redan knutet till ett konto";
                _color = red;
            }
            else if (!CheckEmail(email))
            {
                _message = "*E-mail adressen är inte giltig";
                _color = red;
            }
            else if (!CheckPassword(password))
            {
                _message = "*Lösenordet måste vara minst 6 tecken långt, innehålla minst en versal, en siffra och ett av följande tecken: ] [ ? / < ~ # ` ! @ $ % ^ & * ( ) + = } | : \" ; ' , > { space";
                _color = red;
            }
            else if (password == passwordRepeat && !string.IsNullOrEmpty(password))
            {
                _state = true;
                _message = "Registration successfull!";
                _color = green;
            }
            else if (password != passwordRepeat && !string.IsNullOrEmpty(password))
            {
                _message = "*Lösenordet måste matcha";
                _color = red;
            }
            else
            {
                _message = "*Lösenordsfältet kan inte vara tomt";
                _color = red;
            }
        }
        else
        {
            _message = "*Användarnamnsfältet kan inte vara tomt";
            _color = red;
        }
    }

    //Felhantering i login fasen
    /// <summary>
    /// Checks if username and password exists in the database
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    public void Login(string username, string password)
    {
        if (sql.Login(username, password))
        {
            _state = true;
        }
        else
        {
            _message = "*Felaktigt användarnamn/lösenord";
            _color = red;
        }
    }
    /// <summary>
    /// Performs error detection on the file pending upload
    /// </summary>
    /// <param name="file"></param>
    /// <param name="username"></param>
    public void Upload(UserFile file, string username)
    {
        if (file.GetSizeMB > 200)
        {
            _message = "*Filen är för stor";
            _color = red;
        }
        else if (!sql.CheckDuplicateFile(file, username))
        {
            _message = "*Det existerar redan en fil med det namnet";
            _color = red;
        }
        else if (!Match(file.GetFileName, @"(?i)(\.)(pdf|txt|jpeg|jpg|png|zip|rar|7z)"))
        {
            _message = "*Filtypen är förbjuden";
            _color = red;
        }
        else if ((sql.CheckTotalSpaceUsed(username) + (file.GetSizeMB / 1000)) > 1)
        {
            _message = "*Du har överskridit din totala lagringskvot på 1GB";
            _color = red;
        }
        else
        {
            _state = true;
        }
    }
    /// <summary>
    /// Determines if the folder that is up for deletion is the default folder
    /// </summary>
    /// <param name="folderID"></param>
    public void Delete(string folderID)
    {
        if (folderID.Equals("0"))
        {
            _message = "Unsorted kan inte raderas!";
        }
        else
        { 
            _state = true;
        }
    }
}