using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Insurance.Models
{
    public class RespuestaModelo
    {
        public RespuestaModelo()
        {
            Respuesta = new List<object>();
        }

        public string NumeroError { get; set; }
        public string MensajeError { get; set; }
        public Boolean ProcesoExitoso { get; set; }
        public string DetalleError { get; set; }
        public List<Object> Respuesta { get; set; }

    }
}

 