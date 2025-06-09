using System;
using Graph.Views;

namespace Graph.Models
{
    [Serializable]
    public class EdgeModelData
    {
        public NodeView NodeA;
        public NodeView NodeB;
        public float Distance;
    }
}