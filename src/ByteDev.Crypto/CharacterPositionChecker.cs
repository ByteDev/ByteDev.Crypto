using System;
using System.Collections.Generic;
using System.Linq;

namespace ByteDev.Crypto
{
    public static class CharacterPositionChecker
    {
        public static bool Verify(string phrase, IEnumerable<CharacterPosition> characterPositions)
        {
            if(phrase == null)
                throw new ArgumentNullException(nameof(phrase));

            if(phrase == string.Empty)
                throw new ArgumentException("Phrase is empty.", nameof(phrase));

            CheckParams(characterPositions);

            return characterPositions.All(cp => phrase[cp.Position] == cp.Character);
        }

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