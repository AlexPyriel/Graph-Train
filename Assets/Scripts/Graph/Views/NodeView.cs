using System;
using System.Collections.Generic;
using Graph.Enums;
using Graph.Models;
using UnityEngine;

namespace Graph.Views
{
    public class NodeView : MonoBehaviour
    {
        [SerializeField] private ENodeType _nodeType;
        [SerializeField, Range(0.1f, 10f)] private float _multiplier;

        private const float SPHERE_RADIUS = 20f;

        private readonly Color _defaultColor = Color.gray;
        private readonly Dictionary<ENodeType, Color> _nodeColors = new()
        {
            { ENodeType.Station, Color.black },
            { ENodeType.Base, Color.cyan },
            { ENodeType.Mine, Color.magenta }
        };

        public NodeModel Model { get; private set; }

        private void Awake()
        {
            Vector3 position = transform.position;

            Model = _nodeType switch
            {
                ENodeType.Mine => new MineNodeModel(_multiplier, position),
                ENodeType.Base => new BaseNodeModel(_multiplier, position),
                ENodeType.Station => new StationNodeModel(position),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private void OnDrawGizmos()
        {
            Color color = _nodeColors.GetValueOrDefault(_nodeType, _defaultColor);

            Gizmos.color = color;
            Gizmos.DrawSphere(transform.position, SPHERE_RADIUS);
        }
    }
}