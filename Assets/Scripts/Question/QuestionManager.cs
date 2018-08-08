using System.Security.Cryptography.X509Certificates;
using DefaultNamespace;
using Installers;
using Solution;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Question
{
    public class QuestionManager : IWindow
    {
        private readonly QuestionWindow _window;
        private readonly GameSettings _settings;
        private readonly SignalBus _signal;       

        private int _count;

        public QuestionManager(QuestionWindow window,
                               GameSettings settings,
                               SignalBus signal
                               )
        {
            _window = window;
            _settings = settings;
            _signal = signal;            
        }

        public Views GetId() { return Views.Question; }

        public void Run()
        {
            _window.Show(true);
            _window.Listen(OnClose, OnStart, OnCountUp, OnCountDown);
            _count = _settings.MinDiscs;
            Refresh();
        }

        private void Refresh()
        {
            _window.SetCount(_count);
        }

        private void OnCountDown()
        {
            if (_count <= _settings.MinDiscs) return;
            _count--;
            Refresh();
        }

        private void OnCountUp()
        {
            if (_count >= _settings.MaxDiscs) return;
            _count++;
            Refresh();
        }

        private void CloseWindow()
        {
            _window.Unlisten();
            _window.Show(false);
        }

        private void OnStart()
        {            
            CloseWindow();
            _settings.SelectedCount = _count;
            _signal.Fire(new OpenView { Name = Views.Solution});        
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