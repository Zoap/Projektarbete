﻿using System;

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
        else if (Request.QueryString["Logout"] == "true")
        {
            leftEventLabel.Visible = false;
        }

        //Redirect om session finns
        //if(!string.IsNullOrEmpty(Session["Username"].ToString()))
        //    Server.Transfer("LoggedIN.aspx", true);

        loginUsername.Focus();
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        User login_user = new User(loginUsername.Text, loginPassword.Text);

        HandleLogin(login_user);
    }

    protected void btnRegistration_Click(object sender, EventArgs e)
    {
        //Det går att hämta asp: taggarna på sidan direkt!
        string username = registrationUsername.Text;
        string email = registrationEmail.Text;
        string password = registrationPassword.Text;
        string passwordRepeat = registrationPasswordRepeat.Text;

        User new_user = new User(username, password, email);
        
        HandleRegistration(new_user, password, passwordRepeat);
    }

    private void HandleLogin(User user)
    {
        error.Login(user);
        
        if (error.State)
        {
            //Skapar session & tömmer den gamla
            Session.Clear();
            Session["Username"] = user.Username;
            Response.Redirect("LoggedIN.aspx", true);
        }
        else
        {
            registrationUsername.Text = string.Empty;
            leftEventLabel.Text = error.Message;
            leftEventLabel.CssClass = error.Color;
            rightEventLabel.Visible = false;
            leftEventLabel.Visible = true;
        }
    }

    private void HandleRegistration(User user, string password, string passwordRepeat)
    {
        error.Registration(user, password, passwordRepeat);

        if (error.State)
        {
            sql.Register(user);
        }

        loginUsername.Text = string.Empty;
        rightEventLabel.Text = error.Message;
        rightEventLabel.CssClass = error.Color;
        leftEventLabel.Visible = false;
        rightEventLabel.Visible = true;
    }
}