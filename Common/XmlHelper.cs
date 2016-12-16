using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Collections;
using System.IO;

namespace Common
{
    public class XmlHelper
    {



        /// <summary>
        /// 返回一个XML元素
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="rootName"></param>
        /// <param name="Encode"></param>
        /// <returns></returns>
        public XmlDocument CreateXmlDocument(string rootName)
        {
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                XmlDeclaration xmldecl;
                xmldecl = xmldoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                xmldoc.AppendChild(xmldecl);
                XmlElement xmlelem;

                xmlelem = xmldoc.CreateElement("", rootName, "");
                xmldoc.AppendChild(xmlelem);

                return xmldoc;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public static string GetString(XmlDocument xmlDoc)
        {
            StringWriter sw = new StringWriter();
            XmlTextWriter xw = new XmlTextWriter(sw);
            xmlDoc.WriteTo(xw);
            return sw.ToString();
        }

        /// <summary>
        /// 返回二维XML字符串
        /// </summary>
        /// <param name="ReDict"></param>
        /// <returns></returns>
        public static string BackXmlStr(Dictionary<string, string> ReDict)
        {
            StringBuilder w = new StringBuilder();
            w.Append("<root>");
            if (ReDict.Count > 0)
            {
                foreach (KeyValuePair<string, string> item in ReDict)
                {
              

                    w.Append("<" + item.Key + ">");

                    string val = item.Value.Replace("&", "&lt;");

                    w.Append(val);
                    w.Append("</" + item.Key + ">");
                }
            }
            w.Append("</root>");
            return w.ToString();
        }

    }
}
