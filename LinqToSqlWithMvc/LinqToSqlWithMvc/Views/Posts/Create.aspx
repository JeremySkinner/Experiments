<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LinqToSqlWithMvc.Models.Post>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create Post</h2>

	<% using(Html.BeginForm()) { %>
		Title: <%= Html.TextBox("Title", null, new{ maxlength = 50 }) %><br /><br />
		Content: <%= Html.TextBox("Content", null, new{ maxlength = 1000 }) %><br /><br />
		
		<input type="submit" value="Submit" />
	<% } %>
</asp:Content>
