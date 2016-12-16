using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class SqlStrHelper
    {

        public static string BindImgSqlStr(string ImgId)
        {

            StringBuilder w = new StringBuilder();

            w.Append(" update CORE.dbo.ImageInfo set IsBind=1 where ImgId='" + ImgId + "' ");
            return w.ToString();

        }

        public static string BindCommentRep(decimal CommentId)
        {
            StringBuilder w = new StringBuilder();

            w.Append(" UPDATE dbo.Comment  SET RepCount =(SELECT COUNT(0) FROM dbo.Comment b WHERE ParentCommenId='" + CommentId + "') WHERE  dbo.Comment.CommentId='" + CommentId + "' ");
            return w.ToString();
        
        }

 
    }
}
