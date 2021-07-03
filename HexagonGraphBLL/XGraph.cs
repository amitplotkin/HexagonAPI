using HexagonGraphBLL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HexagonGraphBLL
{
    public class XGraph : IGraph
    {
       
        private IVertexManager _VertexManager;
        private IEdgeManager _EdgeManager;

        // Definitions

        public Dictionary<string, Edge> Edges { get; private set; }

        public List<Vertex> Vertices { get; private set; }

        public XGraph()
        {
            Edges = new Dictionary<string, Edge>();
            Vertices = new List<Vertex>();
            _VertexManager = new VertexManager(Vertices);
            _EdgeManager = new EdgeManager(Edges);

        }

        public XGraph(Vertex vertex) : this()
        {
            this.Vertices.Add(vertex);
        }

        public XGraph(List<Vertex> vertex,Dictionary<string,Edge> edges) : this()
        {
            if (vertex!=null && vertex.Any())
            {
                _VertexManager.AddVertex(vertex);
            }
            if (edges!=null && edges.Any())
            {
                _EdgeManager.AddNewEdge(edges);
            }
            
        }

        public XGraph(IList<Vertex> vertex, IList<Edge> edges) : this()
        {
            if (vertex != null && vertex.Any())
            {
                _VertexManager.AddVertex(vertex);
            }
            if (edges != null && edges.Any())
            {
                _EdgeManager.AddNewEdge(edges);
            }
        }

        // methods


        /// <summary>
        /// 
        /// </summary>
        /// <param name="edge"></param>

        public void AddEdge(Edge edge)
        {
            try
            {
                //before adding edge make sure the vertex are created/exists
                var v1 = _VertexManager.AddVertex(edge.vertex1);
                var v2 = _VertexManager.AddVertex(edge.vertex2);

                edge.vertex1 = v1;
                edge.vertex2 = v2;
                //create the edge
                string EdgeId = _EdgeManager.AddNewEdge(edge);

                if (EdgeId != string.Empty)//if added or exists update the Vertex
                {
                    _VertexManager.AddVertexToEdge(edge.vertex1, EdgeId);
                    _VertexManager.AddVertexToEdge(edge.vertex2, EdgeId);
                }
            }
            catch (Exception)
            {

                throw;
            }


        }

        public void AddVertex(Vertex vertex)
        {
            _VertexManager.AddVertex(vertex);

        }

        public Vertex GetVertex(Vertex vertex)
        {
            return Vertices.FirstOrDefault(x => x.PointX == vertex.PointX);
        }
        public Edge GetEdge(string edgeID)
        {
            if (Edges.ContainsKey(edgeID))
            {
                return Edges[edgeID];
            }
            return null;

        }

        public IEnumerable<XGraph> Split()
        {
            List<XGraph> xGraphs = new List<XGraph>();
            var tmpVertex = this.Vertices.ToList();
            //select * vertex where no edge applied or self pointed



            //////////////////////////////////////////////////////////////////////////
            Func<IEnumerable<Vertex>, XGraph> FindGr = (VCollection) =>
             {
                 Dictionary<string, Edge> gEdge = new Dictionary<string, Edge>();

                 List<Vertex> gVert = new List<Vertex>() { };

                 Queue<Vertex> gVertToVisit = new Queue<Vertex>();

                 gVertToVisit.Enqueue(VCollection.First());


                 while (gVertToVisit.Count > 0)
                 {
                     var firstItem = gVertToVisit.Peek();

                     Dictionary<string, Edge> x = _EdgeManager.GetAllEdgetOFVertix(firstItem);
                    
                     //if single node
                     if (!x.Any() && !gVert.Contains(firstItem))
                     {
                         gVert.Add(firstItem);
                     }
                    
                     foreach (var item in x)
                     {
                         if (!gVertToVisit.Contains(item.Value.vertex2))
                         {
                             gVertToVisit.Enqueue(item.Value.vertex2);
                         }
                         gEdge.TryAdd(item.Key, item.Value);
                         if (!gVert.Contains(item.Value.vertex1))//COsidre to remove this if
                         {
                             gVert.Add(item.Value.vertex1);
                         }
                         if (!gVert.Contains(item.Value.vertex2))
                         {
                             gVert.Add(item.Value.vertex2);
                         }

                     }

                  
                   tmpVertex.Remove(gVertToVisit.Dequeue());
                 }

                 return new XGraph(gVert, gEdge);
             };
            ////////////////////////////////////////////////////////////////////////////

            while (tmpVertex.Count > 0)
            {
                var startnode = tmpVertex.First();
                XGraph graph = FindGr(tmpVertex);
          

                xGraphs.Add(graph);
            }

            return xGraphs;
        }

      

      

        public bool VericexExists(int vertexID)
        {
            return _VertexManager.IsExists(vertexID);
        }
    }
}
