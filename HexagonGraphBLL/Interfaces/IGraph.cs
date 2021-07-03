
using System.Collections.Generic;

namespace HexagonGraphBLL
{
    public interface IGraph
    {
        void AddVertex(Vertex vertex);//Adds a vertex to the graph
        void AddEdge(Edge edge);//Adds an edge to the graph
        bool VericexExists(int vertexID);//  Gets if a vertex with the specified ID exists
        Edge GetEdge(string edgeID);// – Gets the edge with the specied ID
        IEnumerable<XGraph> Split();// – Splits the graph to its sub-graphs
    }
}