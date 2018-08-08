using System;

namespace Solution.Data
{
    [Serializable]
    public class Ring
    {
        public int Place;
        public int Position;

        public Ring(int place, int position)
        {
            Place = place;
            Position = position;
        }
    }
}