using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexagonGraphBLL
{
    public class VertexManager : IVertexManager
    {
        private List<Vertex> vertices;

        public VertexManager(List<Vertex> vertices)
        {
            this.vertices = vertices;
        }

        public Vertex AddVertex(Vertex vertex)
        {
            if (!vertices.Any(x => x.PointX == vertex.PointX))
            {
                vertices.Add(vertex);
                return vertex;
            }

            return vertices.First(x => x.PointX == vertex.PointX);
        }

        public void AddVertex(IList<Vertex> vertex)
        {
            foreach (var item in vertex)
            {
                AddVertex(item);
            }
        }


        public void AddVertexToEdge(Vertex vertex1, string edgeId)
        {
            try
            {
                var vertex = vertices.FirstOrDefault(x => x.PointX == vertex1.PointX);
                if (vertex!=null)
                {
                    vertex.AddEdge(edgeId);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Vertex> GetVertexAppliedToEdge(IEnumerable<string> EdgeIDList)
        {
            foreach (var edgeId in EdgeIDList)
            {
               var v= vertices.Where(x => x.GraphItems.Contains(edgeId));
                if (v.Any())
                {
                    yield return (Vertex)v;
                }
            }
        }

        public bool IsExists(int vertexID)
        {
            return vertices.Any(x => x.PointX == vertexID);
        }
    }

    public interface IVertexManager
    {
        Vertex AddVertex(Vertex vertex);
        void AddVertex(IList<Vertex> vertex);
        void AddVertexToEdge(Vertex vertex1, string edgeId);
        IEnumerable<Vertex> GetVertexAppliedToEdge(IEnumerable<string> edgeOfVertex);
        bool IsExists(int vertexID);
    }
}
