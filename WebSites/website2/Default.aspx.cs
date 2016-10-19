﻿using System;
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
        string username = loginUsername.Text;
        string password = loginPassword.Text;

        handleLogin(username, password);
    }

    protected void btnRegistration_Click(object sender, EventArgs e)
    {
        //Det går att hämta asp: taggarna på sidan direkt!
        string username = registrationUsername.Text;
        string password = registrationPassword.Text;
        string passwordRepeat = registrationPasswordRepeat.Text;

        handleRegistration(username, password, passwordRepeat);
    }

    protected void loginPassword_TextChanged(object sender, EventArgs e)
    {
    }

    protected void registrationPassword_TextChanged(object sender, EventArgs e)
    {
    }

    private void handleLogin(string username, string password)
    {
        string message = error.login(username, password);

        //IF session active -> direkt till LoggedIN.aspx
        Session.Abandon();
        if (error.state)
        {
            //Skapar session
            Session["Username"] = username;
            Server.Transfer("LoggedIN.aspx", true);
        }
        else
        {
            leftEventLabel.CssClass = "leftEventLabelFail";
            leftEventLabel.Text = message;
            leftEventLabel.Visible = true;
        }
    }

    private void handleRegistration(string username, string password, string passwordRepeat)
    {
        string message = error.registration(username, password, passwordRepeat);
        string color = error.color;
        bool state = error.state;

        if (state)
        {
            sql.Register(username, password);
        }      
        rightEventLabel.CssClass = color;
        rightEventLabel.Text = message;
        rightEventLabel.Visible = true;
    }
}