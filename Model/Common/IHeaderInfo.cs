namespace Model.Common
{
    public interface IHeaderInfo
    {
        string ClientVersion { set; get; }
        int ClientId { set; get; }
        string ChannelId { set; get; }
        string RouteChannelId { set; get; }
        string PromotionCode { set; get; }
        string Tel { set; get; }
        string IMEI { set; get; }
        string IMSI { set; get; }
        string UserAgent { set; get; }
        int SourceType { set; get; }
    }
}