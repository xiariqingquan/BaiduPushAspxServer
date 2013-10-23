/* 
    Copyright (c) 2011 Microsoft Corporation.  All rights reserved.
    Use of this sample source code is subject to the terms of the Microsoft license 
    agreement under which you licensed this sample source code and is provided AS-IS.
    If you did not accept the terms of the license agreement, you are not authorized 
    to use this sample source code.  For the terms of the license, please see the 
    license agreement between you and Microsoft.
  
    To see all Code Samples for Windows Phone, visit http://go.microsoft.com/fwlink/?LinkID=219604 
  
*/
using System;
using System.Net;
using System.IO;
using System.Text;

namespace PushAspxDemo
{
    public partial class PushAspxDemo : System.Web.UI.Page
    {

        protected void BtnSend_Click(object sender, EventArgs e)
        {
            try
            {
                string sk = TBSK.Text;
                string ak = TBAK.Text;


                BaiduPush Bpush = new BaiduPush("POST", sk);
                String apiKey = ak;
                String messages="";
                String method = "push_msg";
                TimeSpan ts = (DateTime.UtcNow - new DateTime(1970,1,1,0,0,0));
                uint device_type=3;
                uint unixTime = (uint)ts.TotalSeconds;

                uint message_type;
                string messageksy="xxxxxx";
                if (RbMessage.Checked)
                {
                    message_type = 0;
                    messages = TBMessage.Text;
                }
                else
                {
                    message_type = 1;

                    if (RBIOSPRO.Checked == true || RBIOSDEV.Checked == true)
                    {
                        device_type = 4;
                        IOSNotification notification = new IOSNotification();
                        notification.title = TBTitle.Text;
                        notification.description = TBDescription.Text;
                        messages = notification.getJsonString();
                    }
                    else
                    {
                        BaiduPushNotification notification = new BaiduPushNotification();
                        notification.title = TBTitle.Text;
                        notification.description = TBDescription.Text;
                        messages = notification.getJsonString();
                    }
                }


                PushOptions pOpts;
                if(RBUnicast.Checked)
                {
                    pOpts = new PushOptions(method, apiKey, TBUserId.Text, TBChannelID.Text, device_type,messages, messageksy, unixTime);
                }else if(RBMulticast.Checked)
                {
                    pOpts = new PushOptions(method, apiKey, TBTag.Text, device_type, messages, messageksy, unixTime);
                }else
                {
                    pOpts = new PushOptions(method, apiKey, device_type, messages, messageksy, unixTime);
                }

                pOpts.message_type = message_type;
                if (RBIOSPRO.Checked == true)
                {
                     pOpts.deploy_status = 2;
                }
                else if(RBIOSDEV.Checked == true)
                {
                    pOpts.deploy_status = 1;
                }
               

                string response= Bpush.PushMessage(pOpts);

                TextBoxResponse.Text = response;


            }
            catch (Exception ex)
            {
                TextBoxResponse.Text = "Exception caught sending update: " + ex.ToString();
            }
        }
    }
}
