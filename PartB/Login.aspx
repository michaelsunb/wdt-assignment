<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PartB.Login" %>
<%@ Import Namespace="System.Web.Optimization" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <%-- Style --%>
    <%: Styles.Render("~/Admin/login") %>
    <title></title>
</head>
<body>
    <div class="container">
        <div class="row">
            <div class="col-sm-6 col-md-4 col-md-offset-4">
                <h1 class="text-center login-title">Sign in</h1>
                <div class="account-wall">
                    <form id="form1" runat="server" class="form-signin">
                        <asp:TextBox ID="Username" runat="server" placeholder="Email" class="form-control"></asp:TextBox>
                        <asp:TextBox ID="Password" runat="server" placeholder="Password" TextMode="password" class="form-control"></asp:TextBox>
                        <asp:Button ID="LoginSubmit" OnClick="EnterCredentials_Click" runat="server" Text="Sign in" class="btn btn-lg btn-primary btn-block" />
                        <asp:Label ID="LoginLabel" runat="server" Text=""></asp:Label>
                    </form>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
