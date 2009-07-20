<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<HomeViewModel>" %>
<%@ Import Namespace="Web.Models"%>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%= Html.Encode(ViewData["Message"]) %></h2>
    <p>
        To learn more about ASP.NET MVC visit <a href="http://asp.net/mvc" title="ASP.NET MVC Website">http://asp.net/mvc</a>.
    </p>
    <% if(Model != null) { %>
		Id: <%= Model.Id %>
    <% } %>
    
    <% using(Html.BeginForm()) { %>
		Your Name: <input type="text" name="name" />
		<input type="submit"  value="Submit"/>
    
    <% } %>
    
</asp:Content>
