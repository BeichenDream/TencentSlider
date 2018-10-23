using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//作者QQ：1489154212     瞎几把交流群：303544938
namespace Beichen.Js
{
    class JS
 {
        public MSScriptControl.ScriptControl js = new MSScriptControl.ScriptControl();
        public JS() {
            js.UseSafeSubset = false;
            js.Language = "JScript";

        }
        

        /////   <summary>   
        /////   置入JS代码   
        /////   </summary> 
        /////  <param name="JS">需要置入JS</param>
        ///// <returns>无</returns>
        //public bool 置入JS(string JS) {
        //    try
        //    {
        //        js.AddCode(JS);
        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        return false;
               
        //    }
        // }
        /////   <summary>   
        /////   运行指定方法并返回结果   
        /////   </summary> 
        /////  <param name="name">函数名字</param>
        /////  <param name="parm">参数</param>
        ///// <returns>文本型</returns>
        //public string 运行方法(string name, params object[] parm) {
        //    string temp = name+"(";
        //    foreach (var item in parm)
        //    {
        //        temp += "\"" + item.ToString() + "\"" + ",";

        //        /**  if (item is string)
        //          {

        //              temp += "\"" + item.ToString() + "\"" + ",";
        //          }
        //          else {
        //              temp +=   item.ToString()  + ",";

        //          }*/

        //    }
        //    temp=temp.Substring(0, temp.Length - 1)+")";
        //       try
        //       {
                   
        //           return js.Eval(temp);
        //       }
        //       catch (Exception ex)
        //       {
        //           return ex.Source + ex.Message;
        //       }

        //    ///   <summary>   
        //    ///   运行指定方法并返回结果   
        //    ///   </summary> 
        //    ///  <param name="name">函数名字</param>
        //    ///  <param name="parm">参数</param>
        //    /// <returns>文本型</returns>

        //}


    }
}
