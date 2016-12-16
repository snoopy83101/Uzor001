<%@ Page Title="" Language="C#" MasterPageFile="~/Merchant/MerPub.master" AutoEventWireup="true" CodeBehind="AppUpdate.aspx.cs" Inherits="Manage.Merchant.AppUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="AppUpdate.css" rel="stylesheet" />
    <script src="AppUpdate.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="div_t">APP升级</div>
    <div class="div_t2">王力专用  </div>

    优做工人端:<input id="txt_Url" type="text" style="width: 500px;" />
    <input type="button" value="确 定" onclick="DownLoadApp()" />

    <div>
        <span id="sp_Url"></span>
    </div>
</asp:Content>
