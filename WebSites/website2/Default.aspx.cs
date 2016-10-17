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
        //Kollar om RegSucess är satt till True
        if (Request.QueryString["RegSuccess"] == "true")
        {
            registrationSuccess.Text = "Registration successfull!";
            registrationSuccess.Visible = true;
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
        else
        {
            Server.Transfer("Registration.aspx", true);
        }
    }
}