using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection.Emit;
using System.Globalization;
using System.Threading;

namespace Common
{
    public class StringPlus
    {

        public static string WxSendStr(string SendContent)
        {
            SendContent = SendContent.Replace("<br/>", " \n ");
            SendContent = SendContent.Replace("<p>", "");
            SendContent = SendContent.Replace("</p>", " \n ");
            SendContent = SendContent.Replace("target=\"_self\"", " ");
            return SendContent;
        }

        public static string DanYinHao(string str)
        {
            str = str.Replace("'", "''");
            return str;
        }

        /// <summary>
        /// a字符串是否包含b字符串
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool IsBaoHan(string a, string b)
        {


            if (a.IndexOf(b) > -1)
            {

                //字符串A中包含字符串B
                return true;

            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 获得字符串中开始和结束字符串中间得值
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="s">开始</param>
        /// <param name="e">结束</param>
        /// <returns></returns>
        public static string GetMiddleValue(string str, string s, string e)
        {
            Regex rg = new Regex("(?<=(" + s + "))[.\\s\\S]*?(?=(" + e + "))", RegexOptions.Multiline | RegexOptions.Singleline);
            return rg.Match(str).Value;
        }



        /// <summary>
        /// 生成字符串列表类似于:  '2','3'  (用逗号分隔)
        /// </summary>
        /// <param name="ImgIdList"></param>
        /// <returns></returns>
        public static string StrArrayByList(List<string> ImgIdList)
        {

            string ImgIds = String.Join("','", ImgIdList);

            ImgIds = "'" + ImgIds + "'";
            return ImgIds;
        }

        /// <summary>
        /// 获得视频教育平台的的跨站地址
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetVideoUrl(string url)
        {

            string s = url.Replace("~/", "http://10.67.89.10:887/");
            return s;
        }

        /// <summary>
        /// 汉字转拼音缩写
        /// Code By
        /// 2004-11-30
        /// </summary>
        /// <param name="str">要转换的汉字字符串</param>
        /// <returns>拼音缩写</returns>
        public static string GetPYString(string str)
        {
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            TextInfo ti = ci.TextInfo;
            string tempStr = "";
            foreach (char c in str)
            {
                if ((int)c >= 33 && (int)c <= 126)
                {//字母和符号原样保留
                    tempStr += ti.ToTitleCase(c.ToString());
                }
                else
                {//累加拼音声母
                    tempStr += ti.ToTitleCase(GetPYChar(c.ToString()));
                }
            }
            return tempStr;
        }
        /// <summary>
        /// 取单个字符的拼音声母
        /// Code By
        /// 2004-11-30
        /// </summary>
        /// <param name="c">要转换的单个汉字</param>
        /// <returns>拼音声母</returns>
        public static string GetPYChar(string c)
        {
            byte[] array = new byte[2];
            array = System.Text.Encoding.Default.GetBytes(c);
            int i = (short)(array[0] - '\0') * 256 + ((short)(array[1] - '\0'));
            if (i < 0xB0A1) return "*";
            if (i < 0xB0C5) return "a";
            if (i < 0xB2C1) return "b";
            if (i < 0xB4EE) return "c";
            if (i < 0xB6EA) return "d";
            if (i < 0xB7A2) return "e";
            if (i < 0xB8C1) return "f";
            if (i < 0xB9FE) return "g";
            if (i < 0xBBF7) return "h";
            if (i < 0xBFA6) return "j";
            if (i < 0xC0AC) return "k";
            if (i < 0xC2E8) return "l";
            if (i < 0xC4C3) return "m";
            if (i < 0xC5B6) return "n";
            if (i < 0xC5BE) return "o";
            if (i < 0xC6DA) return "p";
            if (i < 0xC8BB) return "q";
            if (i < 0xC8F6) return "r";
            if (i < 0xCBFA) return "s";
            if (i < 0xCDDA) return "t";
            if (i < 0xCEF4) return "w";
            if (i < 0xD1B9) return "x";
            if (i < 0xD4D1) return "y";
            if (i < 0xD7FA) return "z";
            return "*";
        }


        /// <summary>
        /// 将数组字符串1，2，3  转换成  '1','2','3'
        /// </summary>
        /// <param name="ObjArrayStr"></param>
        /// <returns></returns>
        public static string ArrayStrDanYinHao(string ObjArrayStr)
        {
            string[] a = ObjArrayStr.Split(',');
            if (a.Length == 0)
            {
                return "";
            }
            List<string> codeArray = new List<string>();
            foreach (string i in a)
            {
                codeArray.Add("'" + i + "'");
            }
            return string.Join(",", codeArray);
        }

        public static List<string> GetStrArray(string str, char speater, bool toLower)
        {
            List<string> list = new List<string>();
            string[] ss = str.Split(speater);
            foreach (string s in ss)
            {
                if (!string.IsNullOrEmpty(s) && s != speater.ToString())
                {
                    string strVal = s;
                    if (toLower)
                    {
                        strVal = s.ToLower();
                    }
                    list.Add(strVal);
                }
            }
            return list;
        }
        public static string[] GetStrArray(string str)
        {
            return str.Split(new char[',']);
        }
        public static string GetArrayStr(List<string> list, string speater)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                if (i == list.Count - 1)
                {
                    sb.Append(list[i]);
                }
                else
                {
                    sb.Append(list[i]);
                    sb.Append(speater);
                }
            }
            return sb.ToString();
        }


