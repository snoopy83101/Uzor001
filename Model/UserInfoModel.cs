


using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model{
	 	//UserInfo
 	
			public class UserInfoModel
	{
	
	      public UserInfoModel()
	    {
	     
	    }
	
	
	   
   		     
      	/// <summary>
		/// 用户名(登录名)
        /// </summary>		
                        public string UserId
        {
            get;
            set;
        }        
		/// <summary>
		/// 用户昵称
        /// </summary>		
                        public string UserTitle
        {
            get;
            set;
        }        
		/// <summary>
		/// Sex
        /// </summary>		
                        public string Sex
        {
            get;
            set;
        }        
		/// <summary>
		/// 用户的真实姓名
        /// </summary>		
                        public string RealName
        {
            get;
            set;
        }        
		/// <summary>
		/// 电子邮箱
        /// </summary>		
                        public string Email
        {
            get;
            set;
        }        
		/// <summary>
		/// 备注
        /// </summary>		
                        public string Memo
        {
            get;
            set;
        }        
		/// <summary>
		/// 出生日期
        /// </summary>		
                        public DateTime Birthday
        {
            get;
            set;
        }        
		/// <summary>
		/// 注册日期
        /// </summary>		
                        public DateTime CreateTime
        {
            get;
            set;
        }        
		/// <summary>
		/// 密码
        /// </summary>		
                        public string Pwd
        {
            get;
            set;
        }        
		/// <summary>
		/// 密码的MD5编码
        /// </summary>		
                        public string PwdMd5
        {
            get;
            set;
        }        
		/// <summary>
		/// 电话
        /// </summary>		
                        public string Tell
        {
            get;
            set;
        }        
		/// <summary>
		/// 手机
        /// </summary>		
                        public string Phone
        {
            get;
            set;
        }        
		/// <summary>
		/// 是否通过实名验证
        /// </summary>		
                        public bool Validated
        {
            get;
            set;
        }        
		/// <summary>
		/// 身份证号
        /// </summary>		
                        public string IdNo
        {
            get;
            set;
        }        
		/// <summary>
		/// 用户所在的街道
        /// </summary>		
                        public decimal StreetId
        {
            get;
            set;
        }        
		/// <summary>
		/// 用户级别
        /// </summary>		
                        public int UserLv
        {
            get;
            set;
        }        
		/// <summary>
		/// 用户能量(积分)
        /// </summary>		
                        public int Power
        {
            get;
            set;
        }        
		/// <summary>
		/// 用户财富值(预留)
        /// </summary>		
                        public decimal Currency
        {
            get;
            set;
        }        
		/// <summary>
		/// 用户余额
        /// </summary>		
                        public decimal Money
        {
            get;
            set;
        }        
		/// <summary>
		/// 用户类型,qq用户(2),微博用户(3),注册用户(1)
        /// </summary>		
                        public decimal UserTypeId
        {
            get;
            set;
        }        
		/// <summary>
		/// 有无商家门户
        /// </summary>		
                        public bool FlagMerchant
        {
            get;
            set;
        }        
		/// <summary>
		/// 所属乡镇ID
        /// </summary>		
                        public decimal TownId
        {
            get;
            set;
        }        
		/// <summary>
		/// qq号码
        /// </summary>		
                        public string qq
        {
            get;
            set;
        }        
		/// <summary>
		/// 大头像
        /// </summary>		
                        public string PicBig
        {
            get;
            set;
        }        
		/// <summary>
		/// 中头像
        /// </summary>		
                        public string PicMid
        {
            get;
            set;
        }        
		/// <summary>
		/// 小头像
        /// </summary>		
                        public string PicSmall
        {
            get;
            set;
        }        
		/// <summary>
		/// 微信ID
        /// </summary>		
                        public string WxOpenID
        {
            get;
            set;
        }        
		/// <summary>
		/// UserCode
        /// </summary>		
                        public int UserCode
        {
            get;
            set;
        }        
		/// <summary>
		/// 昵称
        /// </summary>		
                        public string Nickname
        {
            get;
            set;
        }        
		   
	}
}


