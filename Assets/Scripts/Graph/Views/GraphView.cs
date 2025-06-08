using System.Collections.Generic;
using System.Linq;
using Graph.Interfaces;
using Graph.Models;
using MVP.Views;
using UnityEngine;

namespace Graph.Views
{
    public class GraphView : View, IGraphView
    {
        [SerializeField] private List<EdgeModelData> _edgeData = new();

        public IReadOnlyList<EdgeModel> Edges => _edgeData
            .Select(data => new EdgeModel(data.NodeA.Model, data.NodeB.Model, data.Distance))
            .ToList();

        private void OnDrawGizmos()
        {
            if (_edgeData == null) return;

            Gizmos.color = Color.green;

            foreach (EdgeModelData edge in _edgeData)
            {
                if (edge.NodeA != null && edge.NodeB != null)
                {
                    Gizmos.DrawLine(edge.NodeA.transform.position, edge.NodeB.transform.position);
                }
            }
        }
    }
}