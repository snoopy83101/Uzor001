


using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //SubjectInfo

    public class SubjectInfoModel
    {

        public SubjectInfoModel()
        {

        }




        /// <summary>
        /// SubjectId
        /// </summary>		
        public decimal SubjectId
        {
            get;
            set;
        }
        /// <summary>
        /// SubjectTitle
        /// </summary>		
        public string SubjectTitle
        {
            get;
            set;
        }
        /// <summary>
        /// SubjectContent
        /// </summary>		
        public string SubjectContent
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
        /// SubjectStatusId
        /// </summary>		
        public int SubjectStatusId
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
        /// ReKey
        /// </summary>		
        public string ReKey
        {
            get;
            set;
        }
        /// <summary>
        /// ReKey2
        /// </summary>		
        public string ReKey2
        {
            get;
            set;
        }
        /// <summary>
        /// Memo
        /// </summary>		
        public string Memo
        {
            get;
            set;
        }
        /// <summary>
        /// DoneTime
        /// </summary>		
        public DateTime DoneTime
        {
            get;
            set;
        }
        /// <summary>
        /// SubjectClassId
        /// </summary>		
        public int SubjectClassId
        {
            get;
            set;
        }

    }
}


