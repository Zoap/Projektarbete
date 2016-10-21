<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
 
<!DOCTYPE html>
 
<html xmlns="http://www.w3.org/1999/xhtml" runat="server">
<head runat="server">
    <title>Uploader</title>
    <link rel="shortcut icon" href="server_inst_eyes_open.ico" />
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
                                <asp:TextBox ID="registrationUsername" runat="server" placeholder="Username" BorderStyle="Solid"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <td class="auto-style2">E-mail:</td>
                            <td>
                                <asp:TextBox ID="registrationEmail" runat="server" placeholder="E-mail" BorderStyle="Solid"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">Password:</td>
                            <td>
                                <asp:TextBox ID="registrationPassword" TextMode="password" runat="server" placeholder="Password" BorderStyle="Solid"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">Repeat:</td>
                            <td>
                                <asp:TextBox ID="registrationPasswordRepeat" TextMode="password" runat="server" placeholder="Password(Repeat)" BorderStyle="Solid"></asp:TextBox>
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