using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("api/AtivarMe")]
    public class AtivarMeController : Controller
    {
        // GET api/AtivarMe
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/AtivarMe/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/AtivarMe
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/AtivarMe/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/AtivarMe/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}