<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
	Home Page
</asp:Content>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
	<p>
		<%= Html.ActionLink("View Post 1", "Show", "Posts", new{ id = 1 }, null) %>
	</p>
	<p>
		<%= Html.ActionLink("Create a new post", "Create", "Posts", new{ id = 1 }, null) %>
	</p>
</asp:Content>
