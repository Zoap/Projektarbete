<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Registration.aspx.cs" Inherits="Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>test</title>
	<link rel="stylesheet" type="text/css" href="style.css" />
</head>
<body>
	<div id="Wrapper">
		<div id="Header">
            <p>Title</p>
		</div>
		<div id="Content">
			<form id="registrationForm" runat="server">
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
</body>
</html>