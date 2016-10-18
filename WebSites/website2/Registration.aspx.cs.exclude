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
                    registrationError.Text = "Något gick super fel :(";
            }
            else
            {
                registrationError.Text = "*Lösenordet måste matcha";
                registrationError.Visible = true;
            }
        else
        {
            registrationError.Text = "*Användarnamnet är inte giltigt.";
            registrationError.Visible = true;
        }

        if(regSuccess)
            //Matar med en parameter till Default.aspx, RegSuccess = true
            Server.Transfer("Default.aspx?RegSuccess=true", true);
    }


}