        #region 删除最后一个字符之后的字符

        /// <summary>
        /// 删除最后结尾的一个逗号
        /// </summary>
        public static string DelLastComma(string str)
        {
            return str.Substring(0, str.LastIndexOf(","));
        }

        /// <summary>
        /// 删除最后结尾的指定字符后的字符
        /// </summary>
        public static string DelLastChar(string str, string strchar)
        {
            return str.Substring(0, str.LastIndexOf(strchar));
        }

        #endregion




        /// <summary>
        /// 转全角的函数(SBC case)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToSBC(string input)
        {
            //半角转全角：
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }

        /// <summary>
        ///  转半角的函数(SBC case)
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns></returns>
        public static string ToDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }

        public static List<string> GetSubStringList(string o_str, char sepeater)
        {
            List<string> list = new List<string>();
            string[] ss = o_str.Split(sepeater);
            foreach (string s in ss)
            {
                if (!string.IsNullOrEmpty(s) && s != sepeater.ToString())
                {
                    list.Add(s);
                }
            }
            return list;
        }


        #region 将字符串样式转换为纯字符串
        public static string GetCleanStyle(string StrList, string SplitString)
        {
            string RetrunValue = "";
            //如果为空，返回空值
            if (StrList == null)
            {
                RetrunValue = "";
            }
            else
            {
                //返回去掉分隔符
                string NewString = "";
                NewString = StrList.Replace(SplitString, "");
                RetrunValue = NewString;
            }
            return RetrunValue;
        }
        #endregion

        #region 将字符串转换为新样式
        public static string GetNewStyle(string StrList, string NewStyle, string SplitString, out string Error)
        {
            string ReturnValue = "";
            //如果输入空值，返回空，并给出错误提示
            if (StrList == null)
            {
                ReturnValue = "";
                Error = "请输入需要划分格式的字符串";
            }
            else
            {
                //检查传入的字符串长度和样式是否匹配,如果不匹配，则说明使用错误。给出错误信息并返回空值
                int strListLength = StrList.Length;
                int NewStyleLength = GetCleanStyle(NewStyle, SplitString).Length;
                if (strListLength != NewStyleLength)
                {
                    ReturnValue = "";
                    Error = "样式格式的长度与输入的字符长度不符，请重新输入";
                }
                else
                {
                    //检查新样式中分隔符的位置
                    string Lengstr = "";
                    for (int i = 0; i < NewStyle.Length; i++)
                    {
                        if (NewStyle.Substring(i, 1) == SplitString)
                        {
                            Lengstr = Lengstr + "," + i;
                        }
                    }
                    if (Lengstr != "")
                    {
                        Lengstr = Lengstr.Substring(1);
                    }
                    //将分隔符放在新样式中的位置
                    string[] str = Lengstr.Split(',');
                    foreach (string bb in str)
                    {
                        StrList = StrList.Insert(int.Parse(bb), SplitString);
                    }
                    //给出最后的结果
                    ReturnValue = StrList;
                    //因为是正常的输出，没有错误
                    Error = "";
                }
            }
            return ReturnValue;
        }
        #endregion

