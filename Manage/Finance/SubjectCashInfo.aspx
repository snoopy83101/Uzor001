<%@ Page Title="" Language="C#" MasterPageFile="~/Finance/FinancePub.master" AutoEventWireup="true" CodeBehind="SubjectCashInfo.aspx.cs" Inherits="Manage.Finance.SubjectCashInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="SubjectCashInfo.css" rel="stylesheet" />
    <script src="SubjectCashInfo.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="div_t">
        提现申请列表

    </div>

    <div class="div_t2">
        在这里可以操作用户的提现申请
    </div>




    <table class="t4">

        <tbody>
            <tr>

                <th>申请状态:</th>
                <td>
                    <span id="sp_SubjectCashStatus"></span>

                </td>
            </tr>

             <tr>

                <th>提现金额:</th>
                <td>
                    <span id="sp_Amount"></span>

                </td>
            </tr>
            <tr>
                <th>用户账号:</th>
                <td>
                    <span id="sp_Member"></span>
                </td>
            </tr>
            <tr>
                <th>用户地址:</th>
                <td>
                    <span id="sp_MemberAddress"></span>
                </td>
            </tr>
        </tbody>
    </table>

    <div class="clr_10px"></div>
        <div class="div_t2">
        提现银行卡
    </div>

    <div id="div_BankCardList" class="div_BankCardList">

        <a>
            <div class="c">
                <img src="" id="img_BankImgUrl" />

            </div>
            <span><b>银行:</b>中国工商银行</span>
            <span><b>卡号:</b>6222001603100334329</span>
            <span><b>开户行:</b>凤起路支行</span>
            <span><b>持卡人:</b>王力</span>
            <div class="clr_20px"></div>
            <div class="c">
                <input type="button" class="" value="确认已转账" />
            </div>

        </a>



    </div>



    <table class="t4">

        <tbody>
            <tr>
                <th>备注</th>
                <td>
                    <input id="txt_Memo" class="" type="text" placeholder="" value="" /></td>
            </tr>
           
        </tbody>
    </table>






</asp:Content>
