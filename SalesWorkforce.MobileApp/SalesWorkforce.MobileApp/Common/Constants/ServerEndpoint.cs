namespace SalesWorkforce.MobileApp.Common.Constants
{
    public static class ServerEndpoint
    {
        public const string AuthLogin = "api/auth/login";
        public const string AuthGetProfile = "api/auth/me";

        public const string CustomerQuery = "api/customer/search?query={0}";
        public const string CustomerGet = "api/customer?recordId={0}";
        public const string CustomerCreate = "api/customer/create";

        public const string ProductQuery = "api/product/search?query={0}";
        public const string ProductGet = "api/product?recordId={0}";

        public const string PurchaseOrderGetAll = "api/purchase-order/get-all";
        public const string PurchaseOrderGet = "api/purchase-order?recordId={0}";
        public const string PurchaseOrderCreate = "api/purchase-order/create";
    }
}
