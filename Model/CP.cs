using Dapper.Contrib.Extensions;
using System;
namespace Model
{
    [Table("[dbo].[CP]")]
    ///<summary>
    ///表CP的实体类
    ///</summary>
    public class CP
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// classId
        /// </summary>
        public int ClassId { get; set; }

        /// <summary>
        /// cnName
        /// </summary>
        public string CnName { get; set; }

        /// <summary>
        /// enName
        /// </summary>
        public string EnName { get; set; }

        /// <summary>
        /// totalProductCount
        /// </summary>
        public int TotalProductCount { get; set; }

        /// <summary>
        /// feeProductCount
        /// </summary>
        public int FeeProductCount { get; set; }

        /// <summary>
        /// freeProductCount
        /// </summary>
        public int FreeProductCount { get; set; }

        /// <summary>
        /// incomeShare
        /// </summary>
        public decimal IncomeShare { get; set; }

        /// <summary>
        /// taxRate
        /// </summary>
        public decimal TaxRate { get; set; }

        /// <summary>
        /// coefficient
        /// </summary>
        public decimal Coefficient { get; set; }

        /// <summary>
        /// beginTime
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// endTime
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// displayType
        /// </summary>
        public int DisplayType { get; set; }

        /// <summary>
        /// description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// userName
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// tel
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// status
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// creator
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// addTime
        /// </summary>
        public DateTime AddTime { get; set; }

        /// <summary>
        /// updateTime
        /// </summary>
        public DateTime UpdateTime { get; set; }

    }
}