using DefaultNamespace;
using EndGame;
using Question;
using Settings;
using Solution;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "PrefabsInstaller", menuName = "Installers/PrefabsInstaller")]
public class PrefabsInstaller : ScriptableObjectInstaller<PrefabsInstaller>
{
    [SerializeField] private QuestionWindow _questionWindow;
    [SerializeField] private SolutionView _solutionView;
    [SerializeField] private SolutionWindow _solutionWindow;
    [SerializeField] private EndGameWindow _endGameWindow;
    [SerializeField] private GameSettings _settings;
    
    public override void InstallBindings()
    {
        BindWindow(_questionWindow);
        BindWindow(_solutionWindow);
        BindWindow(_endGameWindow);
        BindWindow(_solutionView);

        Container.BindInstance(_settings);
    }
    
    private void BindWindow<T>(T prefab) where T : Object {
        if (prefab is MonoBehaviour)
            (prefab as MonoBehaviour).gameObject.SetActive(false);
        Container.Bind<T>().FromMethod(x => prefab).WhenInjectedInto<UiControllerFactory<T>>();
        Container.Bind<T>().FromFactory<UiControllerFactory<T>>().AsSingle();
    }
}