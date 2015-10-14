<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="CineplexAddMovie.aspx.cs" Inherits="PartB.CineplexAddMovie" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>Cineplex Location:
        <asp:DropDownList
            AutoPostBack="true"
            ID="cineplexLocation"
            runat="server"
            ></asp:DropDownList></div>
    <div>Movie Title:
        <asp:DropDownList AutoPostBack="false" ID="movieTitle" runat="server"></asp:DropDownList>
        <asp:Button ID="submit" OnClick="Add_Click" runat="server" Text="Add" /> |
        <asp:Button ID="cancel" OnClick="Back_Click" runat="server" Text="Back" />
    </div>
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
