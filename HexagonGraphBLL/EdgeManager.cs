using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexagonGraphBLL
{
    public class EdgeManager : IEdgeManager
    {
        private Dictionary<string, Edge> edges;

        public EdgeManager(Dictionary<string, Edge> edges)
        {
            this.edges = edges;
        }

        public string AddNewEdge(Edge edge,string key="")
        {
            try
            {
                string EdgeID = string.IsNullOrEmpty(key)? Guid.NewGuid().ToString():key;
                if (!edges.Any(x => Edge.IsSame(x.Value, edge)))//check if the Edge Exists
                {
                    edges.Add(EdgeID, edge);
                    return EdgeID;
                }
                return edges.First(x => Edge.IsSame(x.Value, edge)).Key;
            }
            catch (Exception)
            {

               
            }
            return string.Empty;
        }

        public void AddNewEdge(Dictionary<string, Edge> edges)
        {
            foreach (var item in edges)
            {
                AddNewEdge(item.Value,item.Key);
            }
        }

        public void AddNewEdge(IList<Edge> edges)
        {
            foreach (var item in edges)
            {
                AddNewEdge(item);
            }
        }

        public Dictionary<string, Edge> GetAllEdgetOFVertix(Vertex startPoint)
        {
             return edges.Where(x => x.Value.vertex1 == startPoint || x.Value.vertex2 == startPoint).ToDictionary(i => i.Key, i => i.Value);
        }
    }

    public interface IEdgeManager
    {
        string AddNewEdge(Edge edge,string key="");
        void AddNewEdge(IList<Edge> edges);
        void AddNewEdge(Dictionary<string, Edge> edges);
        Dictionary<string, Edge> GetAllEdgetOFVertix(Vertex startPoint);
    }
}
