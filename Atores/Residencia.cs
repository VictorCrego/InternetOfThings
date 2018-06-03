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
            Estado._listaDispositivos.Add(infoDispositivo);
            return Task.FromResult(true);
        }

        public Task ApagarRegistroDispositivo(InfoDispositivo infoDispositivo)
        {
            Estado._listaDispositivos.Remove(infoDispositivo);
            return Task.FromResult(true);
        }

        /*protected override Task OnActivateAsync()
        {
            ActorEventSource.Current.ActorMessage(this, "Actor activated.");
            return this.StateManager.TryAddStateAsync("count", 0);
        }*/
    }
}