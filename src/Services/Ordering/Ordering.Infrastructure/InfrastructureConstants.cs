namespace Ordering.Infrastructure
{
    public static class InfrastructureConstants
    {
        public const string ORDER_REPOSITORY_CONNECTION_STRING_KEY = "OrderingServerDb";
        public const string EMAIL_SETTINGS_CONFIG_KEY = "EmailSettings";

        public const string MESSAGE_BUS_SETTINGS_SECTION_CONFIG = "EventBusSettings";
        public const string MESSAGE_BUS_HOSTADDRESS_KEY_CONFIG = "HostAddress";
    }
}
