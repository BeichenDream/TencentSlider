using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beichen.web;
using QQlogin.Properties;
using Beichen.TextMpt;
using System.Net;
using System.IO;
using type;
using System.Text.RegularExpressions;
using Beichen.Js;
namespace Beichen.QQlogin
{
    //作者QQ：1489154212     瞎几把交流群：303544938
    class QQLogin
  {

        ///   <summary>   
        ///   是否需要验证码   
        ///   </summary> 
        ///  <param name="qq">QQ号</param>
        ///   <param name="cap_cd">cap_cd</param>
        ///   <param name="code">code</param> 
        ///  <param name="verify">verify</param> 
        /// <returns>逻辑型</returns>
        public bool Is_Code(string qq,ref string cap_cd,ref string code,ref string verify) {
            string url = "https://ssl.ptlogin2.qq.com/check?pt_tea=2&uin=" + qq + "&appid=" + Resources.aid + "&ptlang=2052&regmaster=&pt_uistyle=9&r=" + GetText.GenerateTimeStamp() + "&pt_jstoken=1468653375";
            string temp = string.Empty;
             temp = WEB.GetWebData(url,ref temp,ref temp,ref temp);
          
         string [] data= GetText.TextMiddle(temp, "ptui_checkVC(", ")", 0).Replace("'", "").Split(',');
            if (data[0] == "0")
            {
                code = data[1];
                verify = data[3];
                return false;
            }
            else if (data[0] == "1")
            {
                cap_cd = data[1];
                return true;
            }
            else {
                return false;
            }
        }
        ///   <summary>   
        ///   获取验证码基本配置   
        ///   </summary> 
        ///  <param name="qq">QQ号</param>
        ///   <param name="cap_cd">cap_cd</param>
        ///   <param name="sid">sid</param> 
        ///  <param name="sess">sess</param> 
        /// <returns>逻辑型</returns>
        public bool GetSliderInfo(string qq, string cap_cd, ref string sid, ref string sess) {
            string callback, data,url,temp=string.Empty;
            Json json = new Json();
            callback = "_aq_" + GetText.GetRandom(111111,999999);
            url = "https://ssl.captcha.qq.com/cap_union_prehandle?aid=" + Resources.aid + "&captype=&protocol=https&clientype=1&disturblevel=&apptype=2&noheader=0&color=&showtype=&fb=1&theme=&lang=2052&ua=" + Resources.ua + "&cap_cd=" + cap_cd + "&uid=" + qq + "&callback=" + callback + "&sess=&subsid=1";
            data = WEB.GetWebData(url,ref temp,ref temp,ref temp);
            data = GetText.TextMiddle(data, callback + "(", ")", 0);
            json.Analysis(data);
            sid= json.Getvalue("sid");
            sess = json.Getvalue("sess");
            if (sid == "" || sess == "")
            {
                return false;
            }
            else {
                return true;
            }
            
        }
        ///   <summary>   
        ///   获取验证码剩余配置   
        ///   </summary> 
        ///  <param name="qq">QQ账号</param>
        ///  <param name="sid">sid</param>
        ///  <param name="sess">sess</param>
        ///  <param name="cap_cd">cap_cd</param>
        ///  <param name="vsig">vsig</param>
        ///  <param name="rand_par">rand_par</param>
        ///  <param name="websig">websig</param>
        ///  <param name="y">Y坐标</param>
        /// <returns>Image</returns>
        public void LoadSlider(string qq, string sid, string sess, string cap_cd, ref string vsig, ref string rand_par, ref string websig, ref int y) {
            string temp = string.Empty, url = string.Empty, data=string.Empty;
            Regexs a=new Regexs();
            url = "https://ssl.captcha.qq.com/cap_union_new_show?aid=" + Resources.aid + "&captype=&protocol=https&clientype=1&disturblevel=&apptype=2&noheader=0&color=&showtype=&fb=1&theme=&lang=2052&ua=" + Resources.ua + "&sess=" + sess + "&fwidth=0&sid=" + sid + "&uid=" + qq + "&cap_cd=" + cap_cd + "&rnd=" +GetText.GetRandom(111111,999999)+ "&forcestyle=undefined&wxLang=";
            data= WEB.GetWebData(url,ref temp,ref temp,ref temp);
            a.Matches(data, "class=13&vsig=\"\\+(.*?)\\+");
            temp = a.GetText(0, 1);
            a.Matches(data, ","+"Q"+"=\"(.*?)\"");
            vsig = a.GetText(0, 1);
            a.Matches(data, "Number\\(\"(.*?)\"");
            y = Convert.ToInt16(a.GetText(0, 1));
            a.Matches(data, "ans=\"(.*?)\"&(.*?)=");
            rand_par = a.GetText(0, 2);
            a.Matches(data, "websig=(.*?)&");
            websig = a.GetText(0, 1);
        }
        ///   <summary>   
        ///   提交验证码   
        ///   </summary> 
        ///  <param name="qq">QQ账号</param>
        ///  <param name="sess">sess</param>
        ///  <param name="sid">sid</param>
        ///  <param name="cap_cd">cap_cd</param>
        ///  <param name="vsig">vsig</param>
        ///  <param name="x">x坐标</param>
        ///  <param name="y">y坐标</param>
        ///  <param name="rand_par">rand_par</param>
        ///  <param name="data">data</param>
        ///  <param name="websig">websig</param>
        ///  <param name="cdata">cdata</param>
        ///  <param name="ticket">ticket</param>
        /// <returns>文本型</returns>
        public string SubmissionCode(string qq,string sess,string sid,string cap_cd,string vsig,string x,string y,string rand_par,string data,string websig,string cdata,ref string ticket) {
            Json json = new Json();
            string url, PostData,temp="";
            url = "https://ssl.captcha.qq.com/cap_union_new_verify";
            PostData = "aid=" + Resources.aid + "&captype=&protocol=https&clientype=1&disturblevel=&apptype=2&noheader=0&color=&showtype=&fb=1&theme=&lang=2052&ua=" + Resources.ua + "&sess=" + sess + "&fwidth=0&sid=" + sid + "&subsid=6&uid=" + qq + "&cap_cd=" + cap_cd + "&rnd=411109&forcestyle=undefined&wxLang=&TCapIframeLoadTime=9&prehandleLoadTime=36&createIframeStart=1530521827025&tcScale=1&rand=0.8450369252791015&subcapclass=13&vsig=" + vsig + "&ans=" + x + "," + y + ";&" + rand_par + "=" + data + "&websig=" + websig + "&cdata=" + cdata + "&fpinfo=" + Resources.fpinfo + "&tlg=1&vlg=0_0_0&vmtime=_&vmData=";
            PostData=WEB.GetWebData(url,ref temp, ref temp,ref temp, "POST",temp,temp,PostData);
            json.Analysis(PostData);
            ticket=json.Getvalue("ticket");
            if (json.Getvalue("randstr") != "")
            {
                return json.Getvalue("randstr");
            }
            else {
                return PostData;
            }
        }
        ///   <summary>   
        ///   轨道加密   
        ///   </summary> 
        ///  <param name="x">缺口X坐标</param>
        /// <returns>文本型</returns>
        public string TrajectoryEncryption(int x) {
            JS jS = new JS();
            jS.js.ExecuteStatement(Resources.轨迹加密js);
            return jS.js.Eval("getKey(\""+x+"\")");
        }
        ///   <summary>   
        ///   密码加密   
        ///   </summary> 
        ///  <param name="qq">QQ账号</param>
        ///  <param name="password">QQ密码</param>
        ///  <param name="code">验证码</param>
        /// <returns>文本型</returns>
        public string PasswordEncryption(string qq,string password,string code) {
            JS jS = new JS();
            //var v_qq = qq; var v_password = password; var v_code = code;
            try
            {
                jS.js.ExecuteStatement(Resources.密码加密JS);
                return jS.js.Run("getEncryption", password, qq, code);
            }
            catch (Exception)
            {

                return "";
            }

           // return jS.js.Eval("getEncryption(\"" + qq + "\",\"" + password + "\",\"" + code + "\")");
        }
        ///   <summary>   
        ///   登录   
        ///   </summary> 
        ///  <param name="qq">QQ账号</param>
        ///  <param name="password">QQ密码</param>
        ///  <param name="code">验证码</param>
        ///  <param name="tv">ticket 或者 verify(需要滑块验证码的时候填ticket)</param>
        ///  <param name="cookie">登录成功或失败返回的cookie</param>
        ///  <param name="is_need_code">如果经过滑块了就填真(与tv一个性质)</param>
        /// <returns>文本型</returns>
        public string Login(string qq,string password,string code,string tv,ref string cookie,bool is_need_code) {
            string url,p,data,temp=string.Empty;
            p=PasswordEncryption(qq,password,code);
            if (is_need_code)
            {
                url = "https://ssl.ptlogin2.qq.com/login?verifycode=" + code.ToUpper() + "&u=" + qq + "&p=" + p + "&pt_randsalt=2&ptlang=2052&low_login_enable=1&low_login_hour=720&u1=https%3A%2F%2Fw.mail.qq.com%2Fcgi-bin%2Flogin%3Fvt%3Dpassport%26vm%3Dwsk%26delegate_url%3D%26f%3Dxhtml%26target%3D%26ss%3D1&from_ui=1&fp=loginerroralert&device=2&aid=" + Resources.aid + "&daid=4&pt_3rd_aid=0&ptredirect=1&h=1&g=1&pt_uistyle=9&regmaster=&pt_vcode_v1=1&pt_verifysession_v1=" + tv + "&";

                 data = WEB.GetWebData(url, ref temp, ref temp, ref cookie);
                return data;
            }
            else {

                url = "https://ssl.ptlogin2.qq.com/login?pt_vcode_v1=0&pt_verifysession_v1=" + tv + "&verifycode=" + code.ToUpper() + "&u=" + qq + "&p=" + p + "&pt_randsalt=2&ptlang=2052&low_login_enable=1&low_login_hour=720&u1=https%3A%2F%2Fw.mail.qq.com%2Fcgi-bin%2Flogin%3Fvt%3Dpassport%26vm%3Dwsk%26delegate_url%3D%26f%3Dxhtml%26target%3D%26ss%3D1&from_ui=1&fp=loginerroralert&device=2&aid=" + Resources.aid + "&daid=4&pt_3rd_aid=0&ptredirect=1&h=1&g=1&pt_uistyle=9&regmaster=&";
                data = WEB.GetWebData(url, ref temp, ref temp, ref cookie);
                return data;
            }

        }


    }
    class QQcode{
        ///   <summary>   
        ///   在网络获取一个图片   
        ///   </summary> 
        ///  <param name="url">图片地址</param>
        /// <returns>Image</returns>
        public static Image GetImg(string url)
        {

            WebRequest webreq = WebRequest.Create(url);
            WebResponse webres = webreq.GetResponse();
            Stream stream = webres.GetResponseStream();
            return Image.FromStream(stream);
        }
        ///   <summary>   
        ///   取滑块阴影坐标   
        ///   </summary> 
        ///  <param name="sess">sess</param>
        ///   <param name="sid">sid</param> 
        ///   <param name="vsig">vsig</param> 
        /// <returns>整数型</returns>
        public static int SliderCoordinate(string sess, string sid,string vsig,int y)
        {
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch(); //查看程序耗时
            stopwatch.Start();//开始计时
            string code_img_url = "https://ssl.captcha.qq.com/cap_union_new_getcapbysig?aid=" + Resources.aid + "&clientype=2&accver=1&ua=" + Resources.ua + "&ptcz=62894169db720fee1652745b0e05d87306bd4cb4a090dc5e38c561bc680764c2&fpinfo=" + Resources.fpinfo + "&tkid=1785885581&sess=" + sess + "&theme=undefined&sid=" + sid + "&showtype=popup&fb=1&forcestyle=undefined&uid=&cap_cd=&lang=2052&rnd=" + GetText.GetRandom(111111,999999) + "&rand=" +GetText.GenerateTimeStamp() + "&vsig=" + vsig + "&img_index=";
            Bitmap newBmp = (Bitmap)GetImg(code_img_url + "1");
            Bitmap oldBmp = (Bitmap)GetImg(code_img_url + "0");
            Login.F.newBmp.Image = Image.FromHbitmap(newBmp.GetHbitmap());
            Login.F.oldBmp.Image = Image.FromHbitmap(oldBmp.GetHbitmap());
            for (int i = 400; i < newBmp.Width; i++)  //因为腾讯的滑块没有低于450的,为了保险填400
            {
                for (int j = y; j < y+100; j++)   //加载验证码配置的y坐标,比实际的要小100
                {
                    //获取一个点的像素的RGB的颜色
                    Color oldColor = oldBmp.GetPixel(i, j);
                    Color newColor = newBmp.GetPixel(i, j);
                    if (Math.Abs(oldColor.R - newColor.R) > 60 || Math.Abs(oldColor.G - newColor.G) > 60 || Math.Abs(oldColor.B - newColor.B) > 60)
                    {
                        Login.F.Log.Text += String.Format("计算 X：{0} Y:{1}  计算耗时:{2}s", i, j, stopwatch.Elapsed.TotalSeconds) + Environment.NewLine;
                        return i-16;
                    }


                }
            }
            return 0;
        }

    }

}
