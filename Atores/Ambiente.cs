using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Atores.Interfaces;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;
using Dominio;
using Microsoft.ServiceFabric.Actors.Client;
using Entidades;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Metodos;
using System.Net.Http.Headers;
using System.Dynamic;
using System.Collections.Generic;

namespace Atores
{
    [StatePersistence(StatePersistence.Persisted)]
    internal class Ambiente : Actor, IAmbiente
    {
        EstadoDispositivo Estado = new EstadoDispositivo();
        EstadoEquipamento EquipEstado = new EstadoEquipamento();

        private readonly string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=victorpuc;AccountKey=D9cN80DLeOGyENshbh/PyocYoR9r0y8JRFi+VkqjzXMwXvYyVNB6HD01waENi4kxf4wkRqwkzHUvR5LkOljGpQ==;EndpointSuffix=core.windows.net";

        public Ambiente(ActorService actorService, ActorId actorId)
            : base(actorService, actorId)
        {
        }

        protected override Task OnActivateAsync()
        {
            ActorEventSource.Current.ActorMessage(this, "Actor activated.");
            return this.StateManager.TryAddStateAsync("count", 0);
        }

        public Task MeAtivarAsync(string cliente, string ambiente, string dispositivo, int versao)
        {
            //Cria Tabela em Microsoft Azure através de uma ConnectionString
            CloudTable cloudTable;
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(storageConnectionString);
            var cloudTableClient = cloudStorageAccount.CreateCloudTableClient();
            cloudTable = cloudTableClient.GetTableReference("EquipamentosIoT");
            cloudTable.CreateIfNotExistsAsync();
            //Modelo de dados a adicionar na tabela Equipamentos onde 'dispositivo' é a partitionKey, 'cliente' é a rowKey
            var Dispositivo = new EntidadeDispositivo(dispositivo, cliente) { Ambiente = ambiente, Versao = versao.ToString() };
            //Realiza a Inserção da Tabela
            TableOperation insertOperation = TableOperation.InsertOrReplace(Dispositivo);
            cloudTable.ExecuteAsync(insertOperation);

            //Salva as Informações do Dispositivo
            Estado._infoDispositivo = new InfoDispositivo()
            {
                Cliente = cliente,
                Ambiente = ambiente,
                Dispositivo = dispositivo,
                Versao = versao
            };
            Estado._grupoId = cliente;

            //Cria um proxy de comunicação com o Ator(Residencia) que salvará os dados de todos os dispositivos
            var grupoDispositivo = ActorProxy.Create<IResidencia>(new ActorId(Estado._grupoId));
            return grupoDispositivo.RegistrarDispositivo(Estado._infoDispositivo);
        }

        public Task Equipamento(string dispositivo, PostEquipamento post)
        {
            var grupoDispositivo = ActorProxy.Create<IResidencia>(new ActorId(Estado._grupoId));
            if (post.Equipamento == "V")
            {
                if (!EquipEstado.DicVentilador.ContainsKey(post.Numero))
                    EquipEstado.DicVentilador.Add(post.Numero, post.Valor);
                else
                    EquipEstado.DicVentilador[post.Numero] = post.Valor;

                grupoDispositivo.EstadoDispositivosGrupo(dispositivo + "Vent", EquipEstado.DicVentilador);
            }
            if (post.Equipamento == "L")
            {
                if (!EquipEstado.DicLampada.ContainsKey(post.Numero))
                    EquipEstado.DicLampada.Add(post.Numero, post.Valor);
                else
                    EquipEstado.DicLampada[post.Numero] = post.Valor;

                grupoDispositivo.EstadoDispositivosGrupo(dispositivo + "Lamp", EquipEstado.DicLampada);
            }
            if (post.Equipamento == "U")
            {
                if (!EquipEstado.DicUmidificador.ContainsKey(post.Numero))
                    EquipEstado.DicUmidificador.Add(post.Numero, post.Valor);
                else
                    EquipEstado.DicUmidificador[post.Numero] = post.Valor;

                grupoDispositivo.EstadoDispositivosGrupo(dispositivo + "Umid", EquipEstado.DicUmidificador);
            }

            string JsonEnvio = JsonConvert.SerializeObject(post);
            string endpoint = "http://" + dispositivo + ".ngrok.io/api/Equipamento";
            HttpClient client = new HttpClient();
            var content = new StringContent(JsonEnvio, Encoding.UTF8, "application/json");
            var result =  client.PostAsync(endpoint, content);
            return null;
        }

        public Task<string> StatusSensores(string dispositivo)
        {
            string endpoint = "http://" + dispositivo + ".ngrok.io/api/Status";
            HttpClient client = new HttpClient();
            var result = client.GetStringAsync(endpoint);
            return result;
        }
    }
}