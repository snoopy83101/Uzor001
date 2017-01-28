<%@ Page Title="" Language="C#" MasterPageFile="~/Member/MemberPub.master" AutoEventWireup="true" CodeBehind="MemberList.aspx.cs" Inherits="Manage.Member.MemberList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<link type="text/css" rel="stylesheet" href="MemberList.css?v=2017011415" />
<script src="MemberList.js?v=2017011415"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="div_t">
        会员列表
    </div>
    <div class="div_t2">
        会员列表维护:
    </div>
    <div class="div_s">
        <div class="clr_10px"></div>
        编号:<input id="txt_MemberId" type="search" placeholder="会员编号" />
        已填手机:<input type="checkbox" checked="checked" id="cb_hasPhone" />
        <input id="txt_MemberPhone" type="search" placeholder="手机" />

        昵称:<input id="txt_MemberName" type="search" placeholder="姓名/昵称/备注" />
        <input type="button" value="查询" onclick="SearchMemberList(1)" />
    </div>
    <div class="clr_10px"></div>
    <table id="tb_memberList" class="t3">
        <thead>
            <tr>
                <th style="width: 150px">用户编号</th>
                <th style="width: 150px">手机号</th>

                <th style="width: 100px">真实姓名</th>
                <th style="width: auto">地址</th>

                <th style="width: 200px">技能等级</th>
                <th style="width: 100px">技能认证</th>
                <th style="width: 100px">实名认证</th>
                <th style="width: 100px; cursor: pointer;" onclick="ChangeOrder(this)" ziduan="LastTime" desc="asc">最后活跃时间</th>
            </tr>

        </thead>
        <tbody></tbody>
    </table>
    <%= Common.HtmlHelper.ZyPagerHtml("1") %>
</asp:Content>
