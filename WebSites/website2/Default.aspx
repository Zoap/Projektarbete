<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
 
<!DOCTYPE html>
 
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Uploader</title>
    <link rel="shortcut icon" href="server_inst_eyes_open.ico" />
    <link rel="stylesheet" type="text/css" href="style.css" />
    <style type="text/css">
        .auto-style1 {
            text-align: right;
            width: 27px;
        }
        .auto-style2 {
            text-align: right;
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
                            <td class="auto-style1">Användarnamn</td>
                            <td>
                                <asp:TextBox ID="loginUsername" runat="server" BorderStyle="Solid"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">Lösenord</td>
                            <td>
                                <asp:TextBox ID="loginPassword" TextMode="password" runat="server" BorderStyle="Solid" OnTextChanged="loginPassword_TextChanged"></asp:TextBox>
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
                            <td class="auto-style2">Användarnamn</td>
                            <td>
                                <asp:TextBox ID="registrationUsername" runat="server" BorderStyle="Solid"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">Lösenord</td>
                            <td>
                                <asp:TextBox ID="registrationPassword" TextMode="password" runat="server" BorderStyle="Solid" OnTextChanged="registrationPassword_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">Repetera</td>
                            <td>
                                <asp:TextBox ID="registrationPasswordRepeat" TextMode="password" runat="server" BorderStyle="Solid"></asp:TextBox>
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