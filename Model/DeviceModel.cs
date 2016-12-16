


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //设备表

    public class DeviceModel
    {

        public DeviceModel()
        {

        }




        /// <summary>
        /// 设备编号,为apicloud中取出来的
        /// </summary>		
        public string DeviceId
        {
            get;
            set;
        }
        /// <summary>
        /// 商家ID
        /// </summary>		
        public decimal MerId
        {
            get;
            set;
        }
        /// <summary>
        /// 用户ID,如果匿名则为0
        /// </summary>		
        public decimal MemberId
        {
            get;
            set;
        }
        /// <summary>
        /// ios还是android
        /// </summary>		
        public string SystemType
        {
            get;
            set;
        }
        /// <summary>
        /// app的版本
        /// </summary>		
        public string AppVersion
        {
            get;
            set;
        }
        /// <summary>
        /// 操作系统的版本
        /// </summary>		
        public string SystemVersion
        {
            get;
            set;
        }
        /// <summary>
        /// 最后的活跃时间
        /// </summary>		
        public DateTime LastTime
        {
            get;
            set;
        }
        /// <summary>
        /// 融云的token
        /// </summary>		
        public string RongToken
        {
            get;
            set;
        }
        /// <summary>
        /// IpAddress
        /// </summary>		
        public string IpAddress
        {
            get;
            set;
        }
        /// <summary>
        /// 设备类别, 设备的typeid,web或者phone
        /// </summary>		
        public string DeviceTypeId
        {
            get;
            set;
        }
        /// <summary>
        /// 设备的硬件编号,目前只有手机有效
        /// </summary>		
        public string DeviceHardwareId
        {
            get;
            set;
        }
        /// <summary>
        /// 后台用户ID,当用户为后台客户时有效
        /// </summary>		
        public string UserId
        {
            get;
            set;
        }
        /// <summary>
        /// Lng
        /// </summary>		
        public decimal Lng
        {
            get;
            set;
        }
        /// <summary>
        /// Lat
        /// </summary>		
        public decimal Lat
        {
            get;
            set;
        }
        /// <summary>
        /// 未读消息的数量
        /// </summary>		
        public int MsgNum
        {
            get;
            set;
        }
        /// <summary>
        /// 在IM中互动的名字
        /// </summary>		
        public string DeviceName
        {
            get;
            set;
        }
        /// <summary>
        /// 互动头像, 在IM中沟通的头像
        /// </summary>		
        public string DevicePicImgId
        {
            get;
            set;
        }

    }
}


