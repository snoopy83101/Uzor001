<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/FinancePub.master" AutoEventWireup="true" CodeBehind="MemberAmountDetail.aspx.cs" Inherits="Manage.Finance.MemberAmountDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="MemberAmountDetail.css" rel="stylesheet" />
    <script src="MemberAmountDetail.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="div_t">
        交易流水

    </div>

    <div class="div_t2">
        在这里可以查询用户交易流水


        
    </div>
    <div class="div_s">

        <input id="txt_CreateTime1" type="text" value="" />~
              <input id="txt_CreateTime2" type="text" value="" />

        <input type="button" onclick="GetMemberDetailPageSetting(1)" value="查 询" />
    </div>
    <table class="t3">
        <thead>

            <tr>
                <th>更改前</th>
                <th>变动金额</th>
                <th>更改后</th>
                <th>时间</th>
                <th>类别</th>
                <th>电话</th>
                <th>姓名</th>
            </tr>

        </thead>
        <tbody id="tb_1"></tbody>
    </table>

    <%=Common.HtmlHelper.ZyPagerHtml("1") %>
</asp:Content>
