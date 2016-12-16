<%@ Page Title="" Language="C#" MasterPageFile="~/Comm/CommPub.master" AutoEventWireup="true" CodeBehind="SkillList.aspx.cs" Inherits="Manage.Comm.SkillList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="SkillList.css" rel="stylesheet" />
    <script src="SkillList.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="div_t">
        维护技能种类

    </div>

    <div class="div_t2">
        在这里可以维护技能种类(光标离开即是修改成功),右键可以作废,此配置关联到用户未选择或者已选择的技能种类,排序编号越大, 越靠前

        <select id="sel_Invalid" onchange="BindPageSetting()" >
            <option value="false">查看启用</option>
            <option value="true">查看作废</option>
        </select>


    </div>



    <table class="t3">
        <thead>

            <tr>
                <th style="width: 100px">技能编号</th>
                <th>技能名称</th>
                <th style="width: 100px">排序编号</th>
            </tr>

        </thead>
        <tbody id="tbody_list">
        </tbody>
    </table>



</asp:Content>
