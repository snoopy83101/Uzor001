<%@ Page Title="" Language="C#" MasterPageFile="~/Merchant/MerPub.master" AutoEventWireup="true" CodeBehind="MerConfigList.aspx.cs" Inherits="Manage.Merchant.MerConfigList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="MerConfigList.css" rel="stylesheet" />
    <script src="MerConfigList.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="div_t">配置列表</div>
    <div class="div_t2">修改前请咨询(qq:19278765) <input type="button" value="新增配置" onclick ="PopSaveConfig()"  /> </div>
    <div>

        <table class="t3">
            <thead>
                <tr>
                    <th style="width:200px" >配置名称</th>
                    <th style="width:220px">值</th>
                    <th>备注</th>
                </tr>
            </thead>
            <tbody id="tbody_1">
              

            </tbody>

        </table>

    </div>
</asp:Content>
