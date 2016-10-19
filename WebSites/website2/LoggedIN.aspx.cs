using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class LoggedIN : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Kollar om Username finns i den aktiva sessionen
        if (String.IsNullOrEmpty((string)Session["Username"]))
        {
            Server.Transfer("Default.aspx?SessionActive=false", true);
        }
    }
}