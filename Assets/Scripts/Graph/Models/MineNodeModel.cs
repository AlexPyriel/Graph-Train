using Graph.Enums;
using Graph.Interfaces;
using UnityEngine;

namespace Graph.Models
{
    public class MineNodeModel : NodeModel, IMultiplierProvider
    {
        public float Multiplier { get; }

        public MineNodeModel(float multiplier, Vector3 position) : base(ENodeType.Mine, position)
        {
            Multiplier = multiplier;
        }
    }
}