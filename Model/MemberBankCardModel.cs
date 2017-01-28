


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //MemberBankCard

    public class MemberBankCardModel
    {

        public MemberBankCardModel()
        {

        }




        /// <summary>
        /// MemberBankCardId
        /// </summary>		
        public decimal MemberBankCardId
        {
            get;
            set;
        }
        /// <summary>
        /// BankCardCode
        /// </summary>		
        public string BankCardCode
        {
            get;
            set;
        }
        /// <summary>
        /// BankName
        /// </summary>		
        public string BankName
        {
            get;
            set;
        }
        /// <summary>
        /// BankCardName
        /// </summary>		
        public string BankCardName
        {
            get;
            set;
        }
        /// <summary>
        /// OrderNo
        /// </summary>		
        public int OrderNo
        {
            get;
            set;
        }
        /// <summary>
        /// MemberId
        /// </summary>		
        public decimal MemberId
        {
            get;
            set;
        }
        /// <summary>
        /// BankCardAccount
        /// </summary>		
        public string BankCardAccount
        {
            get;
            set;
        }
        /// <summary>
        /// CreateTime
        /// </summary>		
        public DateTime CreateTime
        {
            get;
            set;
        }
        /// <summary>
        /// 比如工商银行是IDBC
        /// </summary>		
        public string BankId
        {
            get;
            set;
        }

    }
}


