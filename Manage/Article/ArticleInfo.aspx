<%@ Page Title="" Language="C#" MasterPageFile="~/Article/ArticlePub.master" AutoEventWireup="true" CodeBehind="ArticleInfo.aspx.cs" Inherits="Manage.Article.ArticleInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


        <script src="/PubUI/ueditor/ueditor.all.js"></script>
    <script src="/PubUI/ueditor/ueditor.config.js"></script>
        <script src="/PubUI/My97date/My97DatePicker/WdatePicker.js"></script>
        <script src="ArticleInfo.js"></script>
    <link href="ArticleInfo.css" rel="stylesheet" />
        <script>
        var ArticleJson=<%=ArticleJson%>;
        var imgArray=<%=imgArray%>;

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="div_r">
        <div class="div_t">新闻主体页面</div>
        <div class="div_t2">所在新闻的类别</div>

        <table class="t4">


            <tbody>
                <tr id="tr_ArticleId" style="display: none">
                    <th>文章ID:</th>
                    <td>
                        <input id="txt_ArticleId" style="width: 300px" type="text" disabled="disabled" />
                    </td>

                </tr>
                <tr>
                    <th>文章标题:</th>
                    <td>
                        <input id="txt_ArticleTitle" style="width: 300px" type="text" />
                        <input id="cb_invalid" type="checkbox" style="display: none" /></td>

                </tr>
                <tr>
                    <th>副标题:</th>
                    <td>
                        <input id="txt_ArticleSummary" type="text" style="width: 300px" /></td>

                </tr>
                <tr>
                    <th>文章类别:</th>
                    <td><span id="sp_class">
                        <select id="sel_ArticleClass" name="D1" onchange="BindChildArtClass(this)"><%=ArticleClassOpHtml %></select>
                    </span>
                        <input id="txt_ArticleSource" type="text" /></td>

                </tr>
                <tr>
                    <th>展示图片:

                    </th>
                    <td>&nbsp;<img id="img_atricleImg" style="width: 40px; height: 30px;"></img>
                        <img id="img_proImg" style="display: none; position: absolute; z-index: 9999" />


                    </td>

                </tr>
                <tr>
                    <th>创建日期:
                    </th>
                    <td>

                        <input id="txt_CreateTime" type="text" />
                    </td>

                </tr>
                <tr>
                    <th>作者姓名:
                    </th>
                    <td>
                        <input id="txt_Author" type="text" />

                    </td>

                </tr>
                <tr>
                    <th>备注:
                    </th>
                    <td>
                        <input id="txt_Memo" type="text" style="width: 300px" /></td>

                </tr>
                <tr>
                    <th>文章正文:</th>
                    <td>
                        <div style="width: 650px">
                            <!--style给定宽度可以影响编辑器的最终宽度-->
                            <script type="text/plain" id="txt_ArticleContent">
      
                            </script>
                            <script type="text/javascript">
                                var ed=  UE.getEditor('txt_ArticleContent', {
                                    //这里可以选择自己需要的工具按钮名称,此处仅选择如下五个
                                    //  toolbars: [['Source', 'Undo', 'Redo', 'Bold', 'test']],

                                    //关闭字数统计
                                    wordCount: false,
                                    //关闭elementPath
                                    elementPathEnabled: false,
                                    //默认的编辑区域高度
                                    initialFrameHeight: 550,
                                    toolbars: [
                                      [

             'source', 'insertimage', 'emotion', 'attachment', '|', 'justifyleft', 'justifycenter', 'justifyright', '|'

                                      ]
                                    ]
                                    //更多其他参数，请参考ueditor.config.js中的配置项
                                })
                            </script>
                        </div>
                    </td>

                </tr>
                <tr>
                    <th></th>
                    <td>
                        <div class="clr_20px"></div>
                        <input type="button" value="保存文章"  onclick="SaveArtice()" />
                        <div class="clr_20px"></div>
                    </td>
                </tr>

            </tbody>

        </table>


    </div>
</asp:Content>
