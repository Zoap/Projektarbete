using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    private SQLHandler sql = new SQLHandler();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["SessionActive"] == "false")
        {
            leftEventLabel.CssClass = "leftEventLabelFail";
            leftEventLabel.Text = "Sessionen är utgången";
            leftEventLabel.Visible = true;
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
        //IF session active -> direkt till LoggedIN.aspx
        Session.Abandon();
        if (sql.Login(username) == password)
        {
            //Skapar session
            Session["Username"] = username;
            Server.Transfer("LoggedIN.aspx", true);
        }
        else
        {
            leftEventLabel.CssClass = "leftEventLabelFail";
            leftEventLabel.Text = "Uppgifter felaktiga";
            leftEventLabel.Visible = true;
        }
    }
    protected void btnRegistration_Click(object sender, EventArgs e)
    {
        leftEventLabel.Visible = false;

        bool regSuccess = false;
        //NameValueCollection formCollection = Request.Form;

        //Det går att hämta asp: taggarna på sidan direkt!
        string username = registrationUsername.Text;
        string password = registrationPassword.Text;
        string passwordRepeat = registrationPasswordRepeat.Text;

        //Lite fenhantering (borde kollas efter specifika chars osv.) orkarde inte regex
        if (username != "")
            if (password == passwordRepeat && password != "")
            {
                regSuccess = sql.Register(username, password);
                if (!regSuccess)
                    rightEventLabel.Text = "Något gick super fel :(";
            }
            else
            {
                rightEventLabel.Text = "*Lösenordet måste matcha";
                rightEventLabel.Visible = true;
            }
        else
        {
            rightEventLabel.Text = "*Användarnamnet är inte giltigt.";
            rightEventLabel.Visible = true;
        }

        if (regSuccess)
        {
            leftEventLabel.CssClass = "leftEventLabelSuccess";
            leftEventLabel.Text = "Registration successfull!";
            leftEventLabel.Visible = true;
            rightEventLabel.Visible = false;
        }
    }
}