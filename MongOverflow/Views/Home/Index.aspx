<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<MongOverflow.Models.OverflowQuestion>>" %>
<asp:Content ID="titleHolder" ContentPlaceHolderID="TitleContent" runat="server">
	MongOverflow
</asp:Content>
<asp:Content ID="contentHolder" ContentPlaceHolderID="MainContent" runat="server">
<% foreach (var item in Model)
   { %>
<div class="question">
    <div class="questionHeader">
        <%= Html.Encode(item.Title) %>
        --
        <%= Html.Encode(String.Format("{0:g}", item.CreationDate)) %>
        --
        <%= Html.Encode(String.Format("{0:g}", item.LastEditDate)) %>
        --
        <%= Html.Encode(item.Score) %>
    </div>
    <div class="questionBody">
    <%=item.PostBody %></div></div>
<% } %>
</asp:Content>
