using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Atores.Interfaces;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;
using Dominio;
using Microsoft.ServiceFabric.Actors.Client;
using Entidades;

namespace Atores
{
    [StatePersistence(StatePersistence.Persisted)]
    internal class Ambiente : Actor, IAmbiente
    {
        EstadoDispositivo Estado = new EstadoDispositivo();
        private readonly string storageConnectionString = "";

        public Ambiente(ActorService actorService, ActorId actorId) 
            : base(actorService, actorId)
        {
        }

        /*protected override Task OnActivateAsync()
        {
            ActorEventSource.Current.ActorMessage(this, "Actor activated.");
            return this.StateManager.TryAddStateAsync("count", 0);
        }*/

        public Task MeAtivarAsync(string cliente, string ambiente, string dispositivo, int versao)
        {
            CloudTable cloudTable;
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(storageConnectionString);
            var cloudTableClient = cloudStorageAccount.CreateCloudTableClient();
            cloudTable = cloudTableClient.GetTableReference("Equipamentos");
            cloudTable.CreateIfNotExistsAsync();

            var Dispositivo = new EntidadeDispositivo(dispositivo, cliente)
            {
                Ambiente = ambiente,
                Versao = versao.ToString()
            };
            TableOperation insertOperation = TableOperation.InsertOrReplace(Dispositivo);
            cloudTable.ExecuteAsync(insertOperation);

            Estado._infoDispositivo = new InfoDispositivo()
            {
                Cliente = cliente,
                Ambiente = ambiente,
                Dispositivo = dispositivo,
                Versao = versao
            };
            Estado._grupoId = cliente;

            var grupoDispositivo = ActorProxy.Create<IResidencia>(new ActorId(Estado._grupoId));
            return grupoDispositivo.RegistrarDispositivo(Estado._infoDispositivo);
        }
    }
}