<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/FinancePub.master" AutoEventWireup="true" CodeBehind="WaitForPendingOrderToWorkList.aspx.cs" Inherits="Manage.Finance.WaitForPendingOrderToWorkList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="WaitForPendingOrderToWorkList.css" rel="stylesheet" />
    <script src="WaitForPendingOrderToWorkList.js?v=20161220425"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="div_t">
        工单结算
    </div>
    <div class="div_t2">
        在这里可以查询待结算或已结算的工单
    </div>
    <div class="div_s">

        <span>结算时间:</span>
        <input id="txt_PendingTime1" type="text" value="" />~
              <input id="txt_PendingTime2" type="text" value="" />

        <input type="button" onclick="GetWaitForPendingOrderToWorkList(1)" value="查 询" />
    </div>
    <table class="t3">
        <thead>

            <tr>
                <th>所属订单</th>
                <th style="width: 100px">客户款号</th>
                <th style="width: 100px">单价</th>
                <th style="width: 100px">质检数量</th>
                <th style="width: 100px">金额</th>
                <th style="width: 150px">结算日期(提醒)</th>
                <th style="width: 100px">当前状态</th>
                <th style="width: 150px">结算日期(实际)</th>
                <th style="width: 180px">联系方式</th>
            </tr>

        </thead>
        <tbody id="tbody_OrderToWork">
        </tbody>
    </table>

    <%=Common.HtmlHelper.ZyPagerHtml() %>
</asp:Content>
