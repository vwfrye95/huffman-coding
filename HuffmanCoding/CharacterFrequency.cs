using System;

namespace HuffmanCoding
{
    public class CharacterFrequency : IComparable
    {
        private char character;
        private int frequency;

        public char Character
        {
            get
            {
                return character;
            }
            set
            {
                character = value;
            }
        }

        public int Frequency
        {
            get { return frequency; }
            set
            {
                if (value >= 0) frequency = value;
                else throw new ArgumentOutOfRangeException();
            }
        }

        public CharacterFrequency()
        {
            Character = '\0';
            Frequency = 0;
        }

        public CharacterFrequency(char ch)
        {
            Character = ch;
            Frequency = 0;
        }

        public CharacterFrequency(char ch, int count)
        {
            Character = ch;
            Frequency = count;
        }

        public CharacterFrequency(CharacterFrequency cf)
        {
            Character = cf.Character;
            Frequency = cf.Frequency;
        }

        public void Increment()
        {
            frequency++;
        }

        public static CharacterFrequency operator +(CharacterFrequency firstAddend, CharacterFrequency secondAddend)
        {
            CharacterFrequency sum = new CharacterFrequency();

            if (firstAddend.Character == secondAddend.Character)
            {
                sum.Character = firstAddend.Character;
                sum.Frequency = firstAddend.Frequency + secondAddend.Frequency;
            }
            else
            {
                sum.Character = '\0';
                sum.Frequency = firstAddend.Frequency + secondAddend.Frequency;
            }

            return sum;
        }

        public static Boolean operator <(CharacterFrequency firstTerm, CharacterFrequency secondTerm)
        {
            return firstTerm.Frequency < secondTerm.Frequency;
        }

        public static Boolean operator >(CharacterFrequency firstTerm, CharacterFrequency secondTerm)
        {
            return firstTerm.Frequency > secondTerm.Frequency;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            if (obj == this) return 0;

            CharacterFrequency cf = obj as CharacterFrequency;
            if (cf != null)
                return this.frequency.CompareTo(cf.frequency);
            else throw new ArgumentException("Object is not a Character Frequency");
        }

        public override Boolean Equals(object obj)
        {
            if (obj == null) return false;
            if (obj == this) return true;
            if (!(obj.GetType() == GetType())) return false;

            CharacterFrequency cf = obj as CharacterFrequency;

            return Character.Equals(cf.Character);
        }

        public override int GetHashCode()
        {
            return (int)Character;
        }

        public override string ToString()
        {
            return String.Format($"{character}|{(int)character}|{frequency}");
        }
    }
}
