namespace Basket.API
{
    public static class AppConstants
    {
        public const string ERROR_RESPONSE_CONTENT_TYPE = "application/json";

        public const string SWAGGER_URL = "/swagger/v1/swagger.json";
        public const string SWAGGER_APP_NAME = "Basket.API";
        public const string SWAGGER_APP_VERSION = "v1";
        public const string SWAGGER_NAME_FORMAT = "{0} {1}";

        public const string REDIS_CACHE_SETTINGS_SECTION_CONFIG = "CacheSettings";
        public const string REDIS_CONNECTION_STRING_KEY_CONFIG = "ConnectionString";

        public const string GRPC_SETTINGS_SECTION_CONFIG = "GrpcSettings";
        public const string DISCOUNT_URL_GRPC_CONFIG_KEY_CONFIG = "DiscountUrl";

        public const string MESSAGE_BUS_SETTINGS_SECTION_CONFIG = "EventBusSettings";
        public const string MESSAGE_BUS_HOSTADDRESS_KEY_CONFIG = "HostAddress";
    }
}
