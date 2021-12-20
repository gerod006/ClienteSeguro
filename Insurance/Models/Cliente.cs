using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Insurance.Models
{
    public class Cliente
    {
        public string cedula { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string telefono { get; set; }
        public DateTime? fecha_nacimiento { get; set; }

    }

    public class ObjetoClientes
    {
        
        public List<ClienteObj> ModeloObjetoClientes { get; set; }
        public List<int> seguro { get; set; }

    }

    public class ClienteObj
    {
        public string cedula { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string telefono { get; set; }



    }
}