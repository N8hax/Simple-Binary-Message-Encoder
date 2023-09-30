using SBE.Logic.Abstraction;

namespace SBE.Logic.Services
{
    public class ASCIIEncoder : IEncoder
    {
        public byte[] Encode(string input)
        {
            var result = new byte[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                result[i] = input[i] < 128 ? (byte)input[i] : throw new ArgumentException("non ASCII character in input string: " + input[i]);
            }
            return result;
        }
    }
}
