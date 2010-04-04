<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Create
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Create Product</h2>
    <% using (Html.BeginForm("Create", "Example", FormMethod.Post))
       { %>
    Name:
    <%= Html.TextBox("Name") %>
    <input type="submit" value="Create" />
    <% } %>
</asp:Content>
