<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Andrew.Web.Canvas.Test._Default" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <%--    <asp:UpdatePanel ID="uppannel1" runat="server"><ContentTemplate>--%>
    <asp:TextBox ID="TextBox1" runat="server" Width="1303px"></asp:TextBox>
    <br />
    <asp:Button ID="Button1" runat="server" Text="Oauth" OnClick="Button1_Click" Width="120px" />
    <br />
    <asp:Button ID="Button2" runat="server" Text="Get Courses" OnClick="Button2_Click" />
    <br />
    <asp:TextBox ID="TextBox2" runat="server" Height="468px" Width="1288px"></asp:TextBox>
    <asp:Label ID="lblDisplay" runat="server" />
    <br />
    <asp:Panel ID="Panel1" runat="server"></asp:Panel>
    <%--</ContentTemplate></asp:UpdatePanel>--%>

    <hr /><asp:Label ID="lblMessage" runat="server" />
</asp:Content>
