<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SaveMerRole.aspx.cs" Inherits="Manage.Merchant.PopWindow.SaveMerRole" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

        <script src="../../Script/jquery-1.8.2.js"></script>
    <script src="../../Script/ZYUiPub.js"></script>
    <link href="../../Style/ZYUiPub.css" rel="stylesheet" />
    <script src="SaveMerRole.js"></script>

    <script>

        var MerRoleJson=<%=MerRoleJson%>;

    </script>
</head>
<body>
     <h3 style="text-align:center">角色维护</h3>
    <table class="t4">
        <tbody>
            <tr>
                <th>角色名</th>
                <td> <input id="txt_MerRoleName" type="text" name="name"  style="width:200px" value="" /></td>
            </tr>
                        <tr>
                <th>备注</th>
                <td >
                    <textarea id="txt_MerRoleMemo" style="width:200px;height:50px"></textarea></td>
            </tr>
                        <tr>
                <th>权限</th>
                <td> <input id="txt_Power" type="text" name="name" value="100" style="width:50px" /> </td>
            </tr>
                                <tr>
                <th></th>
                <td>  </td>
            </tr>
        </tbody>

    </table>
   <center>
       <input type="button" name="name" value="保存该角色" class="button5px" onclick="SaveMerRole()" />

   </center>
</body>
</html>
