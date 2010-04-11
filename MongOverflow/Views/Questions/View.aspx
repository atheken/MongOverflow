<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MongOverflow.Models.OverflowQuestion>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=this.Model.Title %>
    (<%=this.Model.Answers.Count %>
    answers)
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="question">
        <div class="questionHeader">
            <li class="questionScore score"><span style="font-size: .2em">Score: </span>
                <%= Html.Encode(Model.Score) %></li>
            <%= Html.Encode(Model.Title) %></div>
        <div class="questionBody">
            <%= Model.PostBody %>
            <br />
            <br />
            Created:
            <%= Html.Encode(String.Format("{0:g}", Model.CreationDate)) %><br />
            <%=Model.LastEditDate.HasValue ? Html.Encode(String.Format("Edited: {0:g}", Model.LastEditDate)) : ""%><br />
        </div>
        <hr />
        <%if (!Model.Answers.Any()) { Response.Write("<br/><br/>No Answers Yet..."); }
          else
          {%>
        <%foreach (var a in Model.Answers)
          { %>
        <div class="answer">
            <div class="answerHeader">
                <li class="answerScore score"><span style="font-size: .5em">Score: </span>
                    <%=a.Score%></li>
                Answered @
                <%=String.Format("{0:g}",a.CreationDate) %><span style="float: right; padding:5px;"><%=Model.AcceptedAnswerId == a._id ? "<img src='"+this.ResolveUrl("~/Content/check.png")+"'/>" : "" %></span>
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
