﻿namespace Basket.API
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
    }
}
