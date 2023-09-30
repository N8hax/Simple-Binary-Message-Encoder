using SBE.Logic.Abstraction;

namespace SBE.Logic.Services
{
    public class ASCIIDecoder : IDecoder
    {
        public string Decode(byte[] input)
        {
            var result = string.Empty;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] < 0 || input[i] > 127)
                {
                    throw new ArgumentException("invalid ASCII value in input: " + (int)input[i]);
                }
                result += (char)input[i];
            }
            return result;
        }
    }
}