        /// <summary>
        /// 获得一个16位时间随机数
        /// </summary>
        /// <returns>返回随机数</returns>
        public static string GetDataRandom()
        {
            string strData = DateTime.Now.ToString();
            strData = strData.Replace(":", "");
            strData = strData.Replace("-", "");
            strData = strData.Replace(" ", "");
            Random r = new Random();
            strData = strData + r.Next(100000);
            return strData;
        }
        /// <summary>
        ///  获得某个字符串在另个字符串中出现的次数
        /// </summary>
        /// <param name="strOriginal">要处理的字符</param>
        /// <param name="strSymbol">符号</param>
        /// <returns>返回值</returns>
        public static int GetStrCount(string strOriginal, string strSymbol)
        {
            int count = 0;
            for (int i = 0; i < (strOriginal.Length - strSymbol.Length + 1); i++)
            {
                if (strOriginal.Substring(i, strSymbol.Length) == strSymbol)
                {
                    count = count + 1;
                }
            }
            return count;
        }
        /// <summary>
        /// 获得某个字符串在另个字符串第一次出现时前面所有字符,大小写通用,全部转为小写!
        /// </summary>
        /// <param name="strOriginal">要处理的字符</param>
        /// <param name="strSymbol">符号</param>
        /// <returns>返回值</returns>
        public static string GetFirstStr(string strOriginal, string strSymbol)
        {
            //strOriginal = strOriginal.ToLower();
            //strSymbol = strSymbol.ToLower();
            int strPlace = strOriginal.IndexOf(strSymbol);
            if (strPlace != -1)
                strOriginal = strOriginal.Substring(0, strPlace);
            return strOriginal;
        }
        /// <summary>
        /// 获得某个字符串在另个字符串最后一次出现时后面所有字符
        /// </summary>
        /// <param name="strOriginal">要处理的字符</param>
        /// <param name="strSymbol">符号</param>
        /// <returns>返回值</returns>
        public static string GetLastStr(string strOriginal, string strSymbol)
        {

            try
            {
                if (GetStrCount(strOriginal, strSymbol) == 0)
                {
                    return strOriginal;
                }
                int strPlace = strOriginal.LastIndexOf(strSymbol) + strSymbol.Length;
                strOriginal = strOriginal.Substring(strPlace);
                return strOriginal;

            }
            catch
            {
                return strOriginal;
            }
        }
        /// <summary>
        /// 获得两个字符之间第一次出现时前面所有字符
        /// </summary>
        /// <param name="strOriginal">要处理的字符</param>
        /// <param name="strFirst">最前哪个字符</param>
        /// <param name="strLast">最后哪个字符</param>
        /// <returns>返回值</returns>
        public static string GetTwoMiddleFirstStr(string strOriginal, string strFirst, string strLast)
        {
            strOriginal = GetFirstStr(strOriginal, strLast);
            strOriginal = GetLastStr(strOriginal, strFirst);
            return strOriginal;
        }
        /// <summary>
        ///  获得两个字符之间最后一次出现时的所有字符
        /// </summary>
        /// <param name="strOriginal">要处理的字符</param>
        /// <param name="strFirst">最前哪个字符</param>
        /// <param name="strLast">最后哪个字符</param>
        /// <returns>返回值</returns>
        public static string GetTwoMiddleLastStr(string strOriginal, string strFirst, string strLast)
        {
            strOriginal = GetLastStr(strOriginal, strFirst);
            strOriginal = GetFirstStr(strOriginal, strLast);
            return strOriginal;
        }



