using System;

namespace ByteDev.Crypto
{
    /// <summary>
    /// Represents a character's position in a string or array.
    /// </summary>
    public class CharacterPosition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Crypto.CharacterPosition" /> class.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="position">The position of the character.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="position" /> must be zero or greater.</exception>
        public CharacterPosition(char character, int position)
        {
            if(position < 0)
                throw new ArgumentOutOfRangeException(nameof(position), "Position must be zero or greater.");

            Character = character;
            Position = position;
        }

        /// <summary>
        /// Position of the character.
        /// </summary>
        public int Position { get; }

        /// <summary>
        /// The character.
        /// </summary>
        public char Character { get; }
    }
}