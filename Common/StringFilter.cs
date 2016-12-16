using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common
{
    public class StringFilter
    {

        /// <summary>  

        /// 滤除script引用和区块  

        /// </summary>  

        /// <param name="str"></param>  

        /// <returns></returns>  

        public static string FilterScript(string str)
        {

            string pattern = @"<script[\s\S]+</script *>";

            return StripScriptAttributesFromTags(Regex.Replace(str, pattern, string.Empty, RegexOptions.IgnoreCase));

        }



        /// <summary>  

        /// 去除标签中的script属性  

        /// </summary>  

        /// <param name="str"></param>  

        /// <returns></returns>  

        private static string StripScriptAttributesFromTags(string str)
        {

            //\s 空白字符，包括换行符\n、回车符\r、制表符\t、垂直制表符\v、换页符\f  

            //\S \s的补集  

            //\w 单词字符，指大小写字母、0-9的数字、下划线  

            //\W \w的补集  



            //方法一：整体去除，不能去除不被单引号或双引号包含的属性值  

            //string pattern = @"on\w+=\s*(['""\s]?)([/s/S]*[^\1]*?)\1[\s]*";  

            //content = Regex.Replace(str, pattern, string.Empty, RegexOptions.Compiled | RegexOptions.IgnoreCase);  



            ////方法二：去除属性值  

            //string pattern = @"<\w+\s+(?<Attrs>[^>]*?)[>|/>]";  

            //Regex r = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);  

            //foreach (Match m in r.Matches(content))  

            //{  

            //    //获取标签的全部属性  

            //    string attrs = m.Groups["Attrs"].Value;  



            //    if (!string.IsNullOrEmpty(attrs))  

            //    {  

            //        //获取每一个属性  

            //        Regex rt = new Regex(@"(?<AttrName>\w+)\s*=(?<AttrPre>[\s]*(['""\s]?))(?<AttrVal>[^\1]*?)\1", RegexOptions.Compiled | RegexOptions.IgnoreCase);  

            //        foreach (Match mt in rt.Matches(attrs))  

            //        {  

            //            string attrName = mt.Groups["AttrName"].Value.Trim().ToLower();  

            //            string attrVal = mt.Groups["AttrVal"].Value.Trim().ToLower();  



            //            //匹配以on开头的属性  

            //            if (attrName.StartsWith("on") && !string.IsNullOrEmpty(attrVal))  

            //            {  

            //                //将属性值替换为空  

            //                str = str.Replace(attrVal, string.Empty);  

            //            }  

            //        }  

            //    }  

            //}  



            //整体去除  

            string pattern = @"(?<ScriptAttr>on\w+=\s*(['""\s]?)([/s/S]*[^\1]*?)\1)[\s|>|/>]";

            Regex r = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            foreach (Match m in r.Matches(str))
            {

                string attrs = m.Groups["ScriptAttr"].Value;

                if (!string.IsNullOrEmpty(attrs))
                {

                    str = str.Replace(attrs, string.Empty);

                }

            }



            //滤除包含script的href  

            str = FilterHrefScript(str);



            return str;

        }



        /// <summary>  

        /// 滤除包含script的href  

        /// </summary>  

        /// <param name="str"></param>  

        /// <returns></returns>  

        public static string FilterHrefScript(string str)
        {

            //整体去除，不能去除不被单引号或双引号包含的属性值  

            string regexstr = @" href[ ^=]*=\s*(['""\s]?)[\w]*script+?:([/s/S]*[^\1]*?)\1[\s]*";

            return Regex.Replace(str, regexstr, " ", RegexOptions.IgnoreCase);

        }



        /// <summary>  

        /// 滤除src  

        /// </summary>  

        /// <param name="str"></param>  

        /// <returns></returns>  

        public static string FilterSrc(string str)
        {

            //整体去除  

            string regexstr = @" src *=\s*(['""\s]?)[^\.]+\.(\w+)\1[\s]*";

            return Regex.Replace(str, regexstr, " ", RegexOptions.IgnoreCase);

        }



        /// <summary>  

        /// 滤除Html  

        /// </summary>  

        /// <param name="content"></param>  

        /// <returns></returns>  

        public static string FilterHtml(string str)
        {

            string[] aryReg ={  

              @"<style[\s\S]+</style>",  

              @"<.*?>",  

              @"<(.[^>]*)>",  

              @"([\r\n])[\s]+",  

              @"&(quot|#34);",  

              @"&(amp|#38);",  

              @"&(lt|#60);",  

              @"&(gt|#62);",  

              @"&(nbsp|#160);",  

              @"&(iexcl|#161);",  

              @"&(cent|#162);",  

              @"&(pound|#163);",  

              @"&(copy|#169);",  

              @"&#(\d+);",  

              @"-->",  

              @"<!--.*\n" 

            };



            string[] aryRep = {  

           "",  

           "",  

           "",  

           "",  

           "\"",  

           "&",  

           "<",  

           ">",  

           " ",  

           "\xa1",//chr(161),  

           "\xa2",//chr(162),  

           "\xa3",//chr(163),  

           "\xa9",//chr(169),  

           "",  

           "\r\n",  

           "" 

          };



            string strOutput = str;

            for (int i = 0; i < aryReg.Length; i++)
            {

                Regex regex = new Regex(aryReg[i], RegexOptions.IgnoreCase);

                strOutput = regex.Replace(strOutput, aryRep[i]);

            }



            strOutput = strOutput.Replace("<", "");

            strOutput = strOutput.Replace(">", "");

            strOutput = strOutput.Replace("\r\n", "");



            return strOutput;

        }



        /// <summary>  

        /// 过滤object  

        /// </summary>  

        /// <param name="content"></param>  

        /// <returns></returns>  

        public static string FilterObject(string content)
        {

            string regexstr = @"<object[\s\S]+</object *>";

            return Regex.Replace(content, regexstr, string.Empty, RegexOptions.IgnoreCase);

        }



        /// <summary>  

        /// 过滤Iframe  

        /// </summary>  

        /// <param name="content"></param>  

        /// <returns></returns>  

        public static string FilterIframe(string content)
        {

            string regexstr = @"<iframe[\s\S]+</iframe *>";

            return Regex.Replace(content, regexstr, string.Empty, RegexOptions.IgnoreCase);

        }



        /// <summary>  

        /// 过滤Frameset  

        /// </summary>  

        /// <param name="content"></param>  

        /// <returns></returns>  

        public static string FilterFrameset(string content)
        {

            string regexstr = @"<frameset[\s\S]+</frameset *>";

            return Regex.Replace(content, regexstr, string.Empty, RegexOptions.IgnoreCase);

        }



        /// <summary>  

        /// 过滤SQL注入  

        /// </summary>  

        /// <returns></returns>  

        public static string FilterSql(string str)
        {

            str = str.Replace("'", "''");

            str = str.Replace("<", "&lt;");

            str = str.Replace(">", "&gt;");



            return str;

        }



        /// <summary>  

        /// 移除非法或不友好字符  

        /// </summary>  

        /// <param name="keyWord">非法或不友好字符</param>  

        /// <param name="chkStr">要处理的字符串</param>  

        /// <returns>处理后的字符串</returns>  

        public static string FilterBadWords(string keyWord, string chkStr)
        {

            if (chkStr == "")
            {

                return "";

            }

            string[] bwords = keyWord.Split('|');

            int i, j;

            string str;

            StringBuilder sb = new StringBuilder();

            for (i = 0; i < bwords.Length; i++)
            {

                str = bwords[i].ToString().Trim();

                string regStr, toStr;

                regStr = str;

                Regex r = new Regex(regStr, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline);

                Match m = r.Match(chkStr);

                if (m.Success)
                {

                    j = m.Value.Length;

                    sb.Insert(0, "*", j);

                    toStr = sb.ToString();

                    chkStr = Regex.Replace(chkStr, regStr, toStr, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline);

                }

                sb.Remove(0, sb.Length);

            }

            return chkStr;

        }


        public static int StrToASCII(string character)
        {
            if (character.Length == 1)
            {
                System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                int intAsciiCode = (int)asciiEncoding.GetBytes(character)[0];
                return (intAsciiCode);
            }
            else
            {
                throw new Exception("Character is not valid.");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="asciiCode"></param>
        /// <returns></returns>
        public static string ASCIIToStr(int asciiCode)
        {
            if (asciiCode >= 0 && asciiCode <= 255)
            {
                System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                byte[] byteArray = new byte[] { (byte)asciiCode };
                string strCharacter = asciiEncoding.GetString(byteArray);
                return (strCharacter);
            }
            else
            {
                throw new Exception("ASCII Code is not valid.");
            }
        }


    }


}
