<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	CreateProduct
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>CreateProduct</h2>
	<% using (Html.BeginForm()) { %>
		<%= Html.AntiForgeryToken() %>
		<fieldset>
			<legend>New Product</legend>
			<label for="Name">Product Name:</label>
			<%= Html.TextBox("Name") %>
			<label for="Category">Category:</label>
			<%= Html.TextBox("Category") %>
			<input type="submit" name="Create" />
		</fieldset>
	<% } %>
</asp:Content>
