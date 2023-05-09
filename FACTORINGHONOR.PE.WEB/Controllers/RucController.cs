using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using FACTORINGHONOR.PE.CORE.Helpers;
using FACTORINGHONOR.PE.WEB.ViewModels;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FACTORINGHONOR.PE.WEB.Controllers
{
    [Authorize]
    [Route("ruc")]
    public class RucController : Controller
    {
        [HttpGet("{ruc}")]
        public async Task<IActionResult> GetRucInformation(string ruc)
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("modo", "1"),
                new KeyValuePair<string, string>("nruc", ruc)
            });

            var httpClient = new HttpClient();
            var response = await httpClient.PostAsync(ConstantHelpers.Url.RUC_CHECK, formContent);

            if(response.StatusCode != HttpStatusCode.OK)
                return BadRequest("RUC no encontrado.");

            var html = await response.Content.ReadAsStringAsync();

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var data = new List<string>();

            var parentNode = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='panel panel-primary']");
            if (parentNode == null)
                return BadRequest("RUC no encontrado.");

            foreach (var node in parentNode.SelectNodes("//div[@class='col-sm-7']"))
            {
                data.Add(node.InnerText.Trim());
            }

            if (string.IsNullOrEmpty(data[0]))
                return BadRequest("RUC no encontrado.");

            var model = new RucInfoViewModel
            {
                Name = data[0],
                Status = data[1],
                Condition = data[2],
                Ubigeo = data[3],
                Department = data[4],
                Province = data[5],
                District = data[6],
                Address = data[7],
            };

            return Ok(model);
        }
    }
}