using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class EntidadeDispositivo : TableEntity
    {
        public EntidadeDispositivo(string dispositivo, string cliente)
        {
            PartitionKey = dispositivo;
            RowKey = cliente;
        }

        public string Ambiente { get; set; }
        public string Versao { get; set; }
    }
}
