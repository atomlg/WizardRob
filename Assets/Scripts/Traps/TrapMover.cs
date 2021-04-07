using DG.Tweening;
using UnityEngine;

namespace Traps
{
    public class TrapMover : MonoBehaviour
    {
        private enum MovementType
        {
            Path,
            Random
        }

        [SerializeField] private MovementType _movementType = MovementType.Path;
        [SerializeField] private float _speed = 3f;
        [SerializeField] private Ease _ease = Ease.Linear;
        [SerializeField] private Vector3[] _wayPoints;
        
        private void Start()
        {
            if (_movementType == MovementType.Path)
                PathMovement();
            else if (_movementType == MovementType.Random)
                RandomMovement();
        }

        private void PathMovement()
        {
            transform.DOPath(_wayPoints, _speed).SetLoops(-1, LoopType.Yoyo).SetEase(_ease);
        }

        private void RandomMovement()
        {
            int randomValue = Random.Range(0, _wayPoints.Length);
            Vector3 endValue = _wayPoints[randomValue];
            transform.DOMove(endValue, _speed).SetSpeedBased(true).SetEase(_ease).OnComplete(RandomMovement);
        }
    }
}