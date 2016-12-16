/*----------------------------------------------------------------
   // Copyright (C) 2010 山东众阳软件公司
   // 版权所有。 
   //
   // 文件名：HtmlHelper.cs
   // 文件功能描述：通用的HTML处理静态对象
   // 类HtmlHelper.cs
   //
   // 
   // 创建标识： 王力 2012年2月19日
   // 修改标识： 
   // 修改内容： 
   //

----------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;

namespace Common
{
    public class HtmlHelper
    {


        public static void ConvertHtmlPage(string ServerPageUrl, string HtmlPageUrl)
        {
            System.IO.StringWriter swHtml = new System.IO.StringWriter();

            HttpContext.Current.Server.Execute(ServerPageUrl,swHtml,true);
            string contentHtml = swHtml.ToString();
            string filePath = HttpContext.Current.Server.MapPath(HtmlPageUrl);
            System.IO.StreamWriter sWrite = new System.IO.StreamWriter(filePath, false, System.Text.Encoding.UTF8);
            sWrite.Write(contentHtml);
            sWrite.Flush();
            sWrite.Close();

        }

        //protected override void Render()
        //{
        //    //System.IO.StringWriter html = new System.IO.StringWriter();
        //    //System.Web.UI.HtmlTextWriter tw = new System.Web.UI.HtmlTextWriter(html);

        //    //sw = new System.IO.StreamWriter(Server.MapPath("Default.htm"), false, System.Text.Encoding.Default);
        //    //sw.Write(html.ToString());
        //    //sw.Close();
        //    //tw.Close();

        //}   





        #region 快捷输出HTML元素

        public static string LoadImg200px()
        {

            return "/images/Load200px.gif";
        }

        public static string ImgTickCircleHtml2(string HtmlModel, string onclickName)
        {
            StringBuilder w = new StringBuilder();
            w.Append("<a  onclick='" + onclickName + "'  " + HtmlModel + " class='EditImgTextButton'>");
            w.Append("<img src= '/webcomm/images/tick_circle.png' />");

            w.Append("</a>");
            return w.ToString();
        }

        /// <summary>
        /// 输出圆形对号图标
        /// </summary>
        /// <param name="HtmlModel"></param>
        /// <param name="onclickName"></param>
        /// <returns></returns>
        public static string ImgTickCircleHtml(string HtmlModel, string onclickName)
        {
            StringBuilder w = new StringBuilder();
            w.Append("<a  onclick='" + onclickName + "'  " + HtmlModel + " class='EditImgTextButton'>");
            w.Append("<img src= '/webcomm/images/tick_circle.png' />");
            w.Append("编辑");
            w.Append("</a>");
            return w.ToString();
        }
        /// <summary>
        /// 输出黄色三角叹号图标
        /// </summary>
        /// <param name="HtmlModel"></param>
        /// <param name="onclickName"></param>
        /// <returns></returns>
        public static string ImgExclamationHtml(string HtmlModel, string onclickName)
        {
            StringBuilder w = new StringBuilder();
            w.Append("<a  onclick='" + onclickName + "'  " + HtmlModel + " class='EditImgTextButton'>");
            w.Append("<img src= '/webcomm/images/exclamation.png' />");
            w.Append("编辑");
            w.Append("</a>");
            return w.ToString();
        }
        /// <summary>
        /// 输出红色圆形错号图标
        /// </summary>
        /// <param name="HtmlModel"></param>
        /// <param name="onclickName"></param>
        /// <returns></returns>
        public static string ImgCrossCircleHtml(string HtmlModel, string onclickName)
        {
            StringBuilder w = new StringBuilder();
            w.Append("<a  onclick='" + onclickName + "'  " + HtmlModel + " class='EditImgTextButton'>");
            w.Append("<img src= '/webcomm/images/cross_circle.png' />");
            w.Append("编辑");
            w.Append("</a>");
            return w.ToString();
        }


        /// <summary>
        /// 输出蓝色叹号提示图标
        /// </summary>
        /// <param name="HtmlModel"></param>
        /// <param name="onclickName"></param>
        /// <returns></returns>
        public static string ImgInformationHtml(string HtmlModel, string onclickName)
        {
            StringBuilder w = new StringBuilder();
            w.Append("<a  onclick='" + onclickName + "'  " + HtmlModel + " class='EditImgTextButton'>");
            w.Append("<img src= '/webcomm/images/information.png' />");
            w.Append("编辑");
            w.Append("</a>");
            return w.ToString();
        }

        /// <summary>
        /// 输出编辑图片按钮
        /// </summary>
        /// <param name="HtmlModel"></param>
        /// <returns></returns>
        public static string BtnEditHtml(string HtmlModel, object ValueString)
        {
            string strValue = Convert.ToString(ValueString);
            return "<input type='image' src='/WebComm/Images/btn_edit.jpg' " + HtmlModel + " value='" + ValueString + "' />";
        }


        /// <summary>
        /// 输出删除图片按钮
        /// </summary>
        /// <param name="HtmlModel"></param>
        /// <returns></returns>
        public static string BtnRemoveHtml(string HtmlModel, object ValueString)
        {
            string strValue = Convert.ToString(ValueString);
            return "<input type='image' src='/WebComm/Images/btn_del.jpg' " + HtmlModel + " value='" + ValueString + "' />";
        }


        /// <summary>
        /// 输出添加图片按钮
        /// </summary>
        /// <param name="HtmlModel"></param>
        /// <returns></returns>
        public static string BtnAddHtml(string HtmlModel, object ValueString)
        {
            string strValue = Convert.ToString(ValueString);
            return "<input type='image' src='/WebComm/Images/btn_add.jpg' " + HtmlModel + " value='" + ValueString + "' />";

        }


        /// <summary>
        /// 输出span文本
        /// </summary>
        /// <param name="HtmlModel"></param>
        /// <returns></returns>
        public static string SpanHtml(string HtmlModel, object ValueString)
        {
            string strValue = Convert.ToString(ValueString);
            return "<span  " + HtmlModel + " >" + ValueString + "</span>";
        }

        /// <summary>
        /// 输出文本框
        /// </summary>
        /// <param name="HtmlModel"></param>
        /// <returns></returns>
        public static string TextHtml(string HtmlModel, object ValueString)
        {
            string strValue = Convert.ToString(ValueString);
            return "<input type='text' " + HtmlModel + "  value='" + strValue + "'  /> ";
        }
        /// <summary>
        /// 输出复选框
        /// </summary>
        /// <param name="HtmlModel"></param>
        /// <returns></returns>
        public static string SelectCheckBoxHtml(string HtmlModel)
        {
            return "<input type='checkbox' " + HtmlModel + "  class='Cb_Sel' /> ";
        }

        /// <summary>
        /// 输出删除图片按钮
        /// </summary>
        /// <param name="HtmlModel"></param>
        /// <returns></returns>
        public static string ImgDelButtonHtml(string HtmlModel, string onclickName)
        {
            StringBuilder w = new StringBuilder();
            w.Append("<img onclick='" + onclickName + "'  " + HtmlModel + " src= '/webcomm/images/cross_circle.png' />");
            return w.ToString();
        }
        /// <summary>
        /// 输出图标+文字的删除按钮
        /// </summary>
        /// <param name="HtmlModel"></param>
        /// <param name="onclickName"></param>
        /// <returns></returns>
        public static string ImgTextDelButtonHtml(string HtmlModel, string onclickName)
        {
            StringBuilder w = new StringBuilder();
            w.Append("<a  onclick='" + onclickName + "'  " + HtmlModel + " class='DelImgTextButton'>");
            w.Append("<img src= '/webcomm/images/cross.png' />");
            w.Append("删除");
            w.Append("</a>");
            return w.ToString();
        }
        /// <summary>
        /// 输出图标+文字的编辑按钮
        /// </summary>
        /// <param name="HtmlModel"></param>
        /// <param name="onclickName"></param>
        /// <returns></returns>
        public static string ImgTextEditButtonHtml(string HtmlModel, string onclickName)
        {
            StringBuilder w = new StringBuilder();
            w.Append("<a  onclick='" + onclickName + "'  " + HtmlModel + " class='EditImgTextButton'>");
            w.Append("<img src= '/webcomm/images/hammer_screwdriver.png' />");
            w.Append("编辑");
            w.Append("</a>");
            return w.ToString();
        }
        #endregion


        public static string ProTeXingSelHtml()
        {

            StringBuilder w = new StringBuilder();
            w.Append("<option value='1'>普通商品</option>");
            w.Append("<option value='2'>生鲜商品</option>");
            return w.ToString();
        }

        public static string AdList(DataTable dt)
        {
            StringBuilder w = new StringBuilder();
            if (dt.Rows.Count == 0)
            {
                return "";
            }
            foreach (DataRow dr in dt.Rows)
            {
                string wdith = (dr["w"].ToString());
                string height = (dr["h"].ToString());
                if (height == "0")
                {
                    height = "auto";
                }
                if (wdith == "0")
                {
                    wdith = "auto";
                }

                w.Append("<a id='a_ad_" + dr["adId"] + "' href='" + dr["url"] + "'  style=' width:" + wdith + "; height:" + height + "; float:left; display:block; ' >");
                w.Append("<img style=\" width:" + wdith + "px; height:" + height + "px;  \" src=\"" + dr["ImgUrl"] + "\" />");
                w.Append("</a>");
            }

            return w.ToString();

        }

        public static string WapPager(string PageId, string InputStr)
        {

            StringBuilder w = new StringBuilder();
            w.Append("<div class='showmore' PageId='" + PageId + "' style='margin-top:30px'>");

            w.Append("<a data-role='button' class='a_page_btn'  PageId='" + PageId + "'  page='1' >" + InputStr + "</a>");
            w.Append("</div>");
            return w.ToString();
        }

        public static string PageSrcitpHtml()
        {


            StringBuilder w = new StringBuilder();
            w.Append("<script src=\"/Scritpt/jquery-1.8.2.js\"></script>");
            w.Append("<script src=\"/Scritpt/ZYUiPub.js\"></script>");

            return w.ToString();
        }


        public static string ZyPagerHtml()
        {

            return ZyPagerHtml("p1", "btn");
        }
        public static string ZyPagerHtml(string PageId)
        {

            return ZyPagerHtml(PageId, "btn");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="PageId"></param>
        /// <param name="PageType">如果是url,则是换页分页</param>
        /// <returns></returns>
        public static string ZyPagerHtml(string PageId, string PageType)
        {

            StringBuilder w = new StringBuilder();
            w.Append("    <div id='" + PageId + "' class=\"manu\" PageType='" + PageType + "' >");
            w.Append("    <a id=\"page_prev_" + PageId + "\" >上一页</a><span id=\"span_pageMenu_" + PageId + "\"><a class=\"current\">1</a></span><a");
            w.Append("       id=\"page_next_" + PageId + "\" >下一页</a><a id=\"a_toPage_" + PageId + "\">转 到</a><input onkeypress=\"onlyNumber()\" style=\"width:30px\" id=\"txt_ToPage_" + PageId + "\"");
            w.Append("       type=\"text\"  />页,共 <span id=\"span_totalPage_" + PageId + "\"></span> 页");
            w.Append("   </div>");
            return w.ToString();
        }




        public static void DataTable2Excel(System.Data.DataTable dtData)
        {
            System.Web.UI.WebControls.DataGrid dgExport = null;
            // 当前对话   
            System.Web.HttpContext curContext = System.Web.HttpContext.Current;
            // IO用于导出并返回excel文件   
            System.IO.StringWriter strWriter = null;
            System.Web.UI.HtmlTextWriter htmlWriter = null;

            if (dtData != null)
            {
                // 设置编码和附件格式   
                curContext.Response.ContentType = "application/vnd.ms-excel";
                curContext.Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
                curContext.Response.Charset = "gb2312";

                // 导出excel文件   
                strWriter = new System.IO.StringWriter();
                htmlWriter = new System.Web.UI.HtmlTextWriter(strWriter);

                // 为了解决dgData中可能进行了分页的情况，需要重新定义一个无分页的DataGrid   
                dgExport = new System.Web.UI.WebControls.DataGrid();
                dgExport.DataSource = dtData.DefaultView;
                dgExport.AllowPaging = false;
                dgExport.DataBind();

                // 返回客户端   
                dgExport.RenderControl(htmlWriter);
                curContext.Response.Clear();
                curContext.Response.Write("<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset=gb2312\"/>" + strWriter.ToString());
                curContext.Response.End();
            }
        }


        /// <summary>
        /// 将DS导出成EXCEL
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="FileName">文件名称</param>
        public static void CreateExcel(DataSet ds, string FileName)
        {
            Page page = (Page)HttpContext.Current.Handler;
            HttpResponse resp;
            resp = page.Response;
            resp.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            resp.AppendHeader("Content-Disposition", "attachment;filename=" + FileName);
            string colHeaders = "", ls_item = "";

            //定义表对象与行对象，同时用DataSet对其值进行初始化 
            DataTable dt = ds.Tables[0];
            DataRow[] myRow = dt.Select();//可以类似dt.Select("id>10")之形式达到数据筛选目的
            int i = 0;
            int cl = dt.Columns.Count;

            //取得数据表各列标题，各标题之间以t分割，最后一个列标题后加回车符 
            for (i = 0; i < cl; i++)
            {
                if (i == (cl - 1))//最后一列，加n
                {
                    colHeaders += dt.Columns[i].Caption.ToString() + "n";
                }
                else
                {
                    colHeaders += dt.Columns[i].Caption.ToString() + "t";
                }

            }
            resp.Write(colHeaders);
            //向HTTP输出流中写入取得的数据信息 

            //逐行处理数据   
            foreach (DataRow row in myRow)
            {
                //当前行数据写入HTTP输出流，并且置空ls_item以便下行数据     
                for (i = 0; i < cl; i++)
                {
                    if (i == (cl - 1))//最后一列，加n
                    {
                        ls_item += row[i].ToString() + "n";
                    }
                    else
                    {
                        ls_item += row[i].ToString() + "t";
                    }

                }
                resp.Write(ls_item);
                ls_item = "";

            }
            resp.End();
        }

        public static string Test()
        {
            return "测试成功！";
        }


        #region 根据数据表生成Html table元素
        /** 2012-7-23 杨中华*/
        /// <summary>
        /// 根据数据表生成Html table元素
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static string CreateHtmlByDataTable(DataTable dataTable)
        {
            return CreateHtmlByDataTable(dataTable, null);
        }

        /// <summary>
        /// 根据数据表生成Html table元素
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="tbId">表格Id</param>
        /// <returns></returns>
        public static string CreateHtmlByDataTable(DataTable dataTable, string tbId)
        {
            return CreateHtmlByDataTable(dataTable, tbId, null, null, null, null);
        }

        /// <summary>
        /// 根据数据表生成Html table元素
        /// </summary>
        ///<param name="dataTable">数据表</param>
        ///<param name="tbId">table 的Id属性</param>
        ///<param name="tdClassName">table 单元格类名</param>
        ///<param name="thClassName">表头类名</param>
        ///<param name="trClassName">table行类名</param>
        ///<param name="tbClassName">table的类名</param>
        ///<returns></returns>
        public static string CreateHtmlByDataTable(DataTable dataTable, string tbId, string tbClassName, string thClassName, string trClassName, string tdClassName)
        {
            StringBuilder sbHtml = new StringBuilder();
            sbHtml.Append("<table ");
            //表格id
            if (!string.IsNullOrEmpty(tbId))
            {
                sbHtml.Append(" id=\"" + tbId + "\"");
            }
            //表格类名
            if (!string.IsNullOrEmpty(tbClassName))
            {
                sbHtml.Append("class=\"" + tbClassName + "\"");
            }
            sbHtml.Append(" width=\"100%\" border=\"1\">");


            int rowCount = dataTable.Rows.Count;
            if (rowCount <= 0)
            {
                return "暂无数据。";
            }
            int colums = dataTable.Columns.Count;
            DataRow rowHead = dataTable.Rows[0];
            //标题行
            sbHtml.Append("<tr>");
            for (int j = 0; j < colums; j++)
            {
                sbHtml.Append("<th ");
                if (!string.IsNullOrEmpty(thClassName))
                {
                    sbHtml.Append(" class=\"" + thClassName + "\" ");
                }
                sbHtml.Append("  >" + dataTable.Columns[j].ColumnName + "</th>");
            }
            sbHtml.Append("</tr>");
            //内容行
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                sbHtml.Append("<tr ");
                if (!string.IsNullOrEmpty(trClassName))
                {
                    sbHtml.Append(" class=\"" + trClassName + "\" ");
                }
                sbHtml.Append(" >");
                for (int j = 0; j < colums; j++)
                {
                    sbHtml.Append("<td ");
                    if (!string.IsNullOrEmpty(tdClassName))
                    {
                        sbHtml.Append(" class=\"" + tdClassName + "\" ");
                    }
                    //如果是数值类型右侧对其。
                    if (dataTable.Columns[j].DataType == typeof(int) || dataTable.Columns[j].DataType == typeof(decimal))
                    {
                        sbHtml.Append("  style=\"text-align:right;\"");
                    }
                    sbHtml.Append(" >");
                    sbHtml.Append(dataTable.Rows[i][j] + "</td>");
                }
                sbHtml.Append("</tr>");
            }

            sbHtml.Append("</table>");
            return sbHtml.ToString();
        }
        #endregion


        /// <summary>
        /// title字符串
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public static string TitleSetting(string title)
        {
            StringBuilder w = new StringBuilder();

            w.Append(title);
            w.Append(" - 众阳在线教育平台");

            return w.ToString();
        }

        public static MasterPage GetTopMasterPage(Page p)
        {

            return GetTopMasterPage(p.Master);
        }

        public static MasterPage GetTopMasterPage(MasterPage mp)
        {

            if (mp.Master == null)
            {
                return mp;
            }
            else
            {

                return GetTopMasterPage(mp);
            }
        }
    }
}
