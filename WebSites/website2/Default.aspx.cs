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
    private ErrorHandling error = new ErrorHandling();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["SessionActive"] == "false")
        {
            leftEventLabel.CssClass = "leftEventLabelFail";
            leftEventLabel.Text = "Sessionen är inte aktiv";
            leftEventLabel.Visible = true;
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        //NameValueCollection formCollection = Request.Form;
        string username = loginUsername.Text;//formCollection["loginUsername"];
        string password = loginPassword.Text;//formCollection["loginPassword"];

        handleLogin(username, password);
        //if (sql.Login(username) == password)
        //{
        //    Server.Transfer("LoggedIN.aspx");
        //}
        //else
        //{
        //    leftEventLabel.CssClass = "leftEventLabelFail";
        //    leftEventLabel.Text = "*Felaktigt lösenord";
        //    leftEventLabel.Visible = true;
        //}
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
            leftEventLabel.Text = "*Felaktigt lösenord";
            leftEventLabel.Visible = true;
        }
    }

    protected void btnRegistration_Click(object sender, EventArgs e)
    {
        //Det går att hämta asp: taggarna på sidan direkt!
        string username = registrationUsername.Text;
        string password = registrationPassword.Text;
        string passwordRepeat = registrationPasswordRepeat.Text;
        string message = error.registration(username, password, passwordRepeat);
        string color = error.color;

        rightEventLabel.CssClass = color;
        rightEventLabel.Text = message;
        rightEventLabel.Visible = true;
    }

    protected void loginPassword_TextChanged(object sender, EventArgs e)
    {
    }

    protected void registrationPassword_TextChanged(object sender, EventArgs e)
    {
    }
}