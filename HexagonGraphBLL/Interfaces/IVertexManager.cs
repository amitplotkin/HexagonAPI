using System.Collections.Generic;

namespace HexagonGraphBLL
{
    public interface IVertexManager
    {
        Vertex AddVertex(Vertex vertex);
        void AddVertex(IList<Vertex> vertex);
        void AddVertexToEdge(Vertex vertex1, string edgeId);
        IEnumerable<Vertex> GetVertexAppliedToEdge(IEnumerable<string> edgeOfVertex);
        bool IsExists(int vertexID);
    }
}
