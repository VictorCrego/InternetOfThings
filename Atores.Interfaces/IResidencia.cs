using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Remoting.FabricTransport;
using Microsoft.ServiceFabric.Services.Remoting;

namespace Atores.Interfaces
{
    public interface IResidencia : IActor
    {
        Task RegistrarDispositivo(InfoDispositivo infoDispositivo);
        Task ApagarRegistroDispositivo(InfoDispositivo infoDispositivo);
    }
}