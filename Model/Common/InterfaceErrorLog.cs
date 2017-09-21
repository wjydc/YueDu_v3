using Dapper.Contrib.Extensions;
using System;

namespace Model.Common
{
    [Table("dbo.InterfaceErrorLog")]
    public class InterfaceErrorLog
    {
        [IdentityKey]
        public long Id { get; set; }

        public string Module { get; set; }
        public string Method { get; set; }
        public string Error { get; set; }
        public string Brief { get; set; }
        public DateTime AddTime { get; set; }
    }
}