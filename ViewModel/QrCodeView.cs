using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class QrCodeView
    {
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
        /// UpdateTime
        /// </summary>
        public DateTime UpdateTime { get; set; }

        public string ReplyText { get; set; }

        public string WeChatPubId { get; set; }
    }
}