<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<AbstractAir.Example.Web.StandAlone.Models.ProductCreateEditModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Create Product
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Create Product</h2>
    <%using (Html.BeginForm("Create", "Product", FormMethod.Post))
      { %>
    <%= Html.EditorForModel() %>
    <input type="submit" value="Save" />
    <%} %>
</asp:Content>
