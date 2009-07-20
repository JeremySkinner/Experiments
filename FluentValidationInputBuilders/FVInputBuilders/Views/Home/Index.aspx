<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SampleModel>" %>
<%@ Import Namespace="FVInputBuilders.Models"%>
<%@ Import Namespace="InputBuilder" %>
<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
 <h2>Html.Input( ) Sample</h2>
	<%= Html.ValidationSummary() %>
	
	<% using(Html.BeginForm("save")) { %>
		<%=Html.Input(c => c.Name)%>
        <%=Html.Input(c => c.TimeStamp)%>
        <%=Html.Input(c => c.Guid)%>
        <%=Html.Input(c => c.Enum).Required()%>
        <%=Html.Input(c => c.EnumAsRadioButton)%>
        <%=Html.Input(c => c.Html).Label("Label Overiden From the View")%>
        <%=Html.Input(c => c.IsNeeded)%>
        <%=Html.Input(c => c.IntegerRangeValue) %>
	<% } %>
	
</asp:Content>
