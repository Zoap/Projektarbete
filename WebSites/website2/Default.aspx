<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
 
<!DOCTYPE html>
 
<html xmlns="http://www.w3.org/1999/xhtml" runat="server">
<head runat="server">
    <title>Welcome to Project Drop</title>
    <link rel="shortcut icon" href="Images/server_inst_eyes_open.ico" />
    <link rel="stylesheet" type="text/css" href="style.css" />
    <style type="text/css">
        .auto-style1 {
            text-align: left;
            width: 27px;
        }
        .auto-style2 {
            text-align: left;
            width: 30px;
        }
        #loginRegisterForm {
            width: 566px;
        }
    </style>
</head>
<body>
    <div id="Wrapper">
        <div id="Header">
            <p>Uploader</p>
        </div>
        <div id="loginContent">
            <form id="loginRegisterForm" runat="server">
                <div id="loginContentLeft">
                    <table class="loginTable">
                        <asp:Panel ID="LoginPanel" runat="server" DefaultButton="btnLogin">
                        <tr>
                            <td colspan="2" class="loginTableTop">Logga in!</td>
                        </tr>
                        <tr class="spacingFix"><td colspan="2"></td></tr>
                        <tr>
                            <td class="auto-style1">Username:</td>
                            <td>
                                <asp:TextBox ID="loginUsername" runat="server" placeholder="Username" BorderStyle="Solid"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">Password:</td>
                            <td>
                                <asp:TextBox ID="loginPassword" TextMode="password" runat="server" placeholder="Password" BorderStyle="Solid"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="right">
                                <asp:Button ID="btnLogin" OnClick="btnLogin_Click" runat="server" Text="Login" CssClass="loginFormButton"/>
                            </td>
                        </tr>
                        </asp:Panel>
                    </table>
                    <asp:Label ID="leftEventLabel" runat="server" Visible="false"/>
                </div>
 
                <div id="loginContentRight">
                    <table class="registrationTable">
                        <tr>
                            <td colspan="2" class="registrationTableTop">Registrera dig!</td>
                        </tr>
                        <tr class="spacingFix"><td colspan="2"></td></tr>
                        <tr>
                            <td class="auto-style2">Username:</td>
                            <td>
                                <asp:Panel ID="RegistrationPanel1" runat="server" DefaultButton="btnRegistration">
                                <asp:TextBox ID="registrationUsername" runat="server" placeholder="Username" BorderStyle="Solid"></asp:TextBox>
                                </asp:Panel>
                            </td>
                        </tr>
                         <tr>
                            <td class="auto-style2">E-mail:</td>
                            <td>
                                <asp:Panel ID="RegistrationPanel2" runat="server" DefaultButton="btnRegistration">
                                <asp:TextBox ID="registrationEmail" runat="server" placeholder="E-mail" BorderStyle="Solid"></asp:TextBox>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">Password:</td>
                            <td>
                                <asp:Panel ID="RegistrationPanel3" runat="server" DefaultButton="btnRegistration">
                                <asp:TextBox ID="registrationPassword" TextMode="password" runat="server" placeholder="Password" BorderStyle="Solid"></asp:TextBox>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">Repeat:</td>
                            <td>
                                <asp:Panel ID="RegistrationPanel4" runat="server" DefaultButton="btnRegistration">
                                <asp:TextBox ID="registrationPasswordRepeat" TextMode="password" runat="server" placeholder="Password(Repeat)" BorderStyle="Solid"></asp:TextBox>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="right">
                                <asp:Button ID="btnRegistration" OnClick="btnRegistration_Click" runat="server" Text="Registrera" CssClass="registrationFormButton"/>
                            </td>
                        </tr>
                    </table>
                    <asp:Label ID="rightEventLabel" runat="server" CssClass="rightEventLabel" Visible="false"/>
                </div>
            </form>
        </div>
    </div>
</body>
</html>
<footer class="footer">
    <style>
        a:link    {color:white; background-color:transparent; text-decoration:none}
        a:visited {color:white; background-color:transparent; text-decoration:none}
        a:hover {color:white; background-color:transparent; text-decoration:underline}
    </style>
    <p class="copyright"><strong><small>Copyright © 2016 ProjectDrop. Background Image By: <a href="http://www.designmastery.se">Björn Ed</a></small></strong></p>
</footer>