using Game.Enums;
using UnityEngine;

namespace Game.Configs
{
    [CreateAssetMenu(fileName = "Train Config ", menuName = "Configs/New Train Config", order = 0)]
    public class TrainConfig : ScriptableObject
    {
        [SerializeField] private EFindPathStrategy _findPathStrategy;
        [SerializeField, Range(1f, 500f)] private float _movementSpeed;
        [SerializeField, Range(0.1f, 60f)] private float _baseMiningTime;

        public EFindPathStrategy FindPathStrategy => _findPathStrategy;
        public float MovementSpeed => _movementSpeed;
        public float BaseMiningTime => _baseMiningTime;
    }
}