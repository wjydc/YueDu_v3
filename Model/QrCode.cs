using Dapper.Contrib.Extensions;
using System;
namespace Model
{
    [Table("dbo.QrCode")]
    ///<summary>
    ///表QrCode的实体类
    ///</summary>
    public class QrCode
    {
        [IdentityKey]
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// funcType
        /// </summary>
        public int FuncType { get; set; }

        /// <summary>
        /// cnName
        /// </summary>
        public string CnName { get; set; }

        /// <summary>
        /// enName
        /// </summary>
        public string EnName { get; set; }

        /// <summary>
        /// path
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// sortId
        /// </summary>
        public int SortId { get; set; }

        /// <summary>
        /// status
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// creator
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// updateTime
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// addTime
        /// </summary>
        public DateTime AddTime { get; set; }

    }
}
