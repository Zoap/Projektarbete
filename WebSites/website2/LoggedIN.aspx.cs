using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LoggedIN : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string test = (string)Session["Username"];
        if (String.IsNullOrEmpty((string)Session["Username"]))
            Server.Transfer("Default.aspx?SessionActive=false", true);
    }
}