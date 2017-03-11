<%@ Page Title="" Language="C#" MasterPageFile="~/Order/OrderPub.master" AutoEventWireup="true" CodeBehind="OrderList.aspx.cs" Inherits="Manage.Order.OrderList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="OrderList.css" rel="stylesheet" />
    <script src="OrderList.js?v=201611212129"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="div_t">
        外发订单列表

    </div>

    <div class="div_t2">
        在这里可以查询和操作维护所有外发订单


        
    </div>
    <div style="padding: 10px">

        <div class="div_s">
            <div class="div_Status" id="div_Status">
            </div>
            <div class="clr_20px"></div>
            <input type="button" value="新 增" onclick="ToOrderInfo()" />
            <input type="text" value="" placeholder="订单编号" id="txt_OrderId" />
            <input type="text" value="" placeholder="客户款号" id="txt_ClientsCode" />


           
            <input id="txt_CreateTime1" type="text" value="" />~
              <input id="txt_CreateTime2" type="text" value="" />

            <input type="button" onclick="GetOrderList(1)" value="查 询" />
        </div>
        <table class="t1">
            <thead>

                <tr>
                    <th style="width: 70px">缩略图</th>
                    <th>订单标题</th>
                    <th style="width: 80px">客户款号</th>
                    <th style='width:80px'>每天可赚</th>
                    <th style="width: 80px">登记人数</th>
                    <th style="width: 70px">数量</th>
                    <th style="width: 70px">已生产</th>
                    <th style="width: 70px">已质检</th>
                    <th style="width: 120px">品质要求</th>
                    <th style="width: 80px">单件工价</th>

                    <th style="width: 80px">状态</th>
                    <th style="width: 120px">领取裁片时间</th>
                    <th style="width: 120px">交货时间</th>
                    <th style="width: 100px">结算时间</th>
                </tr>
            </thead>
            <tbody id="t_1">
            </tbody>
        </table>
        <%=Common.HtmlHelper.ZyPagerHtml("1") %>
    </div>
</asp:Content>
