using System.Collections.Generic;

namespace HexagonGraphBLL
{
    public interface IEdgeManager
    {
        string AddNewEdge(Edge edge,string key="");
        void AddNewEdge(IList<Edge> edges);
        void AddNewEdge(Dictionary<string, Edge> edges);
        Dictionary<string, Edge> GetAllEdgetOFVertix(Vertex startPoint);
    }
}
