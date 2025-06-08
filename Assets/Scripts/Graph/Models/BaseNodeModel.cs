using Graph.Enums;
using Graph.Interfaces;
using UnityEngine;

namespace Graph.Models
{
    public class BaseNodeModel : NodeModel, IMultiplierProvider
    {
        public float Multiplier { get; }

        public BaseNodeModel(float multiplier, Vector3 position) : base(ENodeType.Base, position)
        {
            Multiplier = multiplier;
        }
    }
}