using System.Collections.Generic;
using Graph.Enums;
using Graph.Interfaces;
using Graph.Strategies;
using UnityEngine;

namespace Game.Configs
{
    [CreateAssetMenu(fileName = "Train Config ", menuName = "Configs/New Train Config", order = 0)]
    public class TrainConfig : ScriptableObject
    {
        [SerializeField] private EFindPathStrategy _findPathStrategy;
        [SerializeField, Range(1f, 500f)] private float _movementSpeed;
        [SerializeField, Range(0.1f, 60f)] private float _baseMiningTime;

        private readonly Dictionary<EFindPathStrategy, IFindPathStrategy> _strategies = new()
        {
            { EFindPathStrategy.Dijkstra, new DijkstraStrategy() },
            { EFindPathStrategy.AStar, new AStarStrategy() },
            { EFindPathStrategy.Bfs, new BreadthFirstSearchStrategy() },
            { EFindPathStrategy.Dfs, new DepthFirstSearchStrategy() }
        };

        public IFindPathStrategy FindPathStrategy => _strategies.GetValueOrDefault(_findPathStrategy, new DijkstraStrategy());
        public float MovementSpeed => _movementSpeed;
        public float BaseMiningTime => _baseMiningTime;
    }
}