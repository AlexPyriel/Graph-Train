namespace Graph.Models
{
    public class EdgeModel
    {
        public NodeModel NodeA { get; }
        public NodeModel NodeB { get; }
        public float Distance { get; }

        public EdgeModel(NodeModel nodeA, NodeModel nodeB, float distance)
        {
            NodeA = nodeA;
            NodeB = nodeB;
            Distance = distance < 1 ? 1 : distance;
        }

        public bool Connects(NodeModel nodeA, NodeModel nodeB)
        {
            return (NodeA == nodeA && NodeB == nodeB) || (NodeA == nodeB && NodeB == nodeA);
        }

        public NodeModel GetNeighbor(NodeModel node)
        {
            return node == NodeA ? NodeB : NodeA;
        }
    }
}