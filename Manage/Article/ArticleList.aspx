<%@ Page Title="" Language="C#" MasterPageFile="~/Pub/Site1.Master" AutoEventWireup="true" CodeBehind="ArticleList.aspx.cs" Inherits="Manage.Article.ArticleList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="ArticleList.css" rel="stylesheet" />
    <script src="ArticleList.js"></script>
            <style>
        .div_ser
        {
            height:50px;
            line-height:50px;
        }
        .div_ser *
        {
            vertical-align:middle;

     
        }

        table.t1 tr
        {
            cursor:pointer;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="div_r2">
        <div class="div_t">
            新闻类别维护



        </div>
        <div class="div_t2">
            当前的新闻类别列表:

        </div>
        <div class="div_s div_ser">
            <input type="button" value="新 增" class="btn_3"  onclick="ToAddArticle()" /> 
           <span id="sp_class" ><select  id="sel_ArticleClassId"  onchange="BindChildArtClass(this)" >
                <option value="0">所有类别</option>
                 <%=ArticleClassOpHtml %>
            </select></span>
&nbsp; <input id="txt_ArticleTitle" type="text" /> 查询作废:<input type="checkbox" id="cb_invalid"  />  <input type="button" value="查 询"  onclick="Search(1)" />    新闻列表中可以右键操作

        </div>
        <div class="div_alist">

            <table  id="tb_list" class="t3">
                <thead>
                    <tr>
                        <th>文章标题</th>
                        <th>类别</th>
                        <th>发布日期</th>
                        <th>其他属性</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>

                        <td></td>

                    </tr>

                </tbody>


            </table>
            <%=Common.HtmlHelper.ZyPagerHtml("1") %>

        </div>

        <div>
            <ul>
                <li></li>

            </ul>

        </div>
    </div>
</asp:Content>
