using Atores.Interfaces;
using Dominio;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Atores
{
    [StatePersistence(StatePersistence.Persisted)]
    internal class Residencia : Actor, IResidencia
    {
        EstadoDispositivoGrupo Estado = new EstadoDispositivoGrupo();
        EstadoEquipamento EquipEstado = new EstadoEquipamento();

        public Residencia(ActorService actorService, ActorId actorId) : base(actorService, actorId)
        {

        }

        public Task EstadoDispositivosGrupo(string Dispositivo, Dictionary<int, bool> DicEstados)
        {
            if (!EquipEstado.DicEstados.ContainsKey(Dispositivo))
                EquipEstado.DicEstados.Add(Dispositivo, DicEstados);
            else
                EquipEstado.DicEstados[Dispositivo] = DicEstados;
            return null;
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