<%@ Page Title="" Language="C#" MasterPageFile="~/Order/OrderPub.master" AutoEventWireup="true" CodeBehind="OrderInfo.aspx.cs" Inherits="Manage.Order.OrderInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/PubUI/ueditor/ueditor.all.min.js"></script>
    <script src="/PubUI/ueditor/ueditor.config.js"></script>
<link type="text/css" rel="stylesheet" href="OrderInfo.css?v=20161221531" />
<script src="OrderInfo.js?v=20161221153"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="padding: 10px">
        <div class="div_OrderCount">

            <a class="a_zongshu">总数<span id="sp_OrderQuantity"></span>
            </a>
            <a class="a_shengchan">已派单数<span id="sp_WorkQuantity"></span>
            </a>
            <a class="a_Done">已生产数<span id="sp_DoneQuantity"></span>
            </a>
            <a class="a_zhijian">质检通过数<span id="sp_CheckQuantity"></span>
            </a>
            <a class="a_MemberNum">登记数<span id="sp_OrderVsMemberNum"></span>人
            </a>
            <a class="a_OrderToWorkNum">工单数<span id="sp_OrderToWorkNum"></span>个
            </a>

            <div class="clr"></div>
        </div>
        <dl class="dl_tab">
            <dt>
                <b tb="1" id="b_OrderInfo">订单信息</b>
                <%--<b tb="2">图文说明</b>--%>
                <%--    <b tb="3">数量明细</b>--%>
                <b tb="4">工单指派</b>
                <b tb="5" onclick="GetOrderLogList(1)">订单日志</b>
            </dt>
            <dd>


                <div tb="1">
                    <div class="div_abstract">

                        <p>
                            在这里新增或编辑外发订单主要信息
                        </p>
                    </div>
                    <div class="div_OrderInfo">
                        <table class="t4">

                            <tr style="display: none" id="tr_OrderId">

                                <th>订单编号</th>
                                <td><span id="sp_OrderId"></span></td>
                            </tr>

                            <tr>

                                <th>订单标题</th>
                                <td>
                                    <input id="txt_OrderTitle" type="text" placeholder="订单标题" class="i400" /></td>
                            </tr>
                            <tr>

                                <th>自定义编号</th>
                                <td>
                                    <input id="txt_OrderCode" type="text" placeholder="自定义订单编号" /></td>
                            </tr>
                            <tr style="display: ">

                                <th>客户款号</th>
                                <td>
                                    <input id="txt_ClientsCode" type="text" placeholder="客户款号" /></td>
                            </tr>
                            <tr>

                                <th>品质要求</th>
                                <td>

                                    <select id="sel_ProcessLv">
                                    </select>
                                </td>
                            </tr>
                            <tr>

                                <th>生产场地</th>
                                <td>

                                    <select id="sel_ProcessLocationType"></select>
                                </td>
                            </tr>

                            <tr>

                                <th>外发种类</th>
                                <td>

                                    <select id="sel_OrderClass">

                                        <option value="100">外发缝制订单</option>

                                    </select>
                                </td>
                            </tr>
                            <tr>

                                <th>联系人</th>
                                <td>
                                    <input type="text" placeholder="联系人" id="txt_OrderContacts" />
                                    <input type="text" placeholder="联系电话" id="txt_OrderTel" />
                                    (浏览器会自动保存上一次填写的内容)</td>
                            </tr>


                            <tr>

                                <th>联系地址</th>
                                <td>
                                    <input type="text" placeholder="联系地址" class="i400" id="txt_OrderAddress" />(浏览器会自动保存上一次填写的内容)</td>
                            </tr>


                            <tr>

                                <th>数量明细</th>
                                <td>



                                    <input type="button" value="增加颜色" onclick="ToSaveDetail()" />(更改数量可直接在表格的文本框中进行)
                                   

                                    <div class="div_DetailTable" id="div_DetailTable" style="">
                                    </div>
                                </td>
                            </tr>
                            <tr>

                                <th>单件工价</th>
                                <td>
                                    <input type="text" placeholder="单件工价(元)" id="txt_OrderWages" onkeyup="OnlyNumberForInput(this)" style="width: 70px" class="c" value="0.00" />(元)
                      
                                </td>
                            </tr>
                            <tr>
                                <th>计量单位</th>
                                <td>

                                    <select id="sel_Unit">
                                        <option value="件">件</option>
                                        <option value="套">套</option>
                                        <option value="条">条</option>
                                    </select>

                                </td>
                            </tr>
                            <tr>

                                <th>拆单/整单</th>
                                <td>

                                    <select id="sel_ReleaseType" onchange="ReleaseTypeChange(this)">
                                        <option value="10">拆单发布</option>
                                        <option value="20">整单发布</option>
                                    </select>

                                </td>
                            </tr>

                            <tr>



                                <th>实际人数</th>
                                <td>
                                    <input id="txt_Places" class="c" style="width: 50px" type="text" placeholder="抢单名额(人)" onkeyup="onlyNumber(this)" onchange="CountMinQuantity()" />(单位:人,发布抢单时,此数额会*2显示)</td>
                            </tr>
                            <tr id="tr_MinQuantity">
                                <th>领取数量</th>
                                <td>
                                    <span id="sp_MinQuantity"></span>

                                </td>
                            </tr>
                            <tr>
                                <th>预估工作量</th>
                                <td>
                                    <div id="div_OrderExpect">
                                    </div>
                                </td>
                            </tr>


                            <tr>

                                <th>领取裁片时间</th>
                                <td>
                                    <input id="txt_OrderReceivedTime" type="text" placeholder="领取裁片时间" onchange="CountPlanningTime2()" />

                                    <input class="btn_OrderReceivedTime" type="button" value="09:00" />
                                    <input class="btn_OrderReceivedTime" type="button" value="14:00" />
                                    <input class="btn_OrderReceivedTime" type="button" value="19:00"  />



                                </td>

                            </tr>
                            <tr>

                                <th>货期</th>
                                <td>
                                    <input type="text" id="txt_PlanningDay" onkeyup="onlyNumber(this)" onchange="CountPlanningTime2()" style="width: 50px" class="c" />(单位:天)
                             <%--       <input type="button" value="改变交货时间" onclick="CountPlanningTime2()" />--%>
                                </td>
                            </tr>


                            <tr>

                                <th>交货时间</th>
                                <td>

                                    <span id="sp_PlanningTime"></span>

                                    <%--   <input type="button" value="自动计算" onclick="CountPlanningTime()" />
                                    <span id="sp_PlanningDay"></span><span id="sp_PlanningTime"></span>--%>

                                    <%--   <input class="btn_PlanningTime"  type="button" value="9:00" /> <input class="btn_PlanningTime" type="button" value="14:00" /> <input class="btn_PlanningTime" type="button" value="19:00" />--%>

                                </td>
                            </tr>
                            <tr>
                                <th>订单图片</th>
                                <td>

                                    <div id="div_imgs" class="div_imgs">
                                    </div>

                                </td>
                            </tr>
                            <tr>
                                <th style="vertical-align: top;">详细信息</th>
                                <td>
                           
                                    <script type="text/plain" id="txt_OrderContent">
      
                                    </script>
                                    <script type="text/javascript">
                                        var ed = UE.getEditor('txt_OrderContent', {
                                            //这里可以选择自己需要的工具按钮名称,此处仅选择如下五个
                                            //  toolbars: [['Source', 'Undo', 'Redo', 'Bold', 'test']],

                                            //关闭字数统计
                                            wordCount: false,
                                            //关闭elementPath
                                            elementPathEnabled: false,
                                            pasteplain: false,
                                            //默认的编辑区域高度
                                            initialFrameHeight: 550,
                                            initialFrameWidth: 750,
                                            toolbars: [
                                           [

                  'source', 'insertimage', 'emotion', 'attachment', '|', 'justifyleft', 'justifycenter', 'justifyright', '|'

                                           ]
                                            ]
                                            //更多其他参数，请参考ueditor.config.js中的配置项
                                        });
                                    </script>
                                </td>



                            </tr>
                            <tr>
                                <th></th>
                                <td>
                                    <input type="button" value=" 保 存 " onclick="SaveOrderInfo()" />
                                    <input type="button" value="返回列表" onclick="tiaozhuan('OrderList.aspx')" />

                                    <%--  <input id="btn_ReleaseOrder" type="button" value=" 发 布 " onclick="ReleaseOrder()" style="display: none" />--%>

                                </td>
                            </tr>

                        </table>
                    </div>
                </div>



                <div tb="4">
                    <div class="div_abstract">


                        <p>
                            在这里将一个订单分割为多个工单,每个工单可以指派给提交抢单的用户或者主动指派给任何用户
                        </p>
                    </div>

                    <div class="div_OrderVsMember" id="div_OrderVsMember">




                        <%--  <a class="a_Member">
                            <span>昵称:<b>疯掉的陌陌</b></span>
                            <span>手机号:<b>18678158567</b></span>
                            <span>红色:<b>5</b>件</span>
                            <span>红色:<b>5</b>件</span>
                            <span>合计:<b>5</b>件</span>
                        </a>
                        <a class="a_Member">
                            <span>昵称:<b>疯掉的陌陌</b></span>
                            <span>手机号:<b>18678158567</b></span>
                            <span>红色:<b>5</b>件</span>
                            <span>红色:<b>5</b>件</span>
                            <span>合计:<b>5</b>件</span>
                        </a>
                        <a class="a_Member">
                            <span>昵称:<b>疯掉的陌陌</b></span>
                            <span>手机号:<b>18678158567</b></span>
                            <span>红色:<b>5</b>件</span>
                            <span>红色:<b>5</b>件</span>
                            <span>合计:<b>5</b>件</span>
                        </a>--%>

                        <a class="a_Member add"></a>
                        <div class="clr_10px"></div>

                    </div>
                    <div class="clr_20px"></div>
                </div>
                <div tb="5">
                    <div style="margin: 10px">
                        <table class="t1">
                            <thead>
                                <tr>

                                    <th>标题</th>
                                    <th style="width: 120px;">日志类别</th>
                                    <th style="width: 120px;">用户</th>
                                    <th style="width: 120px;">操作员</th>
                                    <th style="width: 120px;">时间</th>
                                </tr>

                            </thead>
                            <tbody id="tb_OrderLog">

                                <tr>
                                    <td></td>

                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <%=Common.HtmlHelper.ZyPagerHtml("2") %>
                </div>
            </dd>
        </dl>

    </div>
</asp:Content>
