<%@ Page Title="" Language="C#" MasterPageFile="~/Merchant/MerPub.master" AutoEventWireup="true" CodeBehind="SaveMer.aspx.cs" Inherits="Manage.Merchant.SaveMer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="https://api.map.baidu.com/api?v=2.0&ak=QclQN2P69B0j0wsc3jEQ9hbxTNOFTq93&s=1"></script>
    <script src="/PubUI/ueditor/ueditor.all.min.js"></script>
    <script src="/PubUI/ueditor/ueditor.config.js"></script>
    <link href="SaveMer.css" rel="stylesheet" />
    <script src="SaveMer.js"></script>
    <script>

        var MerJson=<%=MerJson%>;
        var VsMerTypeList=<%=VsMerTypeList%>;
        var imgArray=<%=imgArray%>;

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <div class="div_t">平台基本信息</div>
    <div class="div_t2">
        以下信息将决定整个平台以及用户,请谨慎设置
        <input type="button" value="返 回" onclick="ToBack()" />
    </div>
    <div class="clr_10px"></div>
    <dl class="dl_tab">
        <dt>

            <b tb="1">平台信息</b><b tb="2">分部列表</b>

        </dt>
        <dd>

            <div tb="1">
                <table class="t4">

                    <tbody>
                        <tr>

                            <th class="auto-style1">工厂编号:</th>
                            <td>
                                <input type="text" id="txt_MerchantId" />
                            </td>

                        </tr>
                        <tr>

                            <th class="auto-style1">工厂名称:</th>
                            <td>
                                <input type="text" id="txt_MerName" />
                            </td>

                        </tr>
                        <%--                  <tr>
                            <th class="auto-style1">LOGO照片:</th>
                            <td>
                                <input type="button" value="上传" id="btn_logo" />&nbsp;
                                <img id="img_1" style="width: 40px; height: 30px; margin: 0 auto; vertical-align: middle;" />
                            </td>


                        </tr>--%>




                        <tr>
                            <th class="auto-style1">详细地址:</th>
                            <td>
                                <input id="txt_Address" type="text" style="width: 300px" /></td>

                        </tr>
                        <tr>
                            <th>平台简介:</th>
                            <td>
                                <textarea id="txt_MerMemo" style="width: 380px; height: 50px; overflow: auto"></textarea></td>


                        </tr>


                    </tbody>
                </table>
                <table class="t4" style="width: 600px">

                    <tbody>

                        <tr>
                            <th class="auto-style1">联系人姓名:</th>
                            <td>
                                <input id="txt_Name" type="text" value="" /></td>
                            <th class="auto-style1">联系人手机:</th>
                            <td>
                                <input id="txt_phone" type="text" value="" /></td>
                        </tr>

                        <tr>
                            <th class="auto-style1">座机:</th>
                            <td>
                                <input id="txt_tell" type="text" value="" /></td>
                            <th class="auto-style1">联系人QQ:</th>
                            <td>
                                <input id="txt_qq" type="text" value="" /></td>

                        </tr>

                        <tr>
                            <th class="auto-style1">电子邮件:</th>
                            <td>
                                <input id="txt_Email" type="text" value="" /></td>
                            <th class="auto-style1">官方网站:</th>
                            <td>
                                <input id="txt_WebSite" type="text" value="" /></td>
                        </tr>


                        <tr>
                            <th>地图标记
                            </th>
                            <td>
                                <div id="div_Map" class="div_Map">
                                </div>

                            </td>

                        </tr>
                        <tr>
                            <th></th>
                            <td>
                                <div class="clr_10px"></div>
                                <input type="button" value="保存平台信息" class="button5px" onclick="SaveMerchant()" />
                                <div class="clr_20px"></div>

                            </td>

                        </tr>
                    </tbody>
                </table>

            </div>
            <div tb="2" style="padding: 10px">
                <div class="clr_10px"></div>
                <input type="button" value="添加分部" onclick="ToAddBranch()" />
                <div class="clr_10px"></div>
                <table class="t3">
                    <thead>
                        <tr>
                            <th style="width: 80px">编号</th>
                            <th>分部名称</th>
                            <th>分部简介</th>
                        </tr>

                    </thead>
                    <tbody id="tbody_Branch">
                    </tbody>
                </table>

            </div>
        </dd>
    </dl>


</asp:Content>
