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
		<div id="Content">
			<form id="loginForm" runat="server">
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
			</form>
		</div>
	</div>
</body>
</html>
