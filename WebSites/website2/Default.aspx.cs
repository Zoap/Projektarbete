using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class _Default : System.Web.UI.Page
{
    private SQLHandler sql = new SQLHandler();

    protected void Page_Load(object sender, EventArgs e)
    {
        //Kollar om RegSucess är satt till True
        if (Request.QueryString["RegSuccess"] == "true")
        {
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        //NameValueCollection formCollection = Request.Form;
        string username = loginUsername.Text;//formCollection["loginUsername"];
        string password = loginPassword.Text;//formCollection["loginPassword"];

        handleLogin(username, password);
    }

    private void handleLogin(string username, string password)
    {
        //TODO: Kolla om en SESSION är satt annars ska man inte unna komma åt LoggedIN.aspx
        if (sql.Login(username) == password)
        {
            Server.Transfer("LoggedIN.aspx", true);
        }
    }

    private bool checkUsername(string un)
    {
        bool check = true;
        byte[] ASCIIValues = Encoding.ASCII.GetBytes(un);
        for (int i = 0; i < ASCIIValues.Length;)
        {
            if ((ASCIIValues[i] <= 57 && ASCIIValues[i] >= 48) || (ASCIIValues[i] <= 90 && ASCIIValues[i] >= 65) || (ASCIIValues[i] <= 122 && ASCIIValues[i] >= 97))
            {
            }
            else
            {
                check = false;
                break;
            }
            i++;
        }
        return check;
    }


    protected void btnRegistration_Click(object sender, EventArgs e)
    {
        registrationSuccess.Visible = false;

        bool regSuccess = false;
        //NameValueCollection formCollection = Request.Form;

        //Det går att hämta asp: taggarna på sidan direkt!
        string username = registrationUsername.Text;
        string password = registrationPassword.Text;
        string passwordRepeat = registrationPasswordRepeat.Text;



        //Lite fenhantering (borde kollas efter specifika chars osv.) orkarde inte regex
        if (username != "")
        {
            if (!checkUsername(username))
            {
                registrationError.Text = "*Användarnamnet får endast innehålla karaktärerna 0-9, A-Z, a-z";
                registrationError.Visible = true;
            }
            else if (!sql.checkDuplicate(username))
            {
                registrationError.Text = "*Användarnamnet är upptaget";
                registrationError.Visible = true;
            }
            else if (password == passwordRepeat && password != "")
            {
                regSuccess = sql.Register(username, password);
                if (!regSuccess)
                    registrationError.Text = "Något gick super fel :(";
            }
            else
            {
                registrationError.Text = "*Lösenordet måste matcha";
                registrationError.Visible = true;
            }
        }
        else
        {
            registrationError.Text = "*Användarnamnet är inte giltigt.";
            registrationError.Visible = true;
        }

        if (regSuccess)
        {
            registrationSuccess.Text = "Registration successfull!";
            registrationSuccess.Visible = true;
            registrationError.Visible = false;
        }
    }
}