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

    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        NameValueCollection formCollection = Request.Form;
        string username = formCollection["loginUsername"];
        string password = formCollection["loginPassword"];

        handleLogin(username, password);
    }

    private void handleLogin(string username, string password)
    {
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