<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<CreateProductModel>" %>
<%@ Import Namespace="AbstractAir.Example.UI.Area.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	CreateProduct
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>CreateProduct</h2>
    <% Html.EnableClientValidation(); %>
	<% using (Html.BeginForm()) { %>
		<%= Html.AntiForgeryToken() %>
		<%= Html.ValidationSummary() %>
		<fieldset>
			<legend>New Product</legend>
			<label for="Name">Product Name:</label>
			<%= Html.TextBoxFor(m => m.Name) %> <%= Html.ValidationMessageFor(m => m.Name) %>
			<label for="Category">Category:</label>
			<%= Html.TextBoxFor(model => model.Category) %> <%= Html.ValidationMessageFor(m => m.Category) %>
			<input type="submit" name="Create" />
		</fieldset>
	<% } %>
</asp:Content>
