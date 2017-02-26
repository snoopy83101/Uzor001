<%@ Page Title="" Language="C#" MasterPageFile="~/Member/MemberPub.master" AutoEventWireup="true" CodeBehind="MemberInfo.aspx.cs" Inherits="Manage.Member.MemberInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link type="text/css" rel="stylesheet" href="MemberInfo.css?v=20116121359" />
    <script src="MemberInfo.js?v=20161211359"></script>





</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="div_t">
        会员信息
    </div>
    <div class="div_t2">
        会员的详细信息
    </div>

    <dl class="dl_tab">
        <dt>

            <b tb="1">基本资料</b>
            <b tb="2">收支明细</b>
            <b tb="3">用户擅长</b>
            <b tb="4" id="b_tab_Team">所在团队</b>
            <b tb="5">他的工单</b>
            <b tb="6">银行卡</b>
            <b tb="7" onclick="GetMemberLogList(1)">用户日志</b>

        </dt>



        <dd style="padding: 10px;">
            <div tb="7">
                <div class="div_abstract">

                    <p>该用户的系统日志</p>
                </div>




                <table class="t3">
                    <thead>

                        <tr>
                            <th style="width: 200px">类型</th>
                            <th>标题</th>
                            <th style="width: 100px">操作人</th>
                            <th style="width: 200px">时间</th>
                        </tr>

                    </thead>
                    <tbody id="tb_MemberLogList">
                    </tbody>
                </table>

                <%=Common.HtmlHelper.ZyPagerHtml("MemberLog") %>
            </div>
            <div tb="6">
                <div class="div_abstract">

                    <p>双击可编辑, 双击加号可新增</p>
                </div>
                <div id="div_BankCard" class="div_BankCard">

                    <%--           <a>
                        <h3>6222001603100334329</h3>

                        <span>王力</span>
                        <span>中国工商银行</span>
