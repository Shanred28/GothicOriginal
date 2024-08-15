using CodeBase.Configs.WindowsConfig;
using CodeBase.Services.Factory.UIFactory.Interface;
using CodeBase.UI.Common;
using CodeBase.UI.MainUI;
using CodeBase.UI.MainUI.Windows;
using UnityEngine;
using VContainer;

namespace CodeBase.Services.Factory.UIFactory
{
    public class UIFactory : IUIFactory
    {
        private const string UIRootName = "UI Root";

        public Transform UIRoot { get; set; }

        private readonly IObjectResolver _container;

        public UIFactory(IObjectResolver container)
        {
            _container = container;
        }

        public void CreateUIRoot()
        {
            UIRoot = new GameObject(UIRootName).transform;
        }

        public MainMenuPresenter CreateMainMenuWindows(WindowConfig windowConfig)
        {
            return CreateWindow<MainMenuWindows, MainMenuPresenter>(windowConfig);
        }

        private TPresenter CreateWindow<TWindow, TPresenter>(WindowConfig config) where TWindow : WindowsBase
            where TPresenter : WindowsPresenterBase<TWindow>
        {
            TWindow window = InstantiateGameObject(config.prefab).GetComponent<TWindow>();
            window.transform.SetParent(UIRoot);
            window.SetTitle(config.title);

            TPresenter presenter = _container.Resolve<TPresenter>();
            presenter.SetWindow(window);

            return presenter;
        }

        private GameObject InstantiateGameObject(GameObject prefab)
        {
            GameObject newObj = GameObject.Instantiate(prefab);
            InjectToGameObject(newObj);
            return newObj;
        }

        private void InjectToGameObject(GameObject gameObject)
        {
            _container.Inject(gameObject);
        }
    }
}