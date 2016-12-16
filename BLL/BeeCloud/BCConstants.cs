
namespace BeeCloud
{
    internal static class BCConstants
    {
        public const string version = "/2/";

        //new API
        public const string billURL = "rest/bill";
        public const string billsURL = "rest/bills";
        public const string billsCountURL = "rest/bills/count";
        public const string refundURL = "rest/refund";
        public const string refundsURL = "rest/refunds";
        public const string refundsCountURL = "rest/refunds/count";
        public const string refundStatusURL = "rest/refund/status";
        public const string transferURL = "rest/transfer";
        public const string transfersURL = "rest/transfers";
        public const string internationalURL = "rest/international/bill";

        //test mode API
        public const string billTestURL = "rest/sandbox/bill";
        public const string billsTestURL = "rest/sandbox/bills";
    }
}