        /// <summary>
        /// 从数据库表读记录时,能正常显示
        /// </summary>
        /// <param name="strContent">要处理的字符</param>
        /// <returns>返回正常值</returns>
        public static string GetHtmlFormat(string strContent)
        {
            strContent = strContent.Trim();

            if (strContent == null)
            {
                return "";
            }
            strContent = strContent.Replace("<", "&lt;");
            strContent = strContent.Replace(">", "&gt;");
            strContent = strContent.Replace("\n", "<br />");
            //strContent=strContent.Replace("\r","<br>");
            return (strContent);
        }
        /// <summary>
        /// 检查相等之后，获得字符串
        /// </summary>
        /// <param name="str">字符串1</param>
        /// <param name="checkStr">字符串2</param>
        /// <param name="reStr">相等之后要返回的字符串</param>
        /// <returns>返回字符串</returns>
        public static string GetCheckStr(string str, string checkStr, string reStr)
        {
            if (str == checkStr)
            {
                return reStr;
            }
            return "";
        }
        /// <summary>
        /// 检查相等之后，获得字符串
        /// </summary>
        /// <param name="str">数值1</param>
        /// <param name="checkStr">数值2</param>
        /// <param name="reStr">相等之后要返回的字符串</param>
        /// <returns>返回字符串</returns>
        public static string GetCheckStr(int str, int checkStr, string reStr)
        {
            if (str == checkStr)
            {
                return reStr;
            }
            return "";
        }
        /// <summary>
        /// 检查相等之后，获得字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="checkStr"></param>
        /// <param name="reStr"></param>
        /// <returns></returns>
        public static string GetCheckStr(bool str, bool checkStr, string reStr)
        {
            if (str == checkStr)
            {
                return reStr;
            }
            return "";
        }
        /// <summary>
        /// 检查相等之后，获得字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="checkStr"></param>
        /// <param name="reStr"></param>
        /// <returns></returns>
        public static string GetCheckStr(object str, object checkStr, string reStr)
        {
            if (str == checkStr)
            {
                return reStr;
            }
            return "";
        }
        /// <summary>
        /// 截取左边规定字数字符串
        /// </summary>
        /// <param name="str">需截取字符串</param>
        /// <param name="length">截取字数</param>
        /// <param name="endStr">超过字数，结束字符串，如"..."</param>
        /// <returns>返回截取字符串</returns>
        public static string GetLeftStr(string str, int length, string endStr)
        {
            if (str.Trim() == "")
            {

                return str + endStr;
            }

            string reStr;
            if (length < str.Length)
            {
                reStr = str.Substring(0, length) + endStr;
            }
            else
            {
                reStr = str;
            }
            return reStr;
        }


        /// <summary>
        /// 截取字符串, 从第一次出现时往后的length位
        /// </summary>
        /// <param name="str"></param>
        /// <param name="str2"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetLeftStr(string str, string str2, int length)
        {
            try
            {
                str = Regex.Split(str, str2)[1];


                return GetLeftStr(str, length, "");
            }
            catch (Exception)
            {

                return "";
            }

        }


        public static string GetMidStr(string Str, string leftStr, string rightStr)
        {

            string MidStr = Regex.Split(Str, leftStr)[1];

            MidStr = Regex.Split(MidStr, rightStr)[0];

            return MidStr;



        }




