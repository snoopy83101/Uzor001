<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/FinancePub.master" AutoEventWireup="true" CodeBehind="SubjectCashList.aspx.cs" Inherits="Manage.Finance.SubjectCashList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="SubjectCashList.css" rel="stylesheet" />
    <script src="SubjectCashList.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="div_t">
        提现申请列表

    </div>

    <div class="div_t2">
        在这里可以操作用户的提现申请


        
    </div>
    <div style="padding: 10px">
        <div class="div_s">

            <input id="txt_CreateTime1" type="text" value="" />~
              <input id="txt_CreateTime2" type="text" value="" />

            <input type="button" onclick="GetSubjectCashList(1)" value="查 询" />
        </div>
        <table class="t1">
            <thead>

                <tr>
                    <th>提现方式</th>
                    <th style="width: 120px">申请金额</th>



                    <th style="width: 80px">状态</th>
                    <th style="width: 200px">用户</th>
                    <th style="width: 100px">操作时间</th>

                </tr>
            </thead>
            <tbody id="t_1">
            </tbody>
        </table>
        <%=Common.HtmlHelper.ZyPagerHtml("1") %>
    </div>


</asp:Content>
