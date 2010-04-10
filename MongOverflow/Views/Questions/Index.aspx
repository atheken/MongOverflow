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
        </div>
        <div class="questionBody">
            <%=item.PostBody %></div>
        <div>
            Answers: <%=item.Answers.Count %>&nbsp;&nbsp;&nbsp;&nbsp;
            Created: <%= Html.Encode(String.Format("{0:g}", item.CreationDate)) %>&nbsp;&nbsp;&nbsp;&nbsp;
            Last Edit: <%= Html.Encode(String.Format("{0:g}", item.LastEditDate)) %>&nbsp;&nbsp;&nbsp;&nbsp;
            Score: <%= Html.Encode(item.Score) %>&nbsp;&nbsp;&nbsp;&nbsp;
        </div>
    </div>
    <% } %>
</asp:Content>
