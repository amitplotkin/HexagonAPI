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

        public Dictionary<string, Edge> Edges { get; private set; }//holds the  edges of the graph

        public List<Vertex> Vertices { get; private set; }//holds the  nodes of the graph

        /// <summary>
        /// 
        /// </summary>
        public XGraph()
        {
            Edges = new Dictionary<string, Edge>();
            Vertices = new List<Vertex>();
            _VertexManager = new VertexManager(Vertices);//the logic/manipulations of the Vertex
            _EdgeManager = new EdgeManager(Edges);// logic of the Edges

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vertex"></param>
        /// <param name="edges"></param>
        public XGraph(List<Vertex> vertex, Dictionary<string, Edge> edges) : this()
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vertex"></param>
        /// <param name="edges"></param>
        public XGraph(IList<Vertex> vertex, IList<Edge> edges) : this()
        {
            if (vertex != null && vertex.Any())
            {
                _VertexManager.AddVertex(vertex);
            }
            if (edges != null && edges.Any())
            {
                foreach (var item in edges)
                {
                    AddEdge(item);
                }

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vertex"></param>
        public void AddVertex(Vertex vertex)
        {
            _VertexManager.AddVertex(vertex);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public Vertex GetVertex(Vertex vertex)
        {
            return GetVertex(vertex.PointX);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vertexID"></param>
        /// <returns></returns>
        public Vertex GetVertex(int vertexID)
        {
            return Vertices.FirstOrDefault(x => x.PointX == vertexID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="edgeID"></param>
        /// <returns></returns>
        public Edge GetEdge(string edgeID)
        {
            if (Edges.ContainsKey(edgeID))
            {
                return Edges[edgeID];
            }
            return null;

        }

        /// <summary>
        /// split the graph
        /// </summary>
        /// <returns></returns>
        public IEnumerable<XGraph> Split()
        {
            List<XGraph> xGraphs = new List<XGraph>();
            var tmpVertex = this.Vertices.ToList();//work on a copy of list
            //select * vertex where no edge applied or self pointed



            /////////////////////////////thie Func hsould return a collection of splited graph/////////////////////////////////////////////
            Func<IEnumerable<Vertex>, XGraph> FindGr = (VCollection) =>
             {
                 Dictionary<string, Edge> gEdge = new Dictionary<string, Edge>();

                 List<Vertex> gVert = new List<Vertex>() { };

                 Queue<Vertex> gVertToVisit = new Queue<Vertex>();

                 gVertToVisit.Enqueue(VCollection.First());


                 while (gVertToVisit.Count > 0)
                 {
                     var firstItem = gVertToVisit.Peek();

                     Dictionary<string, Edge> EdgesOfVertex = _EdgeManager.GetAllEdgetOFVertix(firstItem);

                     //if single node
                     if (!EdgesOfVertex.Any() && !gVert.Contains(firstItem))
                     {
                         gVert.Add(firstItem);
                     }

                     foreach (var item in EdgesOfVertex)
                     {
                         //if vertex not visited add it to visit Q
                         if (!gVertToVisit.Contains(item.Value.vertex2))
                         {
                             gVertToVisit.Enqueue(item.Value.vertex2);
                         }
                         //add the edge to Graph collection
                         gEdge.TryAdd(item.Key, item.Value);

                         //try to add vertex1 to the vertex Collection
                         if (!gVert.Contains(item.Value.vertex1))//TODO:Considre to remove this, we are starting from Vertex1
                         {
                             gVert.Add(item.Value.vertex1);
                         }

                         if (!gVert.Contains(item.Value.vertex2))
                         {
                             gVert.Add(item.Value.vertex2);
                         }

                     }

                     //remove the node from visit list and from remaining nodes list
                     tmpVertex.Remove(gVertToVisit.Dequeue());
                 }

                 //create and return XGraph
                 return new XGraph(gVert, gEdge);
             };
            ////////////////////////////////////////////////////////////////////////////

            //this is the main loop
            //loop vertex and extract the XGraph
            while (tmpVertex.Count > 0)
            {
                XGraph graph = FindGr(tmpVertex);
                xGraphs.Add(graph);
            }

            return xGraphs;
        }

        /// <summary>
        /// returns true if teh vertex Exists in the Graph
        /// </summary>
        /// <param name="vertexID"></param>
        /// <returns></returns>
        public bool VericexExists(int vertexID)
        {
            return _VertexManager.IsExists(vertexID);
        }
    }
}
