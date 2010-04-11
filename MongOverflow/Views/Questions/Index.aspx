<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<MongOverflow.Models.OverflowQuestion>>" %>

<asp:Content ID="titleHolder" ContentPlaceHolderID="TitleContent" runat="server">
    MongOverflow
</asp:Content>
<asp:Content ID="contentHolder" ContentPlaceHolderID="MainContent" runat="server">
    <% foreach (var item in Model)
       { %>
    <div class="question">
        <div class="questionHeader">
            <li class="questionScore score"><span style="font-size: .2em">Score: </span>
                <%= Html.Encode(item.Score) %></li>
            <%= Html.Encode(item.Title) %>
            <%=Html.ActionLink("View", "View", new { id = item._id }) %>
        </div>
        <div class="questionBody">
            <%=item.PostBody %></div>
        <hr />
        <div>
            Answers:
            <%=item.Answers.Count %>&nbsp;&nbsp;&nbsp;&nbsp; Created:
            <%= Html.Encode(String.Format("{0:g}", item.CreationDate)) %>&nbsp;&nbsp;&nbsp;&nbsp;
            <%= item.LastEditDate.HasValue ? Html.Encode(String.Format("Last Edit: {0:g}", item.LastEditDate)) : "" %>
        </div>
    </div>
    <% } %>
</asp:Content>
