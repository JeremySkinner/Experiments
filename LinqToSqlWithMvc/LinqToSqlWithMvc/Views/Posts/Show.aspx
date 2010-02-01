<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LinqToSqlWithMvc.Models.Post>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Show
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<h2><%= Html.Encode(Model.Title) %></h2>
	
	<div>
		<%= Html.Encode(Model.Content) %>
	</div>
	<h3>Comments</h3>
	
	<% foreach(var comment in Model.Comments) { %>
		<p>Left by: <%= Html.Encode(comment.Commenter.UserName) %></p>
		<div>
			<%= Html.Encode(comment.CommentValue) %>
		</div>
		<hr />
	<% } %>
	
</asp:Content>
