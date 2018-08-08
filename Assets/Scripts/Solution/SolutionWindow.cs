using System;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Solution
{
    public class SolutionWindow : MonoBehaviour
    {
        [Header("Ходы")]
        [SerializeField] private Text _allMovesCount;
        [SerializeField] private Text _remainingMovesCount;

        [Header("Скорость")]
        [SerializeField] private Text _speedText;
        [SerializeField] private UISimpleButton _minusButton;
        [SerializeField] private UISimpleButton _plusButton;        

        public void SetMoves(int all, int remaining)
        {
            _allMovesCount.text = all.ToString("D4");
            _remainingMovesCount.text = remaining.ToString("D4");            
        }

        public void SetSpeed(float speed)
        {
            _speedText.text = speed.ToString("F2");
        }
        
        public void Show(bool show)
        {
            gameObject.SetActive(show);
        }
        
        public void Listen(Action onMinus, Action onPlus)
        {
            _minusButton.Listen(onMinus);
            _plusButton.Listen(onPlus);            
        }

        public void Unlisten()
        {
            _minusButton.Unlisten();
            _plusButton.Unlisten();                        
        }        
    }
}