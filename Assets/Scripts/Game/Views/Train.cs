using System.Collections.Generic;
using Game.Configs;
using Game.Interfaces;
using Graph.Enums;
using Graph.Interfaces;
using Graph.Models;
using UnityEngine;

namespace Game.Views
{
    public class Train : MonoBehaviour
    {
        private IReadOnlyDictionary<NodeModel, List<EdgeModel>> _edgeMap;
        private IFindPathStrategy _strategy;
        private IGameView _gameView;

        private float _movementSpeed;
        private float _baseMiningTime;

        private NodeModel _currentNode;
        private NodeModel _targetNode;
        private Queue<NodeModel> _path = new();

        private float _journeyLength;
        private float _startTime;

        private bool _isMining;
        private float _miningTimer;

        private bool _hasResource;

        public void Initialize(
            NodeModel startNode,
            IReadOnlyDictionary<NodeModel, List<EdgeModel>> edgeMap,
            IGameView gameView,
            TrainConfig config)
        {
            _currentNode = startNode;
            _edgeMap = edgeMap;
            _gameView = gameView;
            _strategy = config.FindPathStrategy;
            _movementSpeed = config.MovementSpeed;
            _baseMiningTime = config.BaseMiningTime;

            ArriveAtNode(_currentNode);
        }

        private void Update()
        {
            if (_isMining)
            {
                ProcessMining();
                return;
            }

            if (_targetNode != null)
            {
                MoveTowardsTarget();
            }
        }

        private void ProcessMining()
        {
            _miningTimer -= Time.deltaTime;

            if (_miningTimer <= 0f)
            {
                _isMining = false;
                _hasResource = true;
                CalculateNewPath();
            }
        }

        private void MoveTowardsTarget()
        {
            float distCovered = (Time.time - _startTime) * _movementSpeed;
            float fraction = _journeyLength == 0 ? 1f : distCovered / _journeyLength;

            transform.position = Vector3.Lerp(
                _currentNode.Position,
                _targetNode.Position,
                Mathf.Clamp01(fraction));
            
            Vector3 direction = _targetNode.Position - _currentNode.Position;

            if (direction != Vector3.zero)
            {
                transform.forward = direction.normalized;
            }

            if (fraction >= 1f)
            {
                ArriveAtNode(_targetNode);
            }
        }

        private void ArriveAtNode(NodeModel node)
        {
            _currentNode = node;
            _targetNode = null;

            switch (node.Type)
            {
                case ENodeType.Mine when !_hasResource:
                    StartMining(node);
                    break;

                case ENodeType.Base when _hasResource:
                    DepositResources(node);
                    break;

                default:
                    CalculateNewPath();
                    break;
            }
        }

        private void StartMining(NodeModel mineNode)
        {
            _isMining = true;
            _miningTimer = _baseMiningTime * GetMultiplier(mineNode);
        }

        private void DepositResources(NodeModel baseNode)
        {
            _gameView.AddResources(GetMultiplier(baseNode));
            _hasResource = false;
            CalculateNewPath();
        }

        public void CalculateNewPath()
        {
            ENodeType targetType = _hasResource ? ENodeType.Base : ENodeType.Mine;
            List<NodeModel> fullPath = _strategy?.FindPath(_currentNode, targetType, _edgeMap);

            if (fullPath == null || fullPath.Count < 2)
            {
                Debug.LogError($"[Train] No path found from {_currentNode} to {targetType}");
                ClearPath();
                return;
            }

            _path = new Queue<NodeModel>(fullPath);
            _path.Dequeue();
            MoveToNextInPath();
        }

        private void MoveToNextInPath()
        {
            if (_path.Count == 0)
            {
                _targetNode = null;
                return;
            }

            _targetNode = _path.Dequeue();

            if (!_edgeMap.TryGetValue(_currentNode, out List<EdgeModel> edges))
            {
                Debug.LogError($"[Train] No edges found for {_currentNode}");
                return;
            }

            EdgeModel edge = FindEdgeConnecting(_currentNode, _targetNode);

            if (edge == null)
            {
                Debug.LogError($"[Train] No edge between {_currentNode} and {_targetNode}");
                _targetNode = null;
                return;
            }

            StartJourney(edge);
        }

        private EdgeModel FindEdgeConnecting(NodeModel from, NodeModel to)
        {
            return _edgeMap.TryGetValue(from, out List<EdgeModel> edges)
                ? edges.Find(edgeModel => edgeModel.Connects(from, to))
                : null;
        }

        private void StartJourney(EdgeModel edge)
        {
            _journeyLength = edge.Distance;
            _startTime = Time.time;
        }

        private void ClearPath()
        {
            _path.Clear();
            _targetNode = null;
        }

        private float GetMultiplier(NodeModel model)
        {
            return model is IMultiplierProvider provider ? provider.Multiplier : 1f;
        }

        private void OnDrawGizmos()
        {
            if (_targetNode != null)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(transform.position, _targetNode.Position);
            }
        }
    }
}