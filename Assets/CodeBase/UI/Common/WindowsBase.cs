using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CodeBase.UI.Common
{
    public abstract class WindowsBase : MonoBehaviour
    {
        public event UnityAction Cleanuped;

        [SerializeField] private Button closeButton;
        [SerializeField] private TMP_Text titleText;

        private void Awake()
        {
            OnAwake();
            closeButton?.onClick.AddListener(OnClose);
        }

        private void OnDestroy()
        {
            closeButton?.onClick?.RemoveListener(OnClose);
            OnCleanup();
            Cleanuped?.Invoke();
        }

        public void Close()
        {
            OnClose();
        }

        public void SetTitle(string title)
        {
            if(title == null) return;
            
            titleText.text = title;
        }

        protected virtual void OnAwake() { }
        protected virtual void OnClose() { }
        protected virtual void OnCleanup() { }
    }
}