        /// <summary>
        /// 截取左边规定字数字符串
        /// </summary>
        /// <param name="str">需截取字符串</param>
        /// <param name="length">截取字数</param>
        /// <returns>返回截取字符串</returns>
        public static string GetLeftStr(string str, int length)
        {
            string reStr;
            if (length < str.Length)
            {
                reStr = str.Substring(0, length) + "...";
            }
            else
            {
                reStr = str;
            }
            return reStr;
        }
        /// <summary>
        /// 获得双字节字符串的字节数
        /// </summary>
        /// <param name="str">要检测的字符串</param>
        /// <returns>返回字节数</returns>
        public static int GetStrLength(string str)
        {
            ASCIIEncoding n = new ASCIIEncoding();
            byte[] b = n.GetBytes(str);
            int l = 0;  // l 为字符串之实际长度
            for (int i = 0; i < b.Length; i++)
            {
                if (b[i] == 63)  //判断是否为汉字或全脚符号
                {
                    l++;
                }
                l++;
            }
            return l;
        }
        ///// <summary>
        ///// 剥去HTML标签
        ///// </summary>
        ///// <param name="text"></param>
        ///// <returns></returns>
        //public static string RegStripHtml(string text)
        //{
        //    string reStr;
        //    string RePattern = @"<\s*(\S+)(\s[^>]*)?>";
        //    reStr = Regex.Replace(text, RePattern, string.Empty, RegexOptions.Compiled);
        //    reStr = Regex.Replace(reStr, @"\s+", string.Empty, RegexOptions.Compiled);
        //    return reStr;
        //}
        /// <summary>
        /// 转换HTML与相对去处相对标签 未测试
        /// </summary>
        /// <param name="text"></param>
        /// <param name="ReLabel"></param>
        /// <returns></returns>
        public static string RegStripHtml(string text, string[] ReLabel)
        {
            string reStr = text;
            string LabelPattern = @"<({0})\s*(\S+)(\s[^>]*)?>[\s\S]*<\s*\/\1\s*>";
            string RePattern = @"<\s*(\S+)(\s[^>]*)?>";
            for (int i = 0; i < ReLabel.Length; i++)
            {
                reStr = Regex.Replace(reStr, string.Format(LabelPattern, ReLabel[i]), string.Empty, RegexOptions.IgnoreCase);
            }
            reStr = Regex.Replace(reStr, RePattern, string.Empty, RegexOptions.Compiled);
            reStr = Regex.Replace(reStr, @"\s+", string.Empty, RegexOptions.Compiled);
            return reStr;
        }
        /// <summary>
        /// 使Html失效,以文本显示
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <returns>失效后字符串</returns>
        //public static string ReplaceHtml(string str)
        //{
        //    str = str.Replace("<", "&lt");
        //    return str;
        //}
        /// <summary>
        /// 获得随机数字
        /// </summary>
        /// <param name="Length">随机数字的长度</param>
        /// <returns>返回长度为 Length 的　<see cref="System.Int32"/> 类型的随机数</returns>
        /// <example>
        /// Length 不能大于9,以下为示例演示了如何调用 GetRandomNext：<br />
        /// <code>
        ///  int le = GetRandomNext(8);
        /// </code>
        /// </example>
        public static int GetRandomNext(int Length)
        {
            if (Length > 9)
                throw new System.IndexOutOfRangeException("Length的长度不能大于10");
            Guid gu = Guid.NewGuid();
            string str = "";
            for (int i = 0; i < gu.ToString().Length; i++)
            {
                if (isNumber(gu.ToString()[i]))
                {
                    str += ((gu.ToString()[i]));
                }
            }
            int guid = int.Parse(str.Replace("-", "").Substring(0, Length));
            if (!guid.ToString().Length.Equals(Length))
                guid = GetRandomNext(Length);
            return guid;
        }
        /// <summary>
        /// 返回一个 bool 值，指明提供的值是不是整数
        /// </summary>
        /// <param name="obj">要判断的值</param>
        /// <returns>true[是整数]false[不是整数]</returns>
        /// <remarks>
        ///  isNumber　只能判断正(负)整数，如果 obj 为小数则返回 false;
        /// </remarks>
        /// <example>
        /// 下面的示例演示了判断 obj 是不是整数：<br />
        /// <code>
        ///  bool flag;
        ///  flag = isNumber("200");
        /// </code>
        /// </example>
        public static bool isNumber(object obj)
        {
            //为指定的正则表达式初始化并编译 Regex 类的实例
            System.Text.RegularExpressions.Regex rg = new System.Text.RegularExpressions.Regex(@"^-?(\d*)$");
            //在指定的输入字符串中搜索 Regex 构造函数中指定的正则表达式匹配项
            System.Text.RegularExpressions.Match mc = rg.Match(obj.ToString());
            //指示匹配是否成功
            return (mc.Success);
        }
        /// <summary>
        /// 高亮显示
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <param name="findstr">查找字符串</param>
        /// <param name="cssclass">Style</param>
        /// <returns></returns>
        public static string OutHighlightText(string str, string findstr, string cssclass)
        {
            if (findstr != "")
            {
                string text1 = "<span class=\"" + cssclass + "\">%s</span>";
                str = str.Replace(findstr, text1.Replace("%s", findstr));
            }
            return str;
        }
        /// <summary>
        ///剥离 去除html标签
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string OutHtmlText(string str)
        {
            string text1 = "<.*?>";
            Regex regex1 = new Regex(text1);
            str = regex1.Replace(str, "");
            str = str.Replace("[$page]", "");
            str = str.Replace("&nbsp;", "");
            return ToHtmlText(str);
        }
        /// <summary>
        /// 将html显示成文本
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToHtmlText(string str)
        {
            if (str == "")
            {
                return "";
            }
            StringBuilder builder1 = new StringBuilder();
            builder1.Append(str);
            builder1.Replace("<", "&lt;");
            builder1.Replace(">", "&gt;");
            //builder1.Replace("\r", "<br>");
            return builder1.ToString();
        }
        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="strInput">输入字符串</param>
        /// <param name="intLen"></param>
        /// <returns></returns>
        public static string CutString(string strInput, int intLen)
        {
            strInput = strInput.Trim();
            byte[] buffer1 = Encoding.Default.GetBytes(strInput);
            if (buffer1.Length > intLen)
            {
                string text1 = "";
                for (int num1 = 0; num1 < strInput.Length; num1++)
                {
                    byte[] buffer2 = Encoding.Default.GetBytes(text1);
                    if (buffer2.Length >= (intLen - 4))
                    {
                        break;
                    }
                    text1 = text1 + strInput.Substring(num1, 1);
                }
                return (text1 + "...");
            }
            return strInput;
        }
        /// <summary>
        /// 根据条件返回值
        /// </summary>
        /// <param name="ifValue"></param>
        /// <param name="trueValue"></param>
        /// <param name="falseVale"></param>
        /// <returns></returns>
        public static string IfValue(bool ifValue, string trueValue, string falseVale)
        {
            if (ifValue)
            {
                return trueValue;
            }
            return falseVale;
        }
        // 检测字符的是否合法; 判断非法字符串
        public static bool isLegalCharacters(string sExt)
        {
            // 合法的字符为;
            // 字母,数字,字符:  a-z      A-Z     0-9      -   .   @    _;
            // 对应的ASCII码：  97-123   65-90   48-57   45  46  64   95;

            char[] s3 = sExt.ToLower().ToCharArray();
            // char[] s2 = ".-_@".ToCharArray();
            for (int i = 0; i < sExt.Length; i++)
            {
                // 转换成数值型判断其ASCII码;
                int sOneCharacter = Convert.ToInt16(s3[i]);
                if (!((sOneCharacter >= 97 && sOneCharacter <= 123) || (sOneCharacter >= 48 && sOneCharacter <= 57) || sOneCharacter == 45 || sOneCharacter == 46 || sOneCharacter == 64 || sOneCharacter == 95))
                {
                    return false;
                }
            }
            return true;
        }

    }
}
