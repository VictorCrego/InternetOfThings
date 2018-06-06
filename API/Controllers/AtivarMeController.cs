using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Atores.Interfaces;
using Metodos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;

namespace API.Controllers
{
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
        public IActionResult Post([FromBody]PostAtivar post)
        {
            try
            {
                var actor = ActorProxy.Create<IAmbiente>(new ActorId(post.Dispositivo), new Uri("fabric:/InternetOfThings/AmbienteActorService"));
                actor.MeAtivarAsync(post.Cliente, post.Ambiente, post.Dispositivo, post.Versao);
                return Ok();
            }
            catch
            {
                return NotFound();
            }
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