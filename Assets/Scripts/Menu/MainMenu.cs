using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _openSettingsPanelButton;
        [SerializeField] private Button _closeSettingsPanelButton;
        [SerializeField] private GameObject _settingsPanel;

        private void OnEnable()
        {
            _openSettingsPanelButton.onClick.AddListener(OpenSettingsPanel);
            _closeSettingsPanelButton.onClick.AddListener(CloseSettingsPanel);
        }

        private void OnDisable()
        {
            _openSettingsPanelButton.onClick.RemoveListener(OpenSettingsPanel);
            _closeSettingsPanelButton.onClick.RemoveListener(CloseSettingsPanel);
        }

        private void OpenSettingsPanel()
        {
            _settingsPanel.SetActive(true);
            _openSettingsPanelButton.gameObject.SetActive(false);
        }
    
        private void CloseSettingsPanel()
        {
            _settingsPanel.SetActive(false);
            _openSettingsPanelButton.gameObject.SetActive(true);
        }
    }
}
