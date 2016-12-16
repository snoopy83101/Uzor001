<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArtClassInfo.aspx.cs" Inherits="Manage.Article.PopWindow.ArtClassInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../../Script/jquery-1.8.2.js"></script>
    <script src="../../Script/ZYUiPub.js"></script>

    <script src="ArtClassInfo.js"></script>
    <link href="../../Style/ZYUiPub.css" rel="stylesheet" />
    <script>
        var ParentArticleClassJson=<%=ParentArticleClassJson%>;
        var ArticleClassJson=<%=ArticleClassJson%>;
    </script>
    <style>

        td
        {

            vertical-align:top;
            text-align:left;
        }

    </style>
</head>
<body>
            <h4 id="h_t" ></h4>
    <table>

        <tr>

            <td>标题:</td>
            <td>
                <input id="txt_ArticleClassName" type="text" style="width:200px;" /></td>
        </tr>
        <tr>
            <td>简介:</td>
            <td>
                <textarea id="txt_ArticleClassMemo" style="height: 130px; width: 200px"></textarea>

            </td>
        </tr>

    </table>

</body>
</html>
