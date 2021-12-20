using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Script.Serialization;
using Insurance;
using Insurance.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Insurance.Controllers
{
    public class ClienteController : ApiController
    {
        private SeguroEntities db = new SeguroEntities();

        // GET: api/Cliente
        public RespuestaModelo GetClientes()
        {
            RespuestaModelo respuestaModelo = new RespuestaModelo();
            respuestaModelo.ProcesoExitoso = true;
            respuestaModelo.Respuesta.Add(db.cliente);

            return respuestaModelo;
           
        }

        [Route("api/seguros")]
        [HttpGet]
        public RespuestaModelo GetSeguros()
        {
            RespuestaModelo respuestaModelo = new RespuestaModelo();
            respuestaModelo.ProcesoExitoso = true;
            respuestaModelo.Respuesta.Add(db.seguro);

            return respuestaModelo;

        }

        [Route("api/BuscarCedula")]
        [HttpGet]
        public RespuestaModelo BuscarCedula(string cedula)
        {
            RespuestaModelo respuestaModelo = new RespuestaModelo();

            var query = from cs in db.cliente_seguro
                        join c in db.cliente
                            on cs.id_cliente equals c.id
                        join s in db.seguro
                           on cs.id_seguro equals s.id
                        where c.cedula == cedula
                        select new { s.nombre, s.codigo, s.suma_asegurada };
                    


            respuestaModelo.ProcesoExitoso = true;
            respuestaModelo.Respuesta.Add(query);

            return respuestaModelo;

        }

        [Route("api/BuscarSeguro")]
        [HttpGet]
        public RespuestaModelo BuscarSeguro(string seguro)
        {
            RespuestaModelo respuestaModelo = new RespuestaModelo();

            var query = from cs in db.cliente_seguro
                        join c in db.cliente
                            on cs.id_cliente equals c.id
                        join s in db.seguro
                           on cs.id_seguro equals s.id
                        where s.codigo == seguro
                        select new { c.nombres, c.apellidos, c.cedula };



            respuestaModelo.ProcesoExitoso = true;
            respuestaModelo.Respuesta.Add(query);

            return respuestaModelo;

        }



        [Route("api/CargarCliente")]
        [HttpPost]
        public RespuestaModelo CargarCliente(ObjetoClientes clientes)
        {
            RespuestaModelo respuestaModelo = new RespuestaModelo();

            // JObject jsonClientes = JObject.Parse(clientes);

            // var clienteList = JsonConvert.DeserializeObject<List<Cliente>>(clientes);
            //var list = this.db.cliente.Select(x => x.cedula);
            String cedulaParametro, nombresParametro, apellidosParametro, telefonoParametro;
            long idTmp;
            for (int i = 0; i < clientes.ModeloObjetoClientes.Count; i++)
            {
                cedulaParametro = clientes.ModeloObjetoClientes[i].cedula.ToString();
                nombresParametro = clientes.ModeloObjetoClientes[i].nombres.ToString();
                apellidosParametro = clientes.ModeloObjetoClientes[i].apellidos.ToString();
                telefonoParametro = clientes.ModeloObjetoClientes[i].telefono.ToString();

                if (db.cliente.Count(e => e.cedula == cedulaParametro) == 0)
                {                   
                        cliente objCli = new cliente();
                        // fields to be insert
                        objCli.cedula = cedulaParametro;
                        objCli.nombres = nombresParametro;
                        objCli.apellidos = apellidosParametro;
                        objCli.telefono = telefonoParametro;
                        objCli.fecha_nacimiento = null;
                        objCli.fecha_creacion = DateTime.UtcNow.Date;
                        objCli.fecha_modificacion = null;
                        db.cliente.Add(objCli);
                        db.SaveChanges();
                        idTmp = objCli.id;

                      
                        for (int j = 0; j < clientes.seguro.Count; j++)
                        {
                            cliente_seguro objCliSeg = new cliente_seguro();
                            objCliSeg.id_cliente = idTmp;
                            objCliSeg.id_seguro = clientes.seguro[j];
                            db.cliente_seguro.Add(objCliSeg);
                        }
                        db.SaveChanges();


                }
                else
                {
                    var item = db.cliente.Single(k => k.cedula == cedulaParametro);

                    for (int j = 0; j < clientes.seguro.Count; j++)
                    {
                        var tmpCliSeg = clientes.seguro[j];
                        if (db.cliente_seguro.Count(e => e.id_cliente == item.id && e.id_seguro == tmpCliSeg) == 0){
                            cliente_seguro objCliSeg = new cliente_seguro();
                            objCliSeg.id_cliente = item.id;
                            objCliSeg.id_seguro = tmpCliSeg;
                            db.cliente_seguro.Add(objCliSeg);
                        }                      
                    }
                    db.SaveChanges();


                }
                 

            }
            

            respuestaModelo.ProcesoExitoso = true;
            //respuestaModelo.Respuesta.Add(query);

            return respuestaModelo;

        }

        // GET: api/Cliente/5
        [ResponseType(typeof(Cliente))]
        public IHttpActionResult GetCliente(int id)
        {
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        // PUT: api/Cliente/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCliente(string id, Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cliente.cedula)
            {
                return BadRequest();
            }

            db.Entry(cliente).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Cliente
        [ResponseType(typeof(Cliente))]
        public IHttpActionResult PostCliente(Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Clientes.Add(cliente);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cliente.cedula }, cliente);
        }

        // DELETE: api/Cliente/5
        [ResponseType(typeof(Cliente))]
        public IHttpActionResult DeleteCliente(int id)
        {
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return NotFound();
            }

            db.Clientes.Remove(cliente);
            db.SaveChanges();

            return Ok(cliente);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClienteExists(string id)
        {
            return db.Clientes.Count(e => e.cedula == id) > 0;
        }
    }
}