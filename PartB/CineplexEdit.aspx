<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="CineplexEdit.aspx.cs" Inherits="PartB.CineplexEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div><asp:Button ID="remove" OnClick="Remove_Click" runat="server" Text="Remove" /></div>
    <div>Cineplex Location: <asp:TextBox ID="location" runat="server"></asp:TextBox></div>
    <div>Short Description: <asp:TextBox ID="shortDescription" runat="server"></asp:TextBox></div>
    <div>Long Description: <asp:TextBox ID="longDescription" runat="server"></asp:TextBox></div>
    <div>Cineplex Image: <asp:TextBox ID="cineplexImage" runat="server"></asp:TextBox></div>
    <div><asp:Button ID="submit" OnClick="Submit_Click" runat="server" Text="Submit" /> |
         <asp:Button ID="cancel" OnClick="Cancel_Click" runat="server" Text="Cancel" />
    </div>
    <div><pre><asp:Label ID="Label1" runat="server" Text=""></asp:Label></pre></div>
</asp:Content>
