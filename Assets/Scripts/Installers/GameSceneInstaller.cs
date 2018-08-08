using EndGame;
using Installers;
using Question;
using Settings;
using Solution;
using Zenject;

namespace DefaultNamespace
{
    public class GameSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {                             
            InstallGame();            
            InstallHelpers();
            InstallMenu();
            InstallEndGame();
            InstallSignals();
        }

        private void InstallGame()
        {
            Container.BindInterfacesAndSelfTo<SolutionManager>().AsSingle();
        }

        private void InstallMenu()
        {
            Container.BindInterfacesAndSelfTo<QuestionManager>().AsSingle();
        }
        
        private void InstallEndGame()
        {
            Container.BindInterfacesAndSelfTo<EndGameManager>().AsSingle();
        }
        
        private void InstallHelpers()
        {
            Container.BindInterfacesAndSelfTo<WindowManager>().AsSingle();
            Container.Bind<MainCanvas>().FromComponentInHierarchy().AsSingle();
        }
        
        private void InstallSignals()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<OpenView>();            
        }                
    }
        
        
}