using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Dominio
{
    [DataContract]
    public class Sensores
    {
        [DataMember]
        public float Temperatura {get; set;}
        [DataMember]
        public float Luminosidade { get; set; }
        [DataMember]
        public float Umidade { get; set; }
    }
}
