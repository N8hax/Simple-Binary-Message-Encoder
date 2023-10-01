using SBE.Logic.Abstraction;

namespace SBE.Logic.Services
{
    /// <summary>
    /// Represents an encoder that converts a string into a byte array containing ASCII-encoded data.
    /// </summary>
    public class ASCIIEncoder : IEncoder
    {
        /// <summary>
        /// Encodes a string into a byte array containing ASCII-encoded data.
        /// </summary>
        /// <param name="input">The string to encode.</param>
        /// <returns>A byte array representing the ASCII-encoded input string.</returns>
        /// <exception cref="ArgumentException">Thrown if the input contains non-ASCII characters.</exception>
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
