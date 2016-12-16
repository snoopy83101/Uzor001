<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ajaxTest.aspx.cs" Inherits="ajaxTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <script>
        var artJson=<%=artJson%>;
        alert(artJson.ArticleJson.ArticleId);
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
</body>
</html>
