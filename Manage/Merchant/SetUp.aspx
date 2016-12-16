<%@ Page Title="" Language="C#" MasterPageFile="~/Merchant/MerPub.master" AutoEventWireup="true" CodeBehind="SetUp.aspx.cs" Inherits="Manage.Merchant.SetUp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="SetUp.css" rel="stylesheet" />
    <script src="SetUp.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="div_t">
        设置

    </div>

    <div class="div_t2">
        设置.


        
    </div>
    <fieldset>
        <legend>消息监听</legend>
        <div class="div_abstract">

            <p>下面是我的所有角色可以允许监听消息类型的并集</p>

        </div>
        <div class="div_MerRoleVsMsgType" id="div_MsgType">
        </div>

    </fieldset>
</asp:Content>
