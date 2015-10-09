<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="wdt_assignment._default" %>
<%@ Import Namespace="System.Web.Security" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<% if (Context.User.Identity.IsAuthenticated) { %>
  <%= Context.User.Identity.Name %>
<% } %>
            <asp:LoginView ID="LoginView1" runat="server">
            <LoggedInTemplate>
                Welcome back,
                <asp:LoginName ID="LoginName1" runat="server" />.
            </LoggedInTemplate>
            <AnonymousTemplate>
                Hello, stranger.
                <asp:HyperLink ID="lnkLogin" runat="server" NavigateUrl="~/Admin/Login.aspx">Log In</asp:HyperLink>
            </AnonymousTemplate>
        </asp:LoginView>
</asp:Content>
