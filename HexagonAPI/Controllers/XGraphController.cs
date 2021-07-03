using HexagonGraphBLL;
using Microsoft.AspNetCore.Mvc;
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
        private XGraph graph;
        public XGraphController()
        {
            InitData();
          

        }


        private void InitData()
        {
            string fileName = @"D:\Hexagon\HexagonAPI\DemoJson.json";
            IList<Edge> eList;
            IList<Vertex> vList;

            using (StreamReader streamReader = System.IO.File.OpenText(fileName))
            {
                string json = streamReader.ReadToEnd();
                JObject jObj = JObject.Parse(json);

                eList = JsonConvert.DeserializeObject<IList<Edge>>(jObj["EList"].ToString());
                vList = JsonConvert.DeserializeObject<IList<Vertex>>(jObj["VList"].ToString()); 
                graph = new XGraph(vList,eList);
                
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
    }
}
