using HexagonGraphBLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HexagonAPI.Controllers
{
    [Route("XGraph")]
    public class XGraphController : Controller
    {
        private readonly IConfiguration Configuration;
        
        private static XGraph graph;
        public XGraphController(IConfiguration configuration)
        {

            Configuration = configuration;
            if (graph==null)
            {
                InitData();
            }
        

        }

        private void InitData()
        {
            string fileName = Configuration["DataJsonFile"];// @"E:\MyGit\HexagonAPI\HexagonAPI\DemoJson.json";
            IList<Edge> eList;
            IList<Vertex> vList;

            using (StreamReader streamReader = System.IO.File.OpenText(fileName))
            {
                string json = streamReader.ReadToEnd();
                JObject jObj = JObject.Parse(json);

                eList = JsonConvert.DeserializeObject<IList<Edge>>(jObj["EList"].ToString());
                vList = JsonConvert.DeserializeObject<IList<Vertex>>(jObj["VList"].ToString());
                graph = new XGraph(vList, eList);
            

            }
        }

        [HttpGet]
        public IActionResult GetGraph()
        {
            return Json(graph);
        }

        [HttpGet("/split")]
        public IActionResult GetGraphSplit()
        {
            return Json(graph.Split());
        }

        [HttpPost]
        public IActionResult SetGraph()
        {
            return Json(new { id = 1, value = "set" });
        }

        [HttpDelete]
        public IActionResult DeleteGraph()
        {
            return Json(new { id = 1, value = "delete" });
        }

        [HttpPut]
        public IActionResult UpdateGraph()
        {
            return Json(new { id = 1, value = "update" });
        }

        [HttpGet("AddVertex")]
        public IActionResult AddVertex(int NewVertexID)
        {
            try
            {
                Vertex newVertex = new Vertex() { PointX = NewVertexID };
                graph.AddVertex(newVertex);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        [HttpGet("IsVertexExists")]
        public IActionResult IsVertexExists(int VertexID)
        {
            try
            {
              
              bool result = graph.VericexExists(VertexID);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        [HttpGet("AddEdge")]
        public IActionResult AddEdge(int from_Vertex, int to_Vertex)
        {
            try
            {
                Vertex v1 = new Vertex() { PointX = from_Vertex };
                Vertex v2 = new Vertex() { PointX = to_Vertex };
                Edge newEdge = new Edge(v1,v2);
                graph.AddEdge(newEdge);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        [HttpGet("GetEdge")]
        public IActionResult GetEdge(string EdgeId)
        {
            try
            {

                if (!graph.Edges.ContainsKey(EdgeId))
                {
                    return NotFound(EdgeId);
                }
                Edge e=graph.Edges[EdgeId];
              
                return Ok(e);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }
    }
}
