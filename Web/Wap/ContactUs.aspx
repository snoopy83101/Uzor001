<%@ Page Title="" Language="C#" MasterPageFile="~/Wap/WapSitePub.Master" AutoEventWireup="true" CodeBehind="ContactUs.aspx.cs" Inherits="Web.Wap.ContactUs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="ContactUs.css" rel="stylesheet" />
            <script type="text/javascript" src="https://api.map.baidu.com/api?v=2.0&ak=QclQN2P69B0j0wsc3jEQ9hbxTNOFTq93&s=1"></script>
    <script src="ContactUs.js"></script>

        <script>

        <%=scStr%>

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <img src="image/Contact/banner.jpg" />
    <div style="background-color: #f7f7f7">

        <div class="div_line1">
            <a>联系我们</a>
        </div>

        <div class="div_lianxi">

            <a>
                <div class="div_q">

                    <img src="/image/Contact/add.png" />
                </div>
                <span id="sp_address" style="display: block; width: 160px; top: 120px; left: 20px">杭州市余杭区乔司镇鑫业路36号1栋4层西</span>
            </a>
            <a>
                <div class="div_q">

                    <img src="/image/Contact/weChat.png" />
                </div>

                <img src="/image/Contact/code.png" style="top: 70px; left: 50px; width: 100px; height: 100px;" />
            </a>

            <a id="a_tell" >
                <div class="div_q">

                    <img src="/image/Contact/tel.png" />
                </div>
                <span id="sp_Tell" style="display: block; width: 160px; top: 120px; left: 50px"></span>

            </a>
            <div class="clr"></div>
               
        </div>
  

                <div id="div_Map" style="width:500px;height:354px; margin:20px auto 20px auto; border:1px solid #ccc">


                   
                </div>
        <div style="height:20px;"></div>
  
    </div>

</asp:Content>
