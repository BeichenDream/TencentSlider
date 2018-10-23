using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;
using Beichen.TextMpt;
namespace Beichen.web
{
    //作者QQ：1489154212     瞎几把交流群：303544938
    class http
{
     public  HttpWebRequest webRequest = null;
     public   StreamWriter writer = null;
        /////   <summary>   
        /////   初始化   
        /////   </summary> 
        /////  <param name="url">网址</param>
        ///// <returns>无</returns>
        public http(string url) {
            webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Method = "GET";
            webRequest.Timeout = 5000;
            
        }
        /////   <summary>   
        /////   设置协议头 一行一个请用换行符隔开,建议填写常量值或文本值,防止因传参引发错误   
        /////   </summary> 
        /////  <param name="Headers">协议头 (可以添加cookie)</param>
        ///// <returns>无</returns>
        public void  SetHeaders(string Headers) {
            MethodInfo priMethod = webRequest.Headers.GetType().GetMethod("AddWithoutValidate", BindingFlags.Instance | BindingFlags.NonPublic);  //创建反射 因为HEAD 无法直接直接设置大部分协议头
            Headers = Headers.Trim();//去掉空白字符
            string[] a = Regex.Split(Headers, Environment.NewLine, RegexOptions.IgnoreCase);
            string[] b = new string[2];
            foreach (var item in a)
            {
                b=item.Split(':');
                if (b[0] == "" || b.Length<2 ||b[1]=="")
                {
                    continue;
                }
                else {
                    if (b.Length > 2) {
                        for (int i = 3; i <= b.Length; i++)
                        {
                            b[1] = b[1] + ":" + b[i-1];
                        }

                    }
                    try
                    {
                        priMethod.Invoke(webRequest.Headers, new[] { b[0], b[1] });
                        Console.WriteLine("{0}   {1}", b[0], b[1]);
                    }
                    catch (Exception)
                    {

                        continue;
                    }
                
                }


                }
        }
        /////   <summary>   
        /////   设置POST数据 如果是json数据 记得在协议头添加Content-Type: application/json; charset=UTF-8   
        /////   </summary> 
        /////  <param name="data">需要发送的数据</param>
        ///// <returns>无</returns>
        public void PostData(string data)
        {
            webRequest.ContentLength = data.Length;
            webRequest.Method = "POST";
            StreamWriter writer = new StreamWriter(webRequest.GetRequestStream(), Encoding.ASCII);
            writer.Write(data);
            writer.Flush();

        }
        public string GetResponse(ref string Headers , ref string Code,ref string cookie) {
            Headers = string.Empty;
            cookie = string.Empty;
            Code = string.Empty;
            string data = string.Empty;
            HttpWebResponse res = null;
            try
            {
                res= (HttpWebResponse)webRequest.GetResponse();
                StreamReader text = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
                data = text.ReadToEnd();
                
            }
            catch (WebException e)
            {
                data = e.Message;

            }
            finally {
                Code = GetCode(res);
                cookie = GetCookies(res,webRequest.Headers.Get("cookie"));
               
                Headers = GetHeaders(res);

                }
            return data;
        }
        /////   <summary>   
        /////   获取响应状态码   
        /////   </summary> 
        /////  <param name="res">已经发送完的httpwebrespone</param>
        ///// <returns>文本型</returns>
        public string GetCode(HttpWebResponse  res) {
            if (res.StatusCode == HttpStatusCode.OK)
            {
                return "200";
            }

            if (res.StatusCode == HttpStatusCode.Accepted)
            {
                return "202";
            }

            if (res.StatusCode == HttpStatusCode.Ambiguous)
            {
                return "300";
            }

            if (res.StatusCode == HttpStatusCode.BadGateway)
            {
                return "502";
            }

            if (res.StatusCode == HttpStatusCode.BadRequest)
            {
                return "400";
            }

            if (res.StatusCode == HttpStatusCode.Conflict)
            {
                return "409";
            }

            if (res.StatusCode == HttpStatusCode.Continue)
            {
                return "100";
            }

            if (res.StatusCode == HttpStatusCode.Created)
            {
                return "201";
            }

            if (res.StatusCode == HttpStatusCode.ExpectationFailed)
            {
                return "417";
            }


            if (res.StatusCode == HttpStatusCode.Forbidden)
            {
                return "403";
            }

            if (res.StatusCode == HttpStatusCode.Found)
            {
                return "302";
            }

            if (res.StatusCode == HttpStatusCode.GatewayTimeout)
            {
                return "504";
            }

            if (res.StatusCode == HttpStatusCode.Gone)
            {
                return "410";
            }

            if (res.StatusCode == HttpStatusCode.HttpVersionNotSupported)
            {
                return "505";
            }


            if (res.StatusCode == HttpStatusCode.InternalServerError)
            {
                return "500";
            }

            if (res.StatusCode == HttpStatusCode.LengthRequired)
            {
                return "411";
            }


            if (res.StatusCode == HttpStatusCode.MethodNotAllowed)
            {
                return "405";
            }

            if (res.StatusCode == HttpStatusCode.Moved)
            {
                return "301";
            }

            if (res.StatusCode == HttpStatusCode.MovedPermanently)
            {
                return "301";
            }

            if (res.StatusCode == HttpStatusCode.MultipleChoices)
            {
                return "300";
            }


            if (res.StatusCode == HttpStatusCode.NoContent)
            {
                return "204";
            }

            if (res.StatusCode == HttpStatusCode.NonAuthoritativeInformation)
            {
                return "203";
            }

            if (res.StatusCode == HttpStatusCode.NotAcceptable)
            {
                return "406";
            }


            if (res.StatusCode == HttpStatusCode.NotFound)
            {
                return "404";
            }

            if (res.StatusCode == HttpStatusCode.NotImplemented)
            {
                return "501";
            }

            if (res.StatusCode == HttpStatusCode.NotModified)
            {
                return "304";
            }

            if (res.StatusCode == HttpStatusCode.PartialContent)
            {
                return "206";
            }

            if (res.StatusCode == HttpStatusCode.PaymentRequired)
            {
                return "402";
            }

            if (res.StatusCode == HttpStatusCode.PreconditionFailed)
            {
                return "412";
            }


            if (res.StatusCode == HttpStatusCode.ProxyAuthenticationRequired)
            {
                return "407";
            }

            if (res.StatusCode == HttpStatusCode.Redirect)
            {
                return "302";
            }

            if (res.StatusCode == HttpStatusCode.RedirectKeepVerb)
            {
                return "307";
            }

            if (res.StatusCode == HttpStatusCode.RedirectMethod)
            {
                return "303";
            }

            if (res.StatusCode == HttpStatusCode.RequestedRangeNotSatisfiable)
            {
                return "416";
            }

            if (res.StatusCode == HttpStatusCode.RequestEntityTooLarge)
            {
                return "413";
            }

            if (res.StatusCode == HttpStatusCode.RequestTimeout)
            {
                return "408";
            }

            if (res.StatusCode == HttpStatusCode.RequestUriTooLong)
            {
                return "414";
            }

            if (res.StatusCode == HttpStatusCode.ResetContent)
            {
                return "205";
            }

            if (res.StatusCode == HttpStatusCode.SeeOther)
            {
                return "303";
            }

            if (res.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                return "503";
            }

            if (res.StatusCode == HttpStatusCode.SwitchingProtocols)
            {
                return "101";
            }

            if (res.StatusCode == HttpStatusCode.TemporaryRedirect)
            {
                return "307";
            }

            if (res.StatusCode == HttpStatusCode.Unauthorized)
            {
                return "401";
            }

            if (res.StatusCode == HttpStatusCode.UnsupportedMediaType)
            {
                return "415";
            }

            if (res.StatusCode == HttpStatusCode.Unused)
            {
                return "306";
            }

            if (res.StatusCode == HttpStatusCode.UpgradeRequired)
            {
                return "426";
            }

            if (res.StatusCode == HttpStatusCode.UseProxy)
            {
                return "305";
            }
            res.Dispose();
            return "0";


        }

