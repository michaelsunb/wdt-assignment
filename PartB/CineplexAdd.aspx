<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="CineplexAdd.aspx.cs" Inherits="PartB.CineplexAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>Cineplex Location: <asp:TextBox ID="location" runat="server"></asp:TextBox></div>
    <div>Short Description: <asp:TextBox ID="shortDescription" runat="server"></asp:TextBox></div>
    <div>Long Description: <asp:TextBox ID="longDescription" runat="server"></asp:TextBox></div>
    <div>Cineplex Image: <asp:TextBox ID="image" runat="server"></asp:TextBox></div>
    <div><asp:Button ID="submit" OnClick="Submit_Click" runat="server" Text="Submit" /> |
         <asp:Button ID="cancel" OnClick="Cancel_Click" runat="server" Text="Cancel" />
    </div>
    <div><pre><asp:Label ID="Label1" runat="server" Text=""></asp:Label></pre></div>
</asp:Content>
