using System;

namespace ByteDev.Crypto
{
    /// <summary>
    /// Represents a character's position in a string or array.
    /// </summary>
    public class CharacterPosition
    {
        public CharacterPosition(char character, int position)
        {
            if(position < 0)
                throw new ArgumentOutOfRangeException(nameof(position), "Position must be zero or greater than.");

            Character = character;
            Position = position;
        }

        public int Position { get; }

        public char Character { get; }
    }
}