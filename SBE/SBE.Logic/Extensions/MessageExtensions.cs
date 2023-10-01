using SBE.Logic.Models;

namespace SBE.Logic.Extensions
{
    public static class MessageExtensions
    {
        public static string ToReadableString(this byte[] bytes)
        {
            var result = "[";
            foreach (var b in bytes)
            {
                result += $"{b}, ";
            }
            result = result.Substring(0, result.Length-2) + "]";
            return result;
        }
        public static string ToReadableString(this Message message)
        {
            var headers = "[";
            foreach (var h in message.Headers)
            {
                headers += $"({h.Key},{h.Value}), ";
            }
            headers = headers.Substring(0, headers.Length - 2) + "]";
            return $"{{Headers: {headers}, Payload: {message.Payload.ToReadableString()}}}";
        }
    }
}
