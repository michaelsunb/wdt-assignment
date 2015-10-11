<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="MovieEdit.aspx.cs" Inherits="PartB.MovieEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>Movie Title: <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></div>
    <div>Short Description: <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></div>
    <div>Long Description: <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></div>
    <div>Price: <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox></div>
    <div>Movie Image: <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox></div>
    <div><asp:Button ID="Button1" runat="server" Text="Button" /></div>
</asp:Content>
