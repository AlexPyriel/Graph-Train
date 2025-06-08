using Graph.Enums;
using UnityEngine;

namespace Graph.Models
{
    public class StationNodeModel : NodeModel
    {
        public StationNodeModel(Vector3 position) : base(ENodeType.Station, position) { }
    }
}