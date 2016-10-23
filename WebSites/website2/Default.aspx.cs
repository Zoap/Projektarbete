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
    private SqlHandler sql = new SqlHandler();
    private ErrorHandling error = new ErrorHandling();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["SessionActive"] == "false")
        {
            leftEventLabel.CssClass = "leftEventLabelFail";
            leftEventLabel.Text = "Sessionen är inte aktiv";
            leftEventLabel.Visible = true;
        }

        loginUsername.Focus();
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string username = loginUsername.Text;
        string password = loginPassword.Text;

        HandleLogin(username, password);
    }

    protected void btnRegistration_Click(object sender, EventArgs e)
    {
        //Det går att hämta asp: taggarna på sidan direkt!
        string username = registrationUsername.Text;
        string email = registrationEmail.Text;
        string password = registrationPassword.Text;
        string passwordRepeat = registrationPasswordRepeat.Text;

        HandleRegistration(username, email, password, passwordRepeat);
    }

    private void HandleLogin(string userName, string password)
    {
        string message = error.login(userName, password);

        //IF session active -> direkt till LoggedIN.aspx
        Session.Abandon();
        if (error.State)
        {
            //Skapar session
            Session["Username"] = userName;
            Server.Transfer("LoggedIN.aspx", true);
        }
        else
        {
            registrationUsername.Text = string.Empty;
            leftEventLabel.Text = message;
            leftEventLabel.CssClass = error.Color;
            rightEventLabel.Visible = false;
            leftEventLabel.Visible = true;
        }
    }

    private void HandleRegistration(string userName, string email, string password, string passwordRepeat)
    {
        string message = error.Registration(userName, email,  password, passwordRepeat);

        if (error.State)
        {
            sql.Register(userName, email, password);
        }

        loginUsername.Text = string.Empty;
        rightEventLabel.Text = message;
        rightEventLabel.CssClass = error.Color;
        leftEventLabel.Visible = false;
        rightEventLabel.Visible = true;
    }
}