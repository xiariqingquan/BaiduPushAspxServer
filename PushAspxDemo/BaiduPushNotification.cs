using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace PushAspxDemo
{
    public class BaiduPushNotification
    {
        public string title { get; set; } //通知标题，可以为空；如果为空则设为appid对应的应用名;
        public string description { get; set; } //通知文本内容，不能为空;
        public int notification_builder_id { get; set; } //android客户端自定义通知样式，如果没有设置默认为0;
        public int notification_basic_style { get; set; } //只有notification_builder_id为0时才有效，才需要设置，如果notification_builder_id为0则可以设置通知的基本样式包括(响铃：0x04;振动：0x02;可清除：0x01;),这是一个flag整形，每一位代表一种样式;
        public int open_type { get; set; }//点击通知后的行为(打开Url：1; 自定义行为：2：其它值则默认打开应用;);
        public string url { get; set; } //只有open_type为1时才有效，才需要设置，如果open_type为1则可以设置需要打开的Url地址;
        public int user_confirm { get; set; } //只有open_type为1时才有效，才需要设置,(需要请求用户授权：1；默认直接打开：0), 如果open_type为1则可以设置打开的Url地址时是否请求用户授权;
        public string pkg_content { get; set; }//只有open_type为2时才有效，才需要设置, 如果open_type为2则可以设置自定义打开行为(具体参考管理控制台文档);
        public string custom_content { get; set; }// 自定义内容，键值对，Json对象形式(可选)；在android客户端，这些键值对将以Intent中的extra进行传递。

        public BaiduPushNotification()
        {
            notification_builder_id = 0;
            notification_basic_style = 0;

            url = "";
            user_confirm = 0;
            pkg_content = "";
            custom_content = "";
            open_type = 0;

        }

        public string getJsonString()
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(this);
        }
    }
}