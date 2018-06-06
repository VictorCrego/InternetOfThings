using System.Threading.Tasks;
using Metodos;
using Microsoft.ServiceFabric.Actors;

namespace Atores.Interfaces
{
    public interface IAmbiente : IActor
    {
        Task MeAtivarAsync(string cliente, string ambiente, string dispositivo, int versao);
        Task Equipamento(string dispositivo, PostEquipamento post);
    }
}