using System;
using System.Collections.Generic;
using System.Linq;

namespace ByteDev.Crypto
{
    /// <summary>
    /// Checks whether certain characters are present at certain positions in strings and char arrays.
    /// </summary>
    public static class CharacterPositionChecker
    {
        /// <summary>
        /// Verify that a set of characters appear within a phrase at certain positions.
        /// </summary>
        /// <param name="phrase">The phrase to check.</param>
        /// <param name="characterPositions">The characters and their positions.</param>
        /// <returns>True if the characters exist at the positions; otherwise returns false.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="phrase" /> is null.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="phrase" /> is empty.</exception>
        public static bool Verify(string phrase, IEnumerable<CharacterPosition> characterPositions)
        {
            if(phrase == null)
                throw new ArgumentNullException(nameof(phrase));

            if(phrase == string.Empty)
                throw new ArgumentException("Phrase is empty.", nameof(phrase));

            CheckParams(characterPositions);

            return characterPositions.All(cp => phrase[cp.Position] == cp.Character);
        }

        /// <summary>
        /// Verify that a set of characters appear within a phrase at certain positions.
        /// </summary>
        /// <param name="phrase">The phrase to check.</param>
        /// <param name="characterPositions">The characters and their positions.</param>
        /// <returns>True if the characters exist at the positions; otherwise returns false.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="phrase" /> is null.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="phrase" /> is empty.</exception>
        public static bool Verify(char[] phrase, IEnumerable<CharacterPosition> characterPositions)
        {
            if (phrase == null)
                throw new ArgumentNullException(nameof(phrase));

            if (!phrase.Any())
                throw new ArgumentException("Phrase is empty.", nameof(phrase));

            CheckParams(characterPositions);

            return characterPositions.All(cp => phrase[cp.Position] == cp.Character);
        }

        private static void CheckParams(IEnumerable<CharacterPosition> characterPositions)
        {
            if (characterPositions == null)
                throw new ArgumentNullException(nameof(characterPositions));

            if (!characterPositions.Any())
                throw new ArgumentException("Character positions is empty.", nameof(characterPositions));
        }
    }
}