<%@ Page Title="" Language="C#" MasterPageFile="~/WebSitePub.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Web.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Index.css" rel="stylesheet" />
    <script src="/Index.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="background-color: #F7F7F7;">
        <div class="d1200" >
            <img src="/image/Index/banner.jpg" />
        </div>
        <div class="clr_20px" style="height:30px" ></div>
        <div class="div_fwyh d1200" >
      
            <div class="div_line1" style="width: 1000px;">
                <a>服务过用户</a>
            </div>

            <div class="div_logolist">
                <div class="clr_20px" style="height: 30px;"></div>

                <img src="/image/Index/partner.png" /><img src="/image/Index/partner1.png" /><img src="/image/Index/partner2.png" /><img src="/image/Index/partner3.png" /><img src="/image/Index/partner4.png" />
            </div>



        </div>
    </div>


    <div class="clr_20px"></div>
</asp:Content>
