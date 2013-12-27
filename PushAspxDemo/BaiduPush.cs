using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace PushAspxDemo
{
    public class BaiduPush
    {
        public PushOptions opts { get; set; }

        public string httpMehtod { get; set; }
        public string url { get; set; }
        public string secret_key { get; set; }

        public BaiduPush(string httpMehtod, string secret_key)
        {
            this.httpMehtod = httpMehtod;
            this.url = "http://channel.api.duapp.com/rest/2.0/channel/channel";
            this.secret_key = secret_key;
        }


        public string PushMessage(PushOptions opts)
        {

            this.opts = opts;

            Dictionary<string, string> dic = new Dictionary<string, string>();

            //将键值对按照key的升级排列
            var props = typeof(PushOptions).GetProperties().OrderBy(p => p.Name);
            foreach (var p in props)
            {
                if (p.GetValue(this.opts, null) != null)
                {
                    dic.Add(p.Name, p.GetValue(this.opts, null).ToString());
                }
            }
            //生成sign时，不能包含sign标签，所有移除
            dic.Remove("sign");

            var preData = new StringBuilder();
            foreach (var l in dic)
            {
                preData.Append(l.Key);
                preData.Append("=");
                preData.Append(l.Value);

            }

            //按要求拼接字符串，并urlencode编码
            var str = HttpUtility.UrlEncode(this.httpMehtod.ToUpper() + this.url + preData.ToString() + this.secret_key, System.Text.Encoding.UTF8);

            var strSignUpper = new StringBuilder();
            int perIndex = 0;
            for (int i = 0; i < str.Length; i++)
            {
                var c = str[i].ToString();
                if (str[i] == '%')
                {
                    perIndex = i;
                }
                if (i - perIndex == 1 || i - perIndex == 2)
                {
                c = c.ToUpper();
                }
                strSignUpper.Append(c);
            }

            strSignUpper = strSignUpper.Replace("(", "%28").Replace(")", "%29");

            
            var sign = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(strSignUpper.ToString(), "MD5").ToLower();
            
            //加入生成好的sign键值对
            dic.Add("sign", sign);
            var strb = new StringBuilder();
            //int tagIndex = 0;
            foreach (var l in dic)
            {

                strb.Append(l.Key);
                strb.Append("=");
                strb.Append(l.Value);
                strb.Append("&");
            }

            var postStr = strb.ToString().EndsWith("&") ? strb.ToString().Remove(strb.ToString().LastIndexOf('&')) : strb.ToString();


            byte[] data = Encoding.UTF8.GetBytes(postStr);//编码，尤其是汉字，事先要看下抓取网页的编码方式  
            WebClient webClient = new WebClient();
            try
            {
                webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");//采取POST方式必须加的header，如果改为GET方式的话就去掉这句话即可  
                byte[] responseData = webClient.UploadData(this.url, "POST", data);//得到返回字符流  
                string srcString = Encoding.UTF8.GetString(responseData);//解码  
                return "Post:" + postStr + "\r\n\r\n" + "Response:" + srcString;
            }
            catch (WebException ex)
            {
                Stream stream = ex.Response.GetResponseStream();
                string m = ex.Response.Headers.ToString();
                byte[] buf=new byte[256];
                stream.Read(buf, 0, 256);
                stream.Close();
                int count = 0;
                foreach (var b in buf)
                {
                    if(b>0)
                    {
                        count++;
                    }else
                    {
                        break;
                    }
                }
                return  Post:" + postStr + ex.Message + "\r\n\r\n" + Encoding.UTF8.GetString(buf, 0, count);
            }
        }
    }
}