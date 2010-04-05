<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<FluentValidationIoC.Models.Person>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<ul>
	<% foreach (var person in Model) { %>	
		<li>
			<%= Html.ActionLink(person.Name, "Edit", new{ id = person.Id }) %>
		</li>
	<% } %>
	</ul>
</asp:Content>
