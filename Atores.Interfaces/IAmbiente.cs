using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Remoting.FabricTransport;
using Microsoft.ServiceFabric.Services.Remoting;

namespace Atores.Interfaces
{
    public interface IAmbiente : IActor
    {
        Task MeAtivarAsync(string cliente, string ambiente, string dispositivo, int versao);
    }
}