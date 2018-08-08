using Boo.Lang;
using Installers;
using Solution.Data;
using UnityEngine;
using Zenject;

namespace Solution
{
    public class SolutionManager : IWindow
    {
        private readonly SolutionView _view;
        private readonly SolutionWindow _window;
        private readonly GameSettings _settings;
        private readonly SignalBus _signal;
        private ISolutionCalculate _calculate;

        private Ring[] _rings;
        private List<Moves> _moves;
        private bool _waiting;
        
        private int _current;        
        private int _count;        

        public SolutionManager(SolutionView view,
                               SolutionWindow window,
                               GameSettings settings,
                               SignalBus signal)
        {
            _view = view;
            _window = window;
            _settings = settings;
            _signal = signal;
            _calculate = new SolutionCalculateRecursive();
        }

        public Views GetId() { return Views.Solution; }

        public void Run()
        {
            _window.Show(true);
            _window.Listen(OnSpeedMinus, OnSpeedPlus);
            _view.Show(true);
            _view.Listen(OnEndMove);
                                                
            _settings.CurrentSpeed = _settings.StartSpeed;            
            SpeedRefresh();

            _count = _settings.SelectedCount;
            InitialiazeRins();
            _current = _count - 1;
            StartMoving();
        }

        private void InitialiazeRins()
        {
            _rings = new Ring[_count];
            for (int i = 0; i < _rings.Length; i++)
            {
                _rings[i] = new Ring(0, i);                
            }
            _view.CreateRings(_rings);
        }

        private void StartMoving()
        {           
            if (_waiting) return;
            _waiting = true;            
            _moves = _calculate.Get(_count);            
            _current = 0;
            Move(_moves[0]);
        }

        private void Move(Moves move)
        {            
            _view.Move(move.Index, move.ToPlace, GetFreePosition(move.ToPlace));
            _window.SetMoves(_moves.Count, _moves.Count - _current);
        }

        private void OnEndMove(int index, int place, int position)
        {            
            _rings[index].Place = place;
            _rings[index].Position = position;
            _current++;
//            Debug.Log("current = " + _current);            
            if (_current >= _moves.Count)
            {
                Debug.Log("end moves");
                _window.SetMoves(_moves.Count, _moves.Count - _current);
                _waiting = false;
                CloseWindow();
                _signal.Fire(new OpenView { Name =  Views.EndGame});
                return;
            } 
            
            Move(_moves[_current]);
        }

        private void CloseWindow()
        {
            _window.Show(false);
            _window.Unlisten();            
            _view.Show(false);
            _view.Unlisten();
            _view.DestroyRings();            
        }

        private int GetFreePosition(int place)
        {
            int maxPos = 0;
            for (int i = 0; i < _rings.Length; i++)
            {
                if (_rings[i].Place == place && _rings[i].Position >= maxPos)
                {
                    maxPos = _rings[i].Position + 1;
                }
            }            
            return maxPos;
        }
        
        private void OnSpeedMinus()
        {
            if (_settings.CurrentSpeed <= 0.2f) return;
            if (_settings.CurrentSpeed <= 1)
                _settings.CurrentSpeed -= 0.1f;
            else if (_settings.CurrentSpeed <= 50)
                _settings.CurrentSpeed--;
            else
                _settings.CurrentSpeed -= 10;
            SpeedRefresh();
        }
        
        private void OnSpeedPlus()
        {
            if (_settings.CurrentSpeed >= 500) return;
            if (_settings.CurrentSpeed <= 1)
                _settings.CurrentSpeed += 0.1f;
            else if (_settings.CurrentSpeed <= 50)
                _settings.CurrentSpeed++;
            else
                _settings.CurrentSpeed += 10;
            SpeedRefresh();
        }

        private void SpeedRefresh()
        {         
            _view.SetSpeed(_settings.CurrentSpeed);
            _window.SetSpeed(_settings.CurrentSpeed);
        }
    }   

}