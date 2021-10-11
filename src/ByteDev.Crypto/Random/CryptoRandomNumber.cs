namespace ByteDev.Crypto.Random
{
    /// <summary>
    /// Represents a generator of cryptographically random numbers.
    /// </summary>
    public class CryptoRandomNumber : CryptoRandomBase
    {
        /// <summary>
        /// Generates a random Int32 value.
        /// </summary>
        /// <returns>Random Int32 value.</returns>
        public int GenerateInt32()
        {
            return _rng.GetInt32();
        }
        
        /// <summary>
        /// Generates a random Int32 value between a given minimum and maximum.
        /// </summary>
        /// <param name="minValue">Minimum possible value.</param>
        /// <param name="maxValue">Maximum possible value.</param>
        /// <returns>Random Int32 value.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="minValue" /> was greater than max.</exception>
        public int GenerateInt32(int minValue, int maxValue)
        {
            return GetRandomInt32(minValue, maxValue);
        }
    }
}