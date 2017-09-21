using Model;
using System.Collections.Generic;

namespace ViewModel
{
    /// <summary>
    /// 充值
    /// </summary>
    public class RechargeView
    {
        /// <summary>
        /// 充值方式
        /// </summary>
        public PayInfo PayInfo { get; set; }

        /// <summary>
        /// 充值配置
        /// </summary>
        public List<RechargeFeeConfigInfo> PayItems { get; set; }
    }

    public class PayInfo
    {
        public PayInfo(string name, int payType, string fPayType, string description = "")
        {
            this.Name = name;
            this.PayType = payType;
            this.Description = description;
            this.FPayType = fPayType;
        }

        private string _name;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private int _payType;

        /// <summary>
        /// 充值类型
        /// </summary>
        public int PayType
        {
            get { return _payType; }
            set { _payType = value; }
        }

        private string _description;

        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private string _fPayType;

        /// <summary>
        /// 充值类型（第三方）
        /// </summary>
        public string FPayType
        {
            get { return _fPayType; }
            set { _fPayType = value; }
        }
    }
}