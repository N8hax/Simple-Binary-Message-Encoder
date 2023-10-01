using SBE.Logic.Abstraction;

namespace SBE.Logic.Services
{
    /// <summary>
    /// Represents a decoder that converts a byte array containing ASCII-encoded data into a string.
    /// </summary>
    public class ASCIIDecoder : IDecoder
    {
        /// <summary>
        /// Decodes a byte array containing ASCII-encoded data into a string.
        /// </summary>
        /// <param name="input">The byte array to decode.</param>
        /// <returns>A string representing the decoded ASCII data.</returns>
        /// <exception cref="ArgumentException">Thrown if the input contains invalid ASCII values.</exception>
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
