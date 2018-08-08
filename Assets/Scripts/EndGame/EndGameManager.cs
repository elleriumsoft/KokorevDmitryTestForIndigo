using Installers;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace EndGame
{
    public class EndGameManager : IWindow
    {
        private readonly EndGameWindow _window;
        private readonly SignalBus _signal;

        public EndGameManager(EndGameWindow window,
                              SignalBus signal)
        {
            _window = window;
            _signal = signal;
        }

        public Views GetId() { return Views.EndGame; }

        public void Run()
        {
            _window.Show(true);
            _window.Listen(OnClose, OnAgain);
        }

        private void OnAgain()
        {
            CloseWindow();
            _signal.Fire(new OpenView { Name = Views.Question });
        }

        private void CloseWindow()
        {
            _window.Show(false);
            _window.Unlisten();
        }

        private void OnClose()
        {
#if !UNITY_EDITOR            
            Application.Quit();
#else
            EditorApplication.isPlaying = false;
#endif            
        }
    }
}