        /////   <summary>   
        /////   获取响应协议头   
        /////   </summary> 
        /////  <param name="res">已经发送完的httpwebrespone</param>
        ///// <returns>文本型</returns>
        public string GetHeaders(HttpWebResponse res)
        {
            string data = string.Empty;
            foreach (string HeaderKey in res.Headers)
                //  res.Add(HeaderKey, res.Headers[HeaderKey]);
                data += HeaderKey +":"+ res.Headers[HeaderKey] + Environment.NewLine;
            res.Dispose();
            return data;

        }
        /////   <summary>   
        /////   获取响应cookies   
        /////   </summary> 
        /////  <param name="res">已经发送完的httpwebrespone</param>
        ////    <param name="oldcookie">旧的cookies</param>
        ///// <returns>文本型</returns>
        public string GetCookies(HttpWebResponse res, string oldcookie)
        {
            if (res.Headers.Get("Set-Cookie") == null) { return oldcookie; }
            Dictionary<string, string> a = new Dictionary<string, string>();
            string temp = string.Empty;
            string[] cookie = res.Headers.Get("Set-Cookie").Split(',');
            if (oldcookie == null) { oldcookie = ""; }
            oldcookie = oldcookie.Trim();
           
            for (int i = 0; i < cookie.Length; i++)
            {
                if (GetText.TextOccurrences(cookie[i], "=") == 0)
                {
                    temp += temp + "," + cookie[i];
                }
                else
                {
                    temp = temp + cookie[i] + ";";
                    /**      try
                          {
                              if (GetText.TextOccurrences(cookie[i + 1], "=") == 0)   //思路不清晰不写了
                              {
                                  temp = temp + cookie[i];
                              }
                              else {
                                  temp = temp + cookie[i] + ";";
                              }
                          }
                          catch (Exception)
                          {

                              continue;
                          }*/

                }


            }
            temp = temp + oldcookie;
            cookie = temp.Split(';');
            temp = string.Empty;

            foreach (var item in cookie)
            {
                string[] b = item.Split('=');
                if (b.Length == 2)
                {
                    try
                    {
                        a.Add(b[0], b[1]);
                    }
                    catch (Exception)
                    {

                        a[b[0]] = b[1];
                    }
                }

            }
            foreach (var item in a)
            {
                temp = temp + item.Key + "=" + item.Value + ";";
            }
            return temp;

        }
    }
    class WEB {
        static private http http = null;
        ///   <summary>   
        ///   发送网络数据   
        ///   </summary> 
        ///  <param name="url">网址(要 带http头)</param>
        ///  <param name="code">对方返回响应状态码</param>
        ///  <param name="Header">对方返回响应协议头</param>
        ///  <param name="cookie">对方返回响应Cookie (cookie会自动更新)</param>
        ///  <param name="Method">请求方式(如 GET)</param>
        ///  <param name="Headers">请求协议头(如 GET)</param>
        ///  <param name="cookies">请求cookie</param>
        ///  <param name="data">请求数据(POST专用)</param>
        /// <returns>文本型</returns>
        public static string GetWebData(string url,ref string code, ref string Header,ref string cookie , string Method = "GET" ,string Headers = "", string cookies="", string data = "") {
         code = string.Empty; ; Header = string.Empty; cookie = string.Empty;
         http = new http(url);
        http.webRequest.Method = Method;
        http.SetHeaders( Headers+ Environment.NewLine + "cookie:"+cookies);
        if (data !="") { http.PostData(data); }
      data=   http.GetResponse(ref Header, ref code, ref cookie);
          //  Console.WriteLine("header：{0} code：{1} cookie：{2}",Header,code,cookie);
            return data;
        }


    }

}
