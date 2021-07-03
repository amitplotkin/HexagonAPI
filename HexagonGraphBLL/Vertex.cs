using System;
using System.Collections.Generic;
using System.Text;

namespace HexagonGraphBLL
{
    [Serializable]
    public class Vertex
    {
        public int PointX { get; set; }
        private List<string> edgeIds;
        public int GraphCount { get=> edgeIds.Count; private set { } }
        public string[] GraphItems { get => edgeIds.ToArray(); private set { } }

        public Vertex()
        {
            edgeIds = new List<string>();
        }
        public Vertex(string graphID)
        {
            edgeIds = new List<string>() { graphID };
        }

        public void AddEdge(string EndeID)
        {
            if (!edgeIds.Contains(EndeID))
            {
                edgeIds.Add(EndeID);
            }
        }

        public void RemoveGraph(string graphID)
        {
            if (edgeIds.Contains(graphID))
            {
                edgeIds.Remove(graphID);
            }
        }

    }
}
