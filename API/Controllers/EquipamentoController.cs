using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Atores.Interfaces;
using Metodos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("api/Equipamento")]
    public class EquipamentoController : Controller
    {
        // GET: api/Equipamento
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Equipamento/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Equipamento
        [HttpPost("{dispositivo}")]
        public IActionResult Post(string dispositivo, [FromBody]PostEquipamento post)
        {
            try
            { 
                var actor = ActorProxy.Create<IAmbiente>(new ActorId(dispositivo), new Uri("fabric:/InternetOfThings/AmbienteActorService"));
                actor.Equipamento(dispositivo, post);
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }
        
        // PUT: api/Equipamento/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
