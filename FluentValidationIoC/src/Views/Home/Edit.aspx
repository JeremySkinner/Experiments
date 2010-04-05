<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FluentValidationIoC.Models.Person>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit</h2>

	<%= Html.ValidationSummary() %>

	<% using(Html.BeginForm()) { %>
		<p>
			Name: <%= Html.TextBoxFor(x => x.Name) %>
		</p>
		<p>
			Date of Birth: <%= Html.TextBoxFor(x => x.DateOfBirth) %>
		</p>
		<p><input type="submit" value="Save" /></p>
	<% } %>
</asp:Content>
