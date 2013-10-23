using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PushAspxDemo
{
    public class PushOptions
    {
         public  PushOptions(string method,
            string apikey,
            string user_id,
            uint push_type,
            string channel_id,
            string tag,
            uint device_type,
            uint message_type,
            string messages,
            string msg_keys,
            uint message_expires,
            uint timestamp,
            uint expires,
            uint v)
        {
            this.method = method;
            this.apikey = apikey;
            this.user_id = user_id;
            this.push_type = push_type;
            this.channel_id = channel_id;
            this.tag = tag;
            this.device_type = device_type;
            this.message_type = message_type;
            this.messages = messages;
            this.msg_keys = msg_keys;
            this.message_expires = message_expires;
            this.timestamp = timestamp;
           
            this.expires = expires;
            this.v = v;
            
        }

        //单播
         public PushOptions(string method,
             string apikey,
             string user_id,
             string channel_id,
             uint device_type,
             string messages,
             string msg_keys,
             uint timestamp
             )
         {
             this.method = method;
             this.apikey = apikey;
             this.user_id = user_id;
             this.channel_id = channel_id;
             this.push_type = 1;
             this.messages = messages;
             this.msg_keys = msg_keys;
             this.timestamp = timestamp;

             this.device_type = device_type;
         }
         //组播
         public PushOptions(string method,
             string apikey,
             string tag,
             uint device_type,
             string messages,
             string msg_keys,
             uint timestamp
             )
         {
             this.method = method;
             this.apikey = apikey;
             this.tag = tag;
             this.push_type = 2;
             this.messages = messages;
             this.msg_keys = msg_keys;
             this.timestamp = timestamp;

             this.device_type = device_type;
         }

        //广播
         public PushOptions(string method,
            string apikey,
             uint device_type,
            string messages,
            string msg_keys,
            uint timestamp
            )
        {
            this.method = method;
            this.apikey = apikey;
            this.push_type = 3;
            this.messages = messages;
            this.msg_keys = msg_keys;
            this.timestamp = timestamp;

            this.device_type = device_type;
        }

        public string method { get; set; } 	//string 	是 	方法名，必须存在：push_msg。
        public string apikey { get; set; }	//string 	是 	访问令牌，明文AK，可从此值获得App的信息，配合sign中的sk做合法性身份认证。
        public string user_id { get; set; }	//string 	否 	用户标识，在Android里，channel_id + userid指定某一个特定client。不超过256字节，如果存在此字段，则只推送给此用户。
        public uint push_type { get; set; } /*	uint 	是 	推送类型，取值范围为：1～3

                                                                1：单个人，必须指定user_id 和 channel_id （指定用户的指定设备）或者user_id（指定用户的所有设备）

                                                                2：一群人，必须指定 tag

                                                                3：所有人，无需指定tag、user_id、channel_id*/
        public string channel_id { get; set; }	//string 	否 	通道标识
        public string tag { get; set; }	//string 	否 	标签名称，不超过128字节
        public uint? device_type { get; set; }	/*uint 	否 	设备类型，取值范围为：1～5

                                        百度Channel支持多种设备，各种设备的类型编号如下：（支持某种组合，如：1,2,4:）

                                        1：浏览器设备；

                                        2：PC设备；

                                        3：Andriod设备；

                                        4：iOS设备；

                                        5：Windows Phone设备；

                                        如果存在此字段，则向指定的设备类型推送消息。 默认不区分设备类型。*/
        public uint? message_type { get; set; }	/*uint 	否 	消息类型

                                                    0：消息（透传）

                                                    1：通知

                                                    默认值为0。*/
        public string messages { get; set; } 	/*string 	是 	指定消息内容，单个消息为单独字符串，多个消息用json数组表示。

                                                    如果有二进制的消息内容，请先做BASE64的编码。

                                                    一次推送最多10个消息。

                                                    注：当message_type=1且为Android端接收消息时，需按照以下格式：

                                                    "{ 
                                                       \"title\" : \"hello\" ,
                                                       \"description\": \"hello\"
                                                     }"

                                                    说明：

                                                        title : 通知标题，可以为空；如果为空则设为appid对应的Android应用名称。
                                                        description：通知文本内容，不能为空，否则Android端上不展示。 */

        public string msg_keys { get; set; } 	//string 	是 	指定消息标识，必须和messages一一对应。相同消息标识的消息会自动覆盖。单个消息为单独字符串，多个msg_key也使用json数组表示。特别提醒：该功能只支持android、browser、pc三种设备类型。。
        public uint? message_expires { get; set; }	//uint 	否 	指定消息的过期时间，默认为86400秒。必须和messages一一对应。
        public uint timestamp { get; set; }	//uint 	是 	用户发起请求时的unix时间戳。本次请求签名的有效时间为该时间戳+10分钟。
        public string sign { get; set; } 	//string 	是 	调用参数签名值，与apikey成对出现。
        public uint? expires { get; set; }//	uint 	否 	用户指定本次请求签名的失效时间。格式为unix时间戳形式。
        public uint? v { get; set; }	//uint 	否 	API版本号，默认使用最高版本。

        public uint? deploy_status { get; set; } //部署状态。指定应用当前的部署状态，可取值：1：开发状态 2：生产状态
    }
}