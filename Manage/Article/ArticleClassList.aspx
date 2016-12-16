<%@ Page Title="" Language="C#" MasterPageFile="~/Article/ArticlePub.master" AutoEventWireup="true" CodeBehind="ArticleClassList.aspx.cs" Inherits="Manage.Article.ArticleClassList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="ArticleClassList.css" rel="stylesheet" />
    <script src="ArticleClassList.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="div_r2">
        <div class="div_t">
            新闻类别维护
        </div>
        <div class="div_t2">
            当前的新闻类别列表:
        </div>
        <div class="div_alist">
<table id="tb_artClass" class="t1">
   <thead>
       <tr>

           <th class="auto-style1">类别标题</th><th class="auto-style1">备注</th><th class="auto-style1">子类别</th><th class="auto-style1">操作</th>
       </tr>
   </thead>
    <tbody>
        <tr>
            <td></td><td></td><td></td><td></td>
        </tr>

    </tbody>

</table>
            <div class="clr_10px"></div>
            <input type="button"  onclick="PopABindClass(this)" value="新 增"  class="" style="float:left" />


        </div>

        <div>
            <ul>
                <li></li>

            </ul>

        </div>
    </div>
</asp:Content>
