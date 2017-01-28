<%@ Page Title="" Language="C#" MasterPageFile="~/WebSitePub.Master" AutoEventWireup="true" CodeBehind="ContactUs.aspx.cs" Inherits="Web.ContactUs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script type="text/javascript" src="https://api.map.baidu.com/api?v=2.0&ak=QclQN2P69B0j0wsc3jEQ9hbxTNOFTq93&s=1"></script>
    <script>

        <%=scStr%>

    </script>
    <link href="ContactUs.css" rel="stylesheet" />
    <script src="ContactUs.js"></script>
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="background-color: #F7F7F7;">

        <div class="d1200">

            <img src="image/Contact/banner.jpg" />
        </div>
                <div class="clr_20px" style="height:30px" ></div>
        <div class="d1200">
            <div class="div_line1" style="width: 1000px;">
                <a>联系我们</a>
                <div class="clr_20px" style="height: 10px;"></div>


            </div>
            <div class="div_lianxi">
                <div class="clr_20px" style="height: 100px;"></div>
                <a>
                    <div class="div_q">

                        <img src="image/Contact/add.png" />
                    </div>
                    <span id="sp_address"  style="display: block; width: 200px; top: 120px; left: 40px"></span>
                </a>
                <a>
                    <div class="div_q">

                        <img src="image/Contact/weChat.png" />
                    </div>

                    <img src="image/Contact/code.png" style="top: 90px; left: 70px; width: 140px; height: 140px;" />
                </a>
                <a>
                    <div class="div_q">

                        <img src="image/Contact/tel.png" />
                    </div>
                    <span id="sp_Tell" style="display: block; width: 200px; top: 120px; left: 40px"></span>

                </a>
  
            </div>

             <div class="clr_20px"></div>

                <div id="div_Map" style="width:1000px;height:354px; margin:auto; border:1px solid #ccc">


                   
                </div>
                        <div class="clr_20px"></div>
        </div>
    </div>
</asp:Content>
