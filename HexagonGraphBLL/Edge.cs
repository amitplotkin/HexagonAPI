using System;
using System.Collections.Generic;
using System.Text;

namespace HexagonGraphBLL
{
    [Serializable]
    public class Edge
    {
        public Edge(Vertex from,Vertex to)
        {
        
            this.vertex1 = from;
            this.vertex2 = to;
        }
              

        public Vertex vertex1 { get;  set; }
        public Vertex vertex2 { get;  set; }


        public static bool IsSame(Edge edge1, Edge edge2)
        {
            if ((edge1.vertex1==edge2.vertex1 && edge1.vertex2==edge2.vertex2)|| //if same vertex
                (edge1.vertex1 == edge2.vertex2 && edge1.vertex2 == edge2.vertex1))//reverse vertex should not exist
            {
                return true;
            }
            return false;
        }
    }
}
