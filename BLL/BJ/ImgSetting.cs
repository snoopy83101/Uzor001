using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Common;
namespace BLL.BJ
{
    public class ImgSetting
    {


        /// <summary>
        /// 清除一张图片
        /// </summary>
        /// <param name="ImgId"></param>
        public static void ClearImg(string ImgId)
        {
            DAL.ImageInfoDAL dal = new DAL.ImageInfoDAL();
            string ImgUrl = DAL.DalComm.ExStr(" select ImgUrl from dbo.ImageInfo where ImgId='" + ImgId + "' ");
            dal.DeleteList(" ImgId='" + ImgId + "'  ");
            Common.FileString.FileDel(ImgUrl);
        }

        public static DataTable ImgArraySetting(DataTable dt, string html)
        {
            if (dt == null)
            {
                return null;
            }

            dt.Columns.Add("count");
            DataTable dt2 = dt.Clone();
            if (dt == null || dt.Rows.Count == -1)
            {
                return dt;
            }

            foreach (DataRow dr in dt.Rows)
            {
                int i = ImgHtmlSetting(dr["ImgId"].ToString(), html);

                if (i >= 0)  //0也可能是
                {
                    //如果是存在,则加入
                    dr["count"] = i;
                    dt2.Rows.Add(dr.ItemArray);

                }
                else
                { //如果没有存在则不加入


                }
            }
            if (dt2.Rows.Count > 0)
            {
                DataRow[] drs2 = dt2.Select("", " count asc ");
                DataTable dt3 = dt2.Clone();
                foreach (DataRow dr2 in drs2)
                {
                    dt3.Rows.Add(dr2.ItemArray);

                }
                dt.Dispose();
                dt2.Dispose();
                return dt3;
            }
            else
            {
                return null;
            }
        }



        /// <summary>
        /// 对比一张图片是否在HTML中,如果不在将其删除数据和文件
        /// </summary>
        /// <param name="ImgUrl"></param>
        /// <param name="html"></param>
        public static int ImgHtmlSetting(string ImgId, string html)
        {

            List<string> ImgFileNameList = Common.ImgHelper.GetImgsForHTML(html);
            string ImgUrl = "";

            for (int i = 0; i < ImgFileNameList.Count; i++)
            {

                string s = ImgFileNameList[i];
                string HtmlImgFileName = Common.FileString.GetFileName(s);
                ImgUrl = DAL.DalComm.ExStr(" select ImgUrl from dbo.ImageInfo where ImgId='" + ImgId + "' ");
                string ImgFileName = Common.FileString.GetFileName(ImgUrl);
                if (ImgFileName == HtmlImgFileName)
                {

                    return i;
                }
                else
                {
                    continue;
                }

            }


            //如果都不存在,清理这个文件!
            DAL.ImageInfoDAL dal = new DAL.ImageInfoDAL();
            dal.DeleteList(" ImgId='" + ImgId + "'  ");
            Common.FileString.FileDel(ImgUrl);
            return -1;


        }
    }
}
