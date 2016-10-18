<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>test</title>
	<link rel="stylesheet" type="text/css" href="style.css">
</head>
<body>
	<div id="Wrapper">
		<div id="Header">
            <p>Title</p>
		</div>
		<div id="loginContent">
            <div id="loginContentLeft">
			    <form id="loginRegisterForm" runat="server">
			    <table class="loginTable">
				    <tr>
					    <td colspan="2" class="loginTableTop">Logga in!</td>
				    </tr>
                    <tr class="spacingFix"><td colspan="2"></td></tr>
				    <tr>
					    <td class="loginTableText">Användarnamn</td>
					    <td>
                            <asp:TextBox ID="loginUsername" runat="server"></asp:TextBox>
					    </td>
				    </tr>
				    <tr>
					    <td class="loginTableText">Lösenord</td>
					    <td>
                            <asp:TextBox ID="loginPassword" TextMode="password" runat="server"></asp:TextBox>
					    </td>
				    </tr>
                    <tr>
                        <td colspan="2" align="right">
                            <asp:Button ID="btnLogin" OnClick="btnLogin_Click" runat="server" Text="Login" CssClass="loginFormButton"/>
                        </td>
                    </tr>
			    </table>
                <asp:Label ID="registrationSuccess" runat="server" CssClass="registrationSuccess" Visible="false"/>
            </div>

            <div id="loginContentRight">
			    <table class="registrationTable">
				    <tr>
					    <td colspan="2" class="registrationTableTop">Registrera dig!</td>
				    </tr>
                    <tr class="spacingFix"><td colspan="2"></td></tr>
				    <tr>
					    <td class="registrationTableText">Användarnamn</td>
					    <td>
                            <asp:TextBox ID="registrationUsername" runat="server"></asp:TextBox>
					    </td>
				    </tr>
				    <tr>
					    <td class="registrationTableText">Lösenord</td>
					    <td>
                            <asp:TextBox ID="registrationPassword" TextMode="password" runat="server"></asp:TextBox>
					    </td>
				    </tr>
				    <tr>
					    <td class="registrationTableText">Repetera</td>
					    <td>
                            <asp:TextBox ID="registrationPasswordRepeat" TextMode="password" runat="server"></asp:TextBox>
					    </td>
				    </tr>
                    <tr>
                        <td colspan="2" align="right">
                            <asp:Button ID="btnRegistration" OnClick="btnRegistration_Click" runat="server" Text="Registrera" CssClass="registrationFormButton"/>
                        </td>
                    </tr>
			    </table>
			    </form>
                <asp:Label ID="registrationError" runat="server" CssClass="registationErrorLabel" Visible="false"/>
            </div>
		</div>
	</div>
</body>
</html>
