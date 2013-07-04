using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace PushAspxDemo
{
    public class IOSNotification
    {
        public string title { get; set; } //通知标题，可以为空；如果为空则设为appid对应的应用名;
        public string description { get; set; } //通知文本内容，不能为空;
        public string aps { get; set; } //android客户端自定义通知样式，如果没有设置默认为0;

        public IOSNotification()
        {

        }

        public string getJsonString()
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(this);
        }
    }
}