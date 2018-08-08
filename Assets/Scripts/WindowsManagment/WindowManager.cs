using System.Linq;
using Boo.Lang;
using EndGame;
using Question;
using Solution;
using Zenject;

namespace Installers
{
    public class WindowManager : IInitializable
    {
        private List<IWindow> _windows;
        private readonly SignalBus _signal;

        public WindowManager(QuestionManager questionManager,
                             SolutionManager solutionManager,
                             EndGameManager endGameManager,
                             SignalBus signal)
        {
            _windows = new List<IWindow>();
            _windows.Add(questionManager);
            _windows.Add(solutionManager);
            _windows.Add(endGameManager);
            _signal = signal;
        }

        public void Initialize()
        {
            OpenView( new OpenView { Name = Views.Question });
            _signal.Subscribe<OpenView>(OpenView);
        }

        private void OpenView(OpenView obj)
        {
            if (obj == null) return;
            var window = _windows.FirstOrDefault(x => x.GetId() == obj.Name);
            if (window != null) window.Run();
        }
    }

    public class OpenView
    {
        public Views Name;
    }

    public enum Views
    {
        Solution,
        Question,
        EndGame
    }
}