<div class="clr"></div>
                        <span>xx路支行</span>
                    </a>
                      <a class="a_addBankCard">
          

                      </a>
                    <div class="clr"></div>--%>
                </div>





            </div>
            <div tb="5">


                <div class="div_abstract">

                    <p>右键可以查看工单所在的订单</p>
                </div>

                <table class="t3">
                    <thead>

                        <tr>
                            <th>所属订单编号</th>
                            <th>所属订单名称</th>



                            <th>分派</th>
                            <th>完成</th>
                            <th>质检</th>
                            <th>工价</th>
                            <th>总价</th>
                            <th>领取裁片日期</th>
                            <th>交货日期</th>
                            <th>工单状态</th>


                        </tr>

                    </thead>
                    <tbody id="tbody_OrderToWork">
                    </tbody>
                </table>

                <%=Common.HtmlHelper.ZyPagerHtml("OrderToWork") %>
            </div>

            <div tb="4">



                <table class="t3">
                    <thead>

                        <tr>

                            <th style="width: 50px">头像</th>
                            <th style="width: 100px;">姓名</th>
                            <th style="width: 150px">身份证号</th>
                            <th style="width: 140px">扫描件</th>

                            <th>手机号码</th>
                            <th>级别</th>
                            <th>认证情况</th>
                        </tr>

                    </thead>
                    <tbody id="tbody_Team">
                    </tbody>
                </table>


            </div>
            <div tb="3">
                <div id="div_Fabric" class="div_1">
                </div>


                <div id="div_Plate" class="div_1">
                </div>

            </div>
            <div tb="1">

                <table class="t4">

                    <tbody>
                        <tr>

                            <th>头像:
                            </th>
                            <td>
                                <img id="img_PicImg" class="img_PicImg" />

                            </td>
                        </tr>
                        <tr>

                            <th>姓名:
                            </th>
                            <td>
                                <input id="txt_RealName" type="text" placeholder="" value="" />

                            </td>
                        </tr>
                        <tr>

                            <th>性别:
                            </th>
                            <td>
                                <select id="sel_Sex">
                                    <option value="未知">未知</option>
                                    <option value="男">男</option>
                                    <option value="女">女</option>
                                </select>

                            </td>
                        </tr>
                        <tr>

                            <th>手机号码:
                            </th>
                            <td>
                                <input id="txt_Phone" type="text" placeholder="" value="" />

                            </td>
                        </tr>
                        <tr>
                            <th>生产档期:
                            </th>
                            <td>
                                <span id="sp_MaxOrderPlanningTime"></span>
                                <input type="button" value="刷 新" onclick="ReSetMaxOrderPlanningTime()" />
                            </td>


                        </tr>
                        <tr>

                            <th>级别认证:
                            </th>
                            <td>
                                <select id="sel_ProcessLv">
                                </select>

                                <%--                        <select id="sel_ProcessLvStatus" >

                            <option value="0" >未提交认证</option>
                            <option value="10" >请求认证</option>
                            <option value="20" >认证通过</option>
                            <option value="0" >未提交认证</option>
                            <option value="0" >未提交认证</option>
                            <option value="0" >未提交认证</option>
                        </select>--%>

                            </td>
                        </tr>

                        <tr>
                            <th>实名认证:</th>
                            <td>

                                <select id="sel_AuthenticationStatus" onchange="AuthenticationStatusChange(this)">

                                    <option value="0">未认证</option>
                                    <option value="10">待认证</option>
                                    <option value="20">通过认证</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <th>技能认证:</th>
                            <td>

                                <select id="sel_ProcessLvStatus" onchange="ProcessLvStatusChange(this)">

                                    <option value="0">未认证</option>
                                    <option value="10">待认证</option>
                                    <option value="20">通过认证</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <th>技能:</th>
                            <td>

                                <input type="button" value="更 改" onclick="SetSkill()" />
                                <div id="div_Skill" style="width: 300px; display: inline-block;">
                                </div>

                            </td>

                        </tr>
                        <tr>

                            <th>身份证号:
                            </th>
                            <td>
                                <input id="txt_SfzNo" type="text" placeholder="" value="" />
                                <a class="a_sfzImg">
                                    <img id="img_SfzImg1" class="img_sfz" src="../Images/none.png" alt="正面" onclick='OpenSfzImg(this)' title="单击图片" />

                                </a>

                                <a class="a_sfzImg">
                                    <img id="img_SfzImg2" class="img_sfz" src="../Images/none.png" alt="背面" onclick='OpenSfzImg(this)' title="单击图片" /></a>

                            </td>
                        </tr>
                        <tr>

                            <th>地 址:
                            </th>
                            <td>

                                <div id="div_ssx"></div>

                                <div class="clr_10px"></div>


                                <textarea id="txt_Address" style="width: 300px; height: 100px"></textarea>


                            </td>
                        </tr>


                        <tr>

                            <th>注册时间:
                            </th>
                            <td>
                                <span id="sp_RegistrationTime"></span>

                            </td>
                        </tr>
                        <tr>

                            <th>通过实名认证:
                            </th>
                            <td>
                                <span id="sp_AcceptAuthenticationTime"></span>

                            </td>
                        </tr>
                        <tr>

                            <th>通过技能认证:
                            </th>
                            <td>
                                <span id="sp_AcceptProcessTime"></span>

                            </td>
                        </tr>
                        <tr>

                            <th>备注:
                            </th>
                            <td>
                                <input id="txt_Memo" type="text" value="" placeholder="对该用户的备注, 可为空"   style="width:300px" />

                            </td>
                        </tr>
                    </tbody>

                </table>
                <div class="clr_10px">
                </div>

                <input type="button" value=" 保存信息 " style="margin-left: 110px;" onclick="SaveMember();" />
            </div>
            <div tb="2">
                <span>当前余额:<b id="b_Amount"></b></span>


                <table class="t3">
                    <thead>
                        <tr>

                            <th>明细类别</th>
                            <th>变动金额</th>
                            <th>变更前余额</th>
                            <th>变更后余额</th>
                            <th>变更时间</th>
                        </tr>

                    </thead>
                    <tbody class="tb_MemberAmountDetail" id="tb_MemberAmountDetail">
                    </tbody>

                </table>
                <%=Common.HtmlHelper.ZyPagerHtml("1") %>
            </div>
        </dd>
    </dl>


</asp:Content>

