<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MongOverflow.Models.OverflowQuestion>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=this.Model.Title %>
    (<%=this.Model.Answers.Count %>
    answers)
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="question">
        <div class="questionHeader">
            <%= Html.Encode(Model.Title) %></div>
        Created:
        <%= Html.Encode(String.Format("{0:g}", Model.CreationDate)) %><br />
        Edited:
        <%= Html.Encode(String.Format("{0:g}", Model.LastEditDate)) %><br />
        Score:
        <%= Html.Encode(Model.Score) %><br /><br />
        <div class="questionBody">
            <%= Model.PostBody %></div>
    </div>
</asp:Content>
