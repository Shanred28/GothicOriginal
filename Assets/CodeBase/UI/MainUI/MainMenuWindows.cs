using CodeBase.UI.Common;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CodeBase.UI.MainUI
{
    public class MainMenuWindows : WindowsBase
    {
       public event UnityAction PlayButtonClicked;
       
       [SerializeField] private string buttonLabelPrefics = "Start level ";
       [SerializeField] private TMP_Text levelNumberText;
       [SerializeField] private Button playButton;

       private void Start()
       {
           playButton.onClick.AddListener(() => PlayButtonClicked?.Invoke());
       }

       public void SetLevelNumberLabel(int levelIndex)
       {
           levelNumberText.text = buttonLabelPrefics + (levelIndex + 1).ToString();
       }

       public void HidePlayButton()
       {
           playButton.gameObject.SetActive(false);
       }
    }
}
