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
    <hr />
    <div><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/CineplexAdd.aspx">Add New Cineplex</asp:HyperLink></div>
    <div><asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/CineplexAddMovie.aspx">Add Movie to Cineplex</asp:HyperLink></div>
        <asp:GridView ID="CineplexGridView" 
        Font-Names="Arial" 
        Font-Size="0.75em" 
        CellPadding="4" 
        ForeColor="#333333"
        onrowdatabound="CineplexGridView_RowDataBound"
        runat="server" GridLines="None">
                
        <AlternatingRowStyle BackColor="White" />
        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#990000" ForeColor="white" Font-Bold="True" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <SortedAscendingCellStyle BackColor="#FDF5AC" />
        <SortedAscendingHeaderStyle BackColor="#4D0000" />
        <SortedDescendingCellStyle BackColor="#FCF6C0" />
        <SortedDescendingHeaderStyle BackColor="#820000" />
    </asp:GridView>
    <hr />
    <div><asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/MovieAdd.aspx">Add New Movie</asp:HyperLink></div>
    <asp:GridView ID="MovieGridView1" 
        Font-Names="Arial" 
        Font-Size="0.75em" 
        CellPadding="4" 
        ForeColor="#333333"
        onrowdatabound="MovieGridView_RowDataBound"
        runat="server" GridLines="None">
                
        <AlternatingRowStyle BackColor="White" />
        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#990000" ForeColor="white" Font-Bold="True" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <SortedAscendingCellStyle BackColor="#FDF5AC" />
        <SortedAscendingHeaderStyle BackColor="#4D0000" />
        <SortedDescendingCellStyle BackColor="#FCF6C0" />
        <SortedDescendingHeaderStyle BackColor="#820000" />
    </asp:GridView>
    <div><pre><asp:Label ID="Label1" runat="server" Text=""></asp:Label></pre></div>
</asp:Content>
