using System;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Question
{
    public class QuestionWindow : MonoBehaviour
    {
        [SerializeField] private UISimpleButton _startButton;
        [SerializeField] private UISimpleButton _closeButton;

        [Header("Selector")]
        [SerializeField] private UISimpleButton _upButton; 
        [SerializeField] private UISimpleButton _downButton;
        [SerializeField] private Text _count;
        
        public void Listen(Action onClose, Action onStart, Action onUp, Action onDown)
        {
            _closeButton.Listen(onClose);
            _startButton.Listen(onStart);
            _upButton.Listen(onUp);
            _downButton.Listen(onDown);
        }

        public void Unlisten()
        {
            _closeButton.Unlisten();
            _startButton.Unlisten();
            _upButton.Unlisten();
            _downButton.Unlisten();
        }
        
        public void Show(bool show)
        {
            gameObject.SetActive(show);
        }

        public void SetCount(int count)
        {
            _count.text = count.ToString("D2");
        }
    }
}