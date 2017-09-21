using System;

namespace Model
{
	[Serializable]
	public class RechargeFeeConfigInfo
	{
		#region Fields
        
		private int _id;
		private int _payType = 0;
		private decimal _cash;
		private decimal _fee;
		private decimal _rebateFee;
		private string _brief = String.Empty;
		private string _goodsId = String.Empty;
		private int _sortId = 0;
		private int _status = 0;
		private DateTime _addTime;
        
		#endregion
        
        #region Contructors
        
		public RechargeFeeConfigInfo()
		{
        
		}
		
		public RechargeFeeConfigInfo
		(
			int id,
			int payType,
			decimal cash,
			decimal fee,
			decimal rebateFee,
			string brief,
			string goodsId,
			int sortId,
			int status,
			DateTime addTime
		)
		{
			_id        = id;
			_payType   = payType;
			_cash      = cash;
			_fee       = fee;
			_rebateFee = rebateFee;
			_brief     = brief;
			_goodsId   = goodsId;
			_sortId    = sortId;
			_status    = status;
			_addTime   = addTime;
			
		}
        
		#endregion
		
		#region Public Properties
		
        /// <summary>
        /// Id
		/// </summary>
		public int Id
		{
			get {return _id;}
			set {_id = value;}
		}

        /// <summary>
        /// PayType
		/// </summary>
		public int PayType
		{
			get {return _payType;}
			set {_payType = value;}
		}

        /// <summary>
        /// Cash
		/// </summary>
		public decimal Cash
		{
			get {return _cash;}
			set {_cash = value;}
		}

        /// <summary>
        /// Fee
		/// </summary>
		public decimal Fee
		{
			get {return _fee;}
			set {_fee = value;}
		}

        /// <summary>
        /// RebateFee
		/// </summary>
		public decimal RebateFee
		{
			get {return _rebateFee;}
			set {_rebateFee = value;}
		}

        /// <summary>
        /// ?????
		/// </summary>
		public string Brief
		{
			get {return _brief;}
			set {_brief = value;}
		}

        /// <summary>
        /// GoodsId
		/// </summary>
		public string GoodsId
		{
			get {return _goodsId;}
			set {_goodsId = value;}
		}

        /// <summary>
        /// SortId
		/// </summary>
		public int SortId
		{
			get {return _sortId;}
			set {_sortId = value;}
		}

        /// <summary>
        /// Status
		/// </summary>
		public int Status
		{
			get {return _status;}
			set {_status = value;}
		}

        /// <summary>
        /// AddTime
		/// </summary>
		public DateTime AddTime
		{
			get {return _addTime;}
			set {_addTime = value;}
		}
        
		#endregion
	}
}

