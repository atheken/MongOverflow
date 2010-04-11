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
        <%= Html.Encode(Model.Score) %><br />
        <br />
        <div class="questionBody">
            <%= Model.PostBody %>
            <hr />
            Answers:<%if (!Model.Answers.Any()) { Response.Write("<br/><br/>No Answers Yet..."); }
                      else
                      {%>
            <%foreach (var a in Model.Answers)
              { %>
            <div class="answer">
                <div class="answerHeader">
                    <span class="score">
                        <%=a.Score%></span>  <%=Model.AcceptedAnswerId == a._id ? "ACCEPTED" : "" %>
                    Answered @
                    <%=String.Format("{0:g}",a.CreationDate) %>
                </div>
                <div class="answerBody">
                    <%=a.PostBody %>
                </div>
            </div>
            <%}
                      }%>
        </div>
    </div>
</asp:Content>
