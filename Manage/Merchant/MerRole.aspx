<%@ Page Title="" Language="C#" MasterPageFile="~/Merchant/MerPub.master" AutoEventWireup="true" CodeBehind="MerRole.aspx.cs" Inherits="Manage.Merchant.MerRole" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="MerRole.css" rel="stylesheet" />
    <script src="MerRole.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <table id="tb_power" class="t3">

        <thead>

            <tr>
                <th style="width: 200px">角色名称</th>
                <th>权限</th>
            </tr>

        </thead>

        <tbody>
            <tr>

                <td>
                    <div id="div_MerRole">
                        <input type="button" value="新增" onclick="PopMerRole()" />
                        <ul id="ul_MerRole" class="ul_MerRole">

                            <%=MerRoleListHtml %>
                        </ul>
                    </div>

                </td>
                <td>

                    <dl class="dl_tab">
                        <dt>
                            <b tb="1">Pc终端菜单</b>
                            <b tb="2">移动终端菜单</b>
                              <b tb="100" >消息监听</b>
                        </dt>
                        <dd>

                            <div tb="1">

                                       <div id="div_RoleMenu" class="div_RoleMenu">
                        <%=MenuListHtml %>
                    </div>

                            </div>
                            <div tb="2">
                                  <div id="div_AppRoleMenu" class="div_RoleMenu">
                                <%=AppMenuListHtml %>
                                                </div>
                            </div>

                            <div tb="100" >

                                <div id="div_MerRoleVsMsgType" class="div_MerRoleVsMsgType">
                                <%=MsgTypeHtml %>

                                </div>

                            </div>
                        </dd>
                    </dl>

             


                </td>
            </tr>


        </tbody>
    </table>
    <div class="clr_10px"></div>
    <input type="button" class="button5px" value="保存权限" onclick="SaveMenuPower()" />
</asp:Content>
