using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Registration : System.Web.UI.Page
{
    private SQLHandler sql = new SQLHandler();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnRegistration_Click(object sender, EventArgs e)
    {
        NameValueCollection formCollection = Request.Form;
        string username = formCollection["registrationUsername"];
        string password = formCollection["registrationPassword"];

        sql.Register(username, password);

        Server.Transfer("Default.aspx", true);
    }


}