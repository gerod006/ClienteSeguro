using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Insurance
{
     
        public class ControlAccesoOpcion : DelegatingHandler
        {
            protected override async Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage solicitud, CancellationToken cancelacionToken)
            {
                var respuesta = await base.SendAsync(solicitud, cancelacionToken);

                if (solicitud.Method == HttpMethod.Options &&
                    respuesta.StatusCode == HttpStatusCode.MethodNotAllowed)
                {
                    respuesta = new HttpResponseMessage(HttpStatusCode.OK);
                }

                return respuesta;
            }
        }
     
}

 