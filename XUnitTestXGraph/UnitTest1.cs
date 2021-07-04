using System;
using Xunit;
using HexagonGraphBLL;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

namespace XUnitTestXGraph
{
    public class UnitTest1
    {
        IList<Edge> eList;
        IList<Vertex> vList;
        public UnitTest1()
        {
            eList = new List<Edge>();
            vList = new List<Vertex>();
            InitData();
        }


        public void InitData()
        {
            string fileName = @"D:\Hexagon\XUnitTestXGraph\DemoJson.json";

            using (StreamReader streamReader = File.OpenText(fileName))
            {
                string json = streamReader.ReadToEnd();
                JObject jObj = JObject.Parse(json);

                eList = JsonConvert.DeserializeObject<IList<Edge>>( jObj["EList"].ToString());
                vList = JsonConvert.DeserializeObject<IList<Vertex>>(jObj["VList"].ToString());
            }
        }

        [Fact]
        public void CreateDataJson()
        {
            return;

            //eList.Add(new Edge(new Vertex() { PointX = 1 }, new Vertex() { PointX = 2 }));
            //eList.Add(new Edge(new Vertex() { PointX = 1 }, new Vertex() { PointX = 3 }));
            //eList.Add(new Edge(new Vertex() { PointX = 3 }, new Vertex() { PointX = 2 }));

            //eList.Add(new Edge(new Vertex() { PointX = 4 }, new Vertex() { PointX = 4 }));


            //eList.Add(new Edge(new Vertex() { PointX = 5 }, new Vertex() { PointX = 6 }));

            //eList.Add(new Edge(new Vertex() { PointX = 4 }, new Vertex() { PointX = 4 }));


            //eList.Add(new Edge(new Vertex() { PointX = 8 }, new Vertex() { PointX = 9 }));
            //eList.Add(new Edge(new Vertex() { PointX = 9 }, new Vertex() { PointX = 10 }));
            //eList.Add(new Edge(new Vertex() { PointX = 9 }, new Vertex() { PointX = 11 }));


            //vList.Add(new Vertex() { PointX = 1 });
            //vList.Add(new Vertex() { PointX = 2 });
            //vList.Add(new Vertex() { PointX = 3 });
            //vList.Add(new Vertex() { PointX = 4 });
            //vList.Add(new Vertex() { PointX = 5 });
            //vList.Add(new Vertex() { PointX = 6 });
            //vList.Add(new Vertex() { PointX = 7 });
            //vList.Add(new Vertex() { PointX = 8 });
            //vList.Add(new Vertex() { PointX = 9 });
            //vList.Add(new Vertex() { PointX = 10 });
            //vList.Add(new Vertex() { PointX = 11 });
            //vList.Add(new Vertex() { PointX = 12 });
            //vList.Add(new Vertex() { PointX = 13 });

            //JObject jObj = new JObject();
            //jObj["EList"] = JToken.FromObject(eList);
            //jObj["VList"] = JToken.FromObject(vList);

            //string MyJson = jObj.ToString();
        }

        [Fact]
        public void Test1()
        {


            IGraph graph = new XGraph();

            var v1 = new Vertex() { PointX = 1 };
            var v2 = new Vertex() { PointX = 2 };
            var v3 = new Vertex() { PointX = 3 };
            var v4 = new Vertex() { PointX = 4 };
            var v5 = new Vertex() { PointX = 1 };
            var v6 = new Vertex() { PointX = 1 };
            var v7 = new Vertex() { PointX = 7 };
            graph.AddVertex(v1);
            graph.AddVertex(v2);
            graph.AddVertex(v3);
            graph.AddVertex(v4);
            graph.AddVertex(v5);

            graph.AddEdge(new Edge( v1,  v2));
            graph.AddEdge(new Edge( v2,  v3));
            graph.AddEdge(new Edge( v6,  v7));
            graph.AddEdge(new Edge( v7,  v1));
        }


        [Fact]
        public void TestCreateionOfGraph()
        {
            XGraph graph = new XGraph();

            foreach (var item in vList)
            {
                graph.AddVertex(item);
            }

            foreach (var item in eList)
            {
                graph.AddEdge(item);
            }

            IEnumerable<XGraph> result = graph.Split();
            Assert.Equal(7, result.Count());
        }
    }
}
