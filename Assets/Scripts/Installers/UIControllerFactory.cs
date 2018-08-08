using Settings;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class UiControllerFactory<T> : IFactory<T> where T : Object {
		
        private readonly MainCanvas _root;
        private readonly DiContainer _container;
        private readonly T _prefab;
		
        [Inject]
        public UiControllerFactory(MainCanvas root, DiContainer container, T prefab) {
            _root = root;
            _container = container;
            _prefab = prefab;
        }

        public T Create() {
            var component = _container.InstantiatePrefab(_prefab, _root.transform);
#if UNITY_EDITOR
            component.name = component.name.Replace("(Clone)", "");
#endif
            component.layer = _root.gameObject.layer;
            return component.GetComponent<T>();
        }
    }
}