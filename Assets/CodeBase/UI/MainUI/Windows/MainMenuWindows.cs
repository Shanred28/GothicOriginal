using CodeBase.UI.Common;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CodeBase.UI.MainUI.Windows
{
    public class MainMenuWindows : WindowsBase
    {
       public event UnityAction OnPlayButtonClicked;
       
       [SerializeField] private Button playButton;

       private void Start()
       {
           playButton.onClick.AddListener(() => OnPlayButtonClicked?.Invoke());
       }
       
       public void HidePlayButton()
       {
           playButton.gameObject.SetActive(false);
       }
    }
}
