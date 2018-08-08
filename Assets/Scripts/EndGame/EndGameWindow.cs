using System;
using UnityEngine;
using Utils;

namespace EndGame
{
    public class EndGameWindow : MonoBehaviour
    {
        [SerializeField] private UISimpleButton _closeButton;
        [SerializeField] private UISimpleButton _againButton;
        
        public void Show(bool show)
        {
            gameObject.SetActive(show);
        }

        public void Listen(Action onClose, Action onAgain)
        {
            _closeButton.Listen(onClose);
            _againButton.Listen(onAgain);
        }

        public void Unlisten()
        {
            _closeButton.Unlisten();
            _againButton.Unlisten();
        }
    }
}