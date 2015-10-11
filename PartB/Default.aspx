<%@ Page Title="" Language="C#" MasterPageFile="Admin.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PartB.Default" %>
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
            <asp:HyperLink ID="lnkLogin" runat="server" NavigateUrl="~/Login.aspx">Log In</asp:HyperLink>
        </AnonymousTemplate>
    </asp:LoginView>
    <div><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/CineplexAdd.aspx">Add New Cinema</asp:HyperLink></div>
    <div><asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/CineplexAddMovie.aspx">Add Movie to Cinema</asp:HyperLink></div>
    <div><asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/CineplexRemoveMovie.aspx">Remove Movie from Cinema</asp:HyperLink></div>
    <br />
    <div><asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/CineplexAdd.aspx">Add New Movie</asp:HyperLink></div>
    <div><asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/CineplexAddMovie.aspx">Edit Movie</asp:HyperLink></div>
    <div><asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/CineplexRemoveMovie.aspx">Remove Movie</asp:HyperLink></div>
</asp:Content>
