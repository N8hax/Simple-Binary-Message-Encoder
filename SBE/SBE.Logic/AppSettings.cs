namespace SBE.Logic
{
    public static class AppSettings
    {
        public static readonly int MESSAGE_PAYLOAD_MAX_SIZE_IN_BYTES = 256 * 1024;
        public static readonly int MESSAGE_HEADERS_MAX_COUNT = 63;
        public static readonly int MESSAGE_HEADER_NAME_MAX_SIZE_IN_BYTES = 1023;
        public static readonly int MESSAGE_HEADER_VALUE_MAX_SIZE_IN_BYTES = 1023;
    }
}
