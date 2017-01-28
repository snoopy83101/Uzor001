<%@ Page Title="" Language="C#" MasterPageFile="~/WebSitePub.Master" AutoEventWireup="true" CodeBehind="Recruitment.aspx.cs" Inherits="Web.Recruitment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Recruitment.css" rel="stylesheet" />
    <script src="Recruitment.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div>
        <div style="background-color: #F7F7F7;">

            <div class="d1200">
                <a href="#div_down">
                <img src="image/Recruitment/banner.jpg"  />
                    </a>
            </div>
            <div class="d1200">

                <img src="image/Recruitment/banner2.jpg" />
            </div>

            <div class="clr_20px" style="height: 30px;"></div>
            <div class="div_line1" style="width: 1000px;">
                <a >接单流程</a>



            </div>
            <div class="d1200">
                <div class="div_liucheng">
                    <div class="clr_20px" style="height: 30px;"></div>

                    <img src="image/Recruitment/step.png" />

                </div>
            </div>


            <div class="d1200" id="div_down">
                <div class="div_liucheng">
                    <div class="clr_20px" style="height: 80px;"></div>

                    <img src="image/Recruitment/code.png" />
                    <div class="clr_20px"></div>

                    <a class="a_down" href="https://itunes.apple.com/cn/app/you-zuo-gong-ren-duan/id1186596338?mt=8" target="_blank">App store</a>
                    <a  class="a_down" href="http://android.myapp.com/myapp/detail.htm?apkName=com.chuanfou.uzjob" target="_blank" >Android</a>


                </div>
            </div>

            <div>
            </div>
            <div class="clr_20px" style="height: 30px;"></div>
        </div>
    </div>
</asp:Content>
