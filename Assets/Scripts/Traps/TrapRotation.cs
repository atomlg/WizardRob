using DG.Tweening;
using UnityEngine;

namespace Traps
{
    public class TrapRotation : MonoBehaviour
    {
        [SerializeField] private Vector3 _endValue = new Vector3(0f, 0f, 180f);
        [SerializeField] private float _duration = 1f;

        private void Start()
        {
            transform.DORotate(_endValue, _duration).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
        }
    }
}
