using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Text.RegularExpressions;
using Beichen.Js;
using QQlogin.Properties;
namespace Beichen.TextMpt
{
    //作者QQ：1489154212     瞎几把交流群：303544938
    class GetText
    {
        ///   <summary>   
        ///   取文本中间。   
        ///   </summary> 
        ///  <param name="AllText">原文本</param>
        ///   <param name="Front">左边文本</param> 
        ///   <param name="Behind">右边文本</param> 
        ///   <param name="startingpoint">起始位置</param>
        /// <returns>文本型</returns>
        public static string TextMiddle(string AllText, string Front, string Behind, int startingpoint)
        {

            int TextFront = AllText.IndexOf(Front, startingpoint);
            int TextBehind = AllText.IndexOf(Behind, TextFront + 1);
            if (TextFront < 0 || TextBehind < 0)
            {
                return "";
            }
            else
            {
                TextFront = TextFront + Front.Length;
                TextBehind = TextBehind - TextFront;
                if (TextFront < 0 || TextBehind < 0)
                {
                    return "";
                }
                return AllText.Substring(TextFront, TextBehind);
            }

        }
        ///   <summary>   
        ///   取文本重复次数   
        ///   </summary> 
        ///  <param name="data">原文本</param>
        ///   <param name="Text">查找文本</param> 
        /// <returns>整数型</returns>
        public static int TextOccurrences(string data, string Text)
        {
            string data2 = data.Replace(Text, "");
            int number = data.Length - data2.Length;
            return number / Text.Length;
        }
        ///   <summary>   
        ///   取随机数   
        ///   </summary> 
        ///  <param name="a">最小值</param> 
        ///  <param name="b">最大值</param> 
        /// <returns>文本型</returns>
        public static string GetRandom(int a,int b) {
            Random c = new Random();
            return c.Next(a, b).ToString();
        }
        ///   <summary>   
        ///   读入文本   
        ///   </summary> 
        ///  <param name="file">路径</param> 
        /// <returns>文本型</returns>
        public string GetTxt(string file)
        {
            try
            {
                StreamReader SR = new StreamReader(file, Encoding.Default);
                string content = SR.ReadToEnd();
                SR.Dispose();
                return content;
            }
            catch
            {
                return "";
            }
        }
        ///   <summary>   
        ///   写出文件   
        ///   </summary> 
        ///  <param name="path">路径</param> 
        ///  <param name="data">数据</param> 
        /// <returns>逻辑型</returns>
        public bool WriteOutText(string path, string data)
        {
            try
            {
                FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write);
                StreamWriter text = new StreamWriter(file);
                text.Write(data);
                text.Close();
                file.Close();
                return true;
            }
            catch
            {
                return false;
            }
            


        }
        ///   <summary>   
        ///   取时间戳   
        ///   </summary> 
        /// <returns>文本型</returns>
        public static string GenerateTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        ///   <summary>   
        ///   分割文本   
        ///   </summary> 
        ///  <param name="data">原文本</param>
        ///  <param name="UDivisionText">用作分割文本</param>
        /// <returns>文本型</returns>
        public string[] DivisionText(string data, string UDivisionText)
        {
            string[] sArray = Regex.Split(data, UDivisionText, RegexOptions.IgnoreCase);
            return sArray;
        }
    }
    class Json {

        private string json;
        JS jS = new JS();

        ///   <summary>   
        ///   JSON解析数据   
        ///   </summary> 
        ///  <param name="data">Json数据</param>
        /// <returns>无</returns>
        public void Analysis(string data) {
            jS.js.ExecuteStatement(Resources.Json);
            json = data;
        }
        ///   <summary>   
        ///   取属性值   
        ///   </summary> 
        ///  <param name="parameter">json格式的访问规则{"user":"ss","list":[{"u":"666"}]},访问规则可以是user.list.0.u，就得到u的值,访问user就是直接user</param>
        /// <returns>文本型</returns>
        public string Getvalue(string parameter) {

        var a= jS.js.Run("Getvalue", json, parameter);

            return a;

        }
        ///   <summary>   
        ///   设置属性值   
        ///   </summary> 
        ///  <param name="parameter">属性</param>
        ///  <param name="value">值</param>
        /// <returns>逻辑型</returns>
        public bool Setvalue(string parameter,string value) {
            var a = jS.js.Run("Setvalue", json, parameter, value);
            if (a == "")
            {
                return false;
            }
            else {
                json = a;
                return true;
            }
        }
        ///   <summary>   
        ///   删除一个属性   
        ///   </summary> 
        ///  <param name="parameter">要删除的属性</param>
        /// <returns>逻辑型</returns>
        public bool del(string parameter)
        {
            var a = jS.js.Run("del", json, parameter);
            if (a == "")
            {
                return false;
            }
            else
            {
                json = a;
                return true;
            }
        }
        ///   <summary>   
        ///   获取一个属性的json   
        ///   </summary> 
        ///  <param name="parameter">要被取属性</param>
        /// <returns>文本型</returns>
        public string GetOneJson(string parameter)
        {
            var a = jS.js.Run("GetOneJson", json, parameter);
            return a;
        }



    }
    class Regexs {
        private GroupCollection b;
        private  MatchCollection a;
        ///   <summary>   
        ///   创建一个正则表达式   
        ///   </summary> 
        ///  <param name="data">要匹配的字符串</param>
        ///  <param name="parameter">正则表达式</param>
        /// <returns>无</returns>
        public void Matches(string data,string patterm) {
          a=  Regex.Matches(data, patterm);
        }
        ///   <summary>   
        ///   取匹配文本   
        ///   </summary> 
        ///  <param name="MatchingIndex">匹配索引</param>
        ///  <param name="SubmatchingIndex">子匹配索引</param>
        /// <returns>文本型</returns>
        public string GetText(int MatchingIndex, int SubmatchingIndex) {
            b = a[MatchingIndex].Groups;
            return b[SubmatchingIndex].Value;
        }


    }
}
