using Atores.Interfaces;
using Dominio;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using System.Threading.Tasks;

namespace Atores
{
    [StatePersistence(StatePersistence.Persisted)]
    internal class Residencia : Actor, IResidencia
    {
        EstadoDispositivoGrupo Estado = new EstadoDispositivoGrupo();

        public Residencia(ActorService actorService, ActorId actorId) : base(actorService, actorId)
        {

        }

        public Task RegistrarDispositivo(InfoDispositivo infoDispositivo)
        {
            if (!Estado._Dispositivos.ContainsKey(infoDispositivo.Dispositivo))
                Estado._Dispositivos.Add(infoDispositivo.Dispositivo, infoDispositivo);
            return Task.FromResult(true);
        }

        public Task ApagarRegistroDispositivo(InfoDispositivo infoDispositivo)
        {
            if (!Estado._Dispositivos.ContainsKey(infoDispositivo.Dispositivo))
                Estado._Dispositivos.Remove(infoDispositivo.Dispositivo);
            return Task.FromResult(true);
        }

        /*protected override Task OnActivateAsync()
        {
            ActorEventSource.Current.ActorMessage(this, "Actor activated.");
            return this.StateManager.TryAddStateAsync("count", 0);
        }*/
    }
}