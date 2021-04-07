using Data;
using Services;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace GameSettings
{
    public partial class GameSettings : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup _mixer;
        [SerializeField] private Slider _globalVolumeSlider;
        [SerializeField] private Slider _musicVolumeSlider;
        [SerializeField] private Slider _effectsVolumeSlider;
        [SerializeField] private Button _applySettingsButton;
        [SerializeField] private GameDataService _gameDataService;

        private float _globalVolume;
        private float _musicVolume;
        private float _effectsVolume;

        private void OnEnable()
        {
            _gameDataService.DataCreated += OnDataCreated;
            _gameDataService.DataLoaded += OnDataLoaded;

            _applySettingsButton.onClick.AddListener(UpdateSettings);

            _globalVolumeSlider.onValueChanged.AddListener(ChangeGlobalVolume);
            _musicVolumeSlider.onValueChanged.AddListener(ChangeMusicVolume);
            _effectsVolumeSlider.onValueChanged.AddListener(ChangeEffectsVolume);
        }

        private void OnDisable()
        {
            _gameDataService.DataCreated -= OnDataCreated;
            _gameDataService.DataLoaded -= OnDataLoaded;

            _applySettingsButton.onClick.RemoveListener(UpdateSettings);

            _globalVolumeSlider.onValueChanged.RemoveListener(ChangeGlobalVolume);
            _musicVolumeSlider.onValueChanged.RemoveListener(ChangeMusicVolume);
            _effectsVolumeSlider.onValueChanged.RemoveListener(ChangeEffectsVolume);
        }

        private void OnDataCreated()
        {
            UpdateSettings();
        }

        private void OnDataLoaded()
        {
            SetGameSettingsData();
        }

        private void SetGameSettingsData()
        {
            SetVolume(_gameDataService.GameSettings.GameVolume);
        }

        private void SetVolume(GameVolumeSettings gameVolume)
        {
            _globalVolumeSlider.value = gameVolume.GlobalVolume;
            _musicVolumeSlider.value = gameVolume.MusicVolume;
            _effectsVolumeSlider.value = gameVolume.EffectsVolume;

            _mixer.audioMixer.SetFloat(AudioMixerExposedParametersNames.GlobalVolume, gameVolume.GlobalVolume);
            _mixer.audioMixer.SetFloat(AudioMixerExposedParametersNames.MusicVolume, gameVolume.MusicVolume);
            _mixer.audioMixer.SetFloat(AudioMixerExposedParametersNames.EffectsVolume, gameVolume.EffectsVolume);
        }

        private void ChangeGlobalVolume(float value)
        {
            _globalVolume = value;
            SetMixerVolume(AudioMixerExposedParametersNames.GlobalVolume, _globalVolume);
        }

        private void ChangeMusicVolume(float value)
        {
            _musicVolume = value;
            SetMixerVolume(AudioMixerExposedParametersNames.MusicVolume, _musicVolume);
        }

        private void ChangeEffectsVolume(float value)
        {
            _effectsVolume = value;
            SetMixerVolume(AudioMixerExposedParametersNames.EffectsVolume, _effectsVolume);
        }

        private void SetMixerVolume(string mixerName, float volume)
        {
            _mixer.audioMixer.SetFloat(mixerName, volume);
        }

        private void UpdateSettings()
        {
            GameVolumeSettings gameVolumeSettings = new GameVolumeSettings()
            {
                GlobalVolume = _globalVolume,
                MusicVolume = _musicVolume,
                EffectsVolume = _effectsVolume
            };

            _gameDataService.GameSettings.UpdateSettings(gameVolumeSettings);
        }
    }
}