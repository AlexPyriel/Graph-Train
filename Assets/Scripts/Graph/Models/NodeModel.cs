using Graph.Enums;
using UnityEngine;

namespace Graph.Models
{
    public abstract class NodeModel
    {
        public ENodeType Type { get; }
        public Vector3 Position { get; }

        protected NodeModel(ENodeType type, Vector3 position)
        {
            Type = type;
            Position = position;
        }
    }
}