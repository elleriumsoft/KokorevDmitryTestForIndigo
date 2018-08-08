using Boo.Lang;
using Solution.Data;
using UnityEngine;

namespace Solution
{
    public class SolutionCalculateRecursive : ISolutionCalculate
    {
        private List<Moves> _moves;        
        
        public List<Moves> Get(int count)
        {
            _moves = new List<Moves>();            
            Hanoi(count, 0, 2, 1);
            foreach (var move in _moves)
            {
                move.Index = count - move.Index;
            }
            Debug.Log("Длина=" + _moves.Count);
            return _moves;
        }

        private void Hanoi(int index, int place1, int place2, int place3)
        {
            if (index > 0)
            {
                Hanoi(index - 1, place1, place3, place2);

                _moves.Add(new Moves(index, place2));                

                Hanoi(index - 1, place3, place2, place1);
            }
        }
    }


}