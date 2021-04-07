using System;
using Player;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Levels
{
    [RequireComponent(typeof(AudioSource))]
    public class InputHandler : MonoBehaviour
    {
        private const float Radius = 3f;

        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private ParticleSystem _explosionEffect;
        [SerializeField] private PlayerHealth _playerHealth;

        private readonly Collider2D[] _results = new Collider2D[6];
        private AudioSource _audioSource;
        private Camera _camera;
        private ParticleSystem _explosionWaveEffect;

        public event Action<Collider2D[],Vector3> PushStared;

        private void Awake()
        {
            _camera = Camera.main;
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (Clicked())
            {
                Vector3 mousePosition = MousePosition();

                EnableEffects(mousePosition);
                PlayAudio();

                int resultsCount = Physics2D.OverlapCircleNonAlloc(mousePosition, Radius, _results, _layerMask);
                if (resultsCount > 0)
                {
                    PushStared?.Invoke(_results, mousePosition);
                }
            }
        }

        private Vector3 MousePosition()
        {
            Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            return mousePosition;
        }

        private bool Clicked()
        {
            if (Application.isMobilePlatform)
                return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began &&
                       !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);

            return Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject();
        }

        private void PlayAudio()
        {
            _audioSource.Play();
        }

        private void EnableEffects(Vector3 mousePosition)
        {
            _explosionEffect.transform.position = mousePosition;
            _explosionEffect.Play();
        }
    }
}