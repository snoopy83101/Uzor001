<%@ Page Title="" Language="C#" MasterPageFile="~/AdManage/AdPub.master" AutoEventWireup="true" CodeBehind="AdList.aspx.cs" Inherits="Manage.Ad.AdList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link type="text/css" rel="stylesheet" href="AdList.css?v=2017011748" />
<script src="AdList.js?v=2017011748"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="div_t">
        广告维护
    </div>
    <div class="div_t2">
        在这里可以维护客户端广告内容
    </div>
    <div class="div_abstract">

        <p>
            当前此功能仍在总结和完善中

        </p>
    </div>



    <div style="padding:10px;">
            <div class="div_s">

        <input type="button" value=" 新 增 " onclick="PopSaveAd()" />  <input type="button" value="查 询" onclick="GetAdList()" />
    </div>
    <table class="t3">
        <thead>
            <tr>
                <th>标题</th>
                <th>位置</th>
                <th style="width: 100px">创建时间</th>
                 <th style="width: 100px">显示/作废</th>
            </tr>

        </thead>
        <tbody id="tb_ad">
        </tbody>
    </table>

        </div>
</asp:Content>
