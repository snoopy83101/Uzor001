using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cn.jpush.api;
using cn.jpush.api.push;
using cn.jpush.api.report;
using cn.jpush.api.common;
using cn.jpush.api.util;
using cn.jpush.api.push.mode;
using cn.jpush.api.push.notification;
using cn.jpush.api.common.resp;
namespace BLL
{
    public class JPushBLL
    {


        /// <summary>
        /// 发送推送
        /// </summary>
        /// <param name="alert">推送内容</param>
        /// <param name="MemberIds">推送到的用户用逗号分隔, 如果留空则为全部用户</param>
        /// <param name="MerId">商家ID</param>
        public void SendPush(string alert, string MemberIds, string eventStr, decimal MerId, string[] tag)
        {
            string[] sa = { "JPushAppKey", "JPushMasterSecret" };

            Dictionary<string, string> MerConfig = BLL.StaticBLL.MerConfig(MerId, sa);

            JPushClient client = new JPushClient(MerConfig["JPushAppKey"], MerConfig["JPushMasterSecret"]);

            PushPayload pushPayload = new PushPayload();
            pushPayload.platform = Platform.all();
            var notification = new Notification().setAlert(alert);
            AndroidNotification androidNotification = new AndroidNotification();
            IosNotification iosNotification = new IosNotification();
            Options options = new Options();
            options.apns_production = true; //生产环境的

            pushPayload.options = options;
            androidNotification.AddExtra("eventStr", eventStr);
            iosNotification.AddExtra("eventStr", eventStr);
            notification.setAndroid(androidNotification);
            notification.setIos(iosNotification);
            if (MemberIds.Trim() != "")
            {
                //如果不为空,说明指定了MemberId
                string[] MemberArray = MemberIds.Split(',');

                pushPayload.audience = Audience.s_alias(MemberArray); //推送设备对象，表示一条推送可以被推送到那些设备，确认推送设备的对象，JPush提供了多种方式，比如：别名，标签，注册id，分群，广播等。

            }

            else if (tag != null)
            {
                if (tag.Length > 0)
                {
                    pushPayload.audience = Audience.s_tag(tag);  //按照标签推送
                }

            }

            else
            {
                pushPayload.audience = Audience.all();//推送设备对象，表示一条推送可以被推送到那些设备，确认推送设备的对象，JPush提供了多种方式，比如：别名，标签，注册id，分群，广播等。
            }

            pushPayload.notification = notification;



            pushPayload.message = Message.content("msg")
                                      .AddExtras("DoEvent", "GetNewMsgNum()");  //如果不加一条自定义消息的话, android是不会触发监听事件的.但是IOS可以





            var result = client.SendPush(pushPayload);
        }

        //public static PushPayload PushObject_All_All_Alert()
        //{
        //    PushPayload pushPayload = new PushPayload();
        //    pushPayload.platform = Platform.all();
        //    pushPayload.audience = Audience.all();

        //    pushPayload.notification = new Notification().setAlert("");
        //    return pushPayload;
        //}
        //public static PushPayload PushObject_all_alias_alert()
        //{

        //    PushPayload pushPayload = new PushPayload();
        //    pushPayload.platform = Platform.android();
        //    pushPayload.audience = Audience.s_alias("alias1");
        //    pushPayload.notification = new Notification().setAlert(ALERT);
        //    return pushPayload;

        //}
        //public static PushPayload PushObject_Android_Tag_AlertWithTitle()
        //{
        //    PushPayload pushPayload = new PushPayload();

        //    pushPayload.platform = Platform.android();
        //    pushPayload.audience = Audience.s_tag("tag1");
        //    pushPayload.notification = Notification.android(ALERT, TITLE);

        //    return pushPayload;
        //}
        //public static PushPayload PushObject_android_and_ios()
        //{
        //    PushPayload pushPayload = new PushPayload();
        //    pushPayload.platform = Platform.android_ios();
        //    var audience = Audience.s_tag("tag1");
        //    pushPayload.audience = audience;
        //    var notification = new Notification().setAlert("alert content");
        //    notification.AndroidNotification = new AndroidNotification().setTitle("Android Title");
        //    notification.IosNotification = new IosNotification();
        //    notification.IosNotification.incrBadge(1);
        //    notification.IosNotification.AddExtra("extra_key", "extra_value");

        //    pushPayload.notification = notification.Check();


        //    return pushPayload;
        //}
        //public static PushPayload PushObject_ios_tagAnd_alertWithExtrasAndMessage()
        //{
        //    PushPayload pushPayload = new PushPayload();
        //    pushPayload.platform = Platform.android_ios();
        //    pushPayload.audience = Audience.s_tag_and("tag1", "tag_all");
        //    var notification = new Notification();
        //    notification.IosNotification = new IosNotification().setAlert(ALERT).setBadge(5).setSound("happy").AddExtra("from", "JPush");

        //    pushPayload.notification = notification;
        //    pushPayload.message = Message.content(MSG_CONTENT);
        //    return pushPayload;

        //}
        //public static PushPayload PushObject_ios_audienceMore_messageWithExtras()
        //{

        //    var pushPayload = new PushPayload();
        //    pushPayload.platform = Platform.android_ios();
        //    pushPayload.audience = Audience.s_tag("tag1", "tag2");
        //    pushPayload.message = Message.content(MSG_CONTENT).AddExtras("from", "JPush");
        //    return pushPayload;

        //}

    }
}
