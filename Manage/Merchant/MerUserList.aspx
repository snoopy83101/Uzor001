<%@ Page Title="" Language="C#" MasterPageFile="~/Merchant/MerPub.master" AutoEventWireup="true" CodeBehind="MerUserList.aspx.cs" Inherits="Manage.Merchant.MerUserList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="MerUserList.js"></script>
    <link href="MerUserList.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <table class="t4 tb_content ">

        <tbody>

            <tr>
                <td style="width: 100px">

                    <div>
                        <ul id="ul_userList">

                       
                        </ul>
                        <input type="button" value="添加用户" onclick="ToAddUser()" />
                    </div>
                </td>
                <td>
                    <table class="t4">
                        <tbody>


                            <tr>
                                <th>登录名</th>
                                <td>
                                    <input id="txt_UserId" type="text" value="" disabled="disabled" /></td>
                            </tr>

                            <tr>
                                <th>真实姓名</th>
                                <td>
                                    <input id="txt_RealName" type="text" value="" placeholder="建议填写真实姓名"  /></td>
                            </tr>
                            <tr>
                                <th>用户密码</th>
                                <td>
                                    <input id="txt_Pwd" type="password" value="" /></td>
                            </tr>
                            <tr>
                                <th>电子邮件</th>
                                <td>
                                    <input id="txt_Email" type="text" value="" /></td>
                            </tr>
                            <tr>
                                <th>QQ号码</th>
                                <td>
                                    <input id="txt_qq" type="text" value="" /></td>
                            </tr>
                            <tr>
                                <th>电话(座机)</th>
                                <td>
                                    <input id="txt_tell" type="text" value="" /></td>
                            </tr>
                            <tr>
                                <th>电话(手机)</th>
                                <td>
                                    <input id="txt_phone" type="text" value="" /></td>
                            </tr>
                            <tr style="display:none">
                                <th>是否绑定微信</th>
                                <td>
                                    <input id="txt_WxOpenID" type="text" value="" /></td>
                            </tr>
                            <tr>
                                    <th>用户备注</th>
                                <td>
                                    <input id="txt_Memo" type="text" value="" /></td>
                            </tr>
                            <tr>
                                <th>用户角色</th>
                                <td>
                                    <div>
                                        <input type="button" value="新增角色"  onclick="AddUserRole()"  />
                                       <ul id="ul_merRoleList" class="ul_merRoleList"></ul>
                                    </div>
                                </td>
                            </tr>
                        </tbody>


                    </table>
                    <div class="clr_10px"></div>
                    <input type="button" value="保 存" onclick="SaveUser()" />
                    <div class="clr_10px"></div>
                </td>

            </tr>
        </tbody>

    </table>
</asp:Content>
