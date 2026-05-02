using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Negocio
{
    public class EstilosServer
    {
        public List<Estilos> ListarEstilos()
        {
        
            List<Estilos> ListEstilo = new List<Estilos>();
            AccesoDatos Datos = new AccesoDatos();

            Datos.SetearConsulta("select Id, Descripcion as Estilo from ESTILOS\r\n");
            Datos.EjecutarReader();

            try
            {

                while (Datos.Reader.Read())
                {
                    Estilos aux = new Estilos();

                    if (!(Datos.Reader.IsDBNull(Datos.Reader.GetOrdinal("Id"))))
                        aux.Id = (int)Datos.Reader["Id"];
                    aux.Descripcion = (string)Datos.Reader["Estilo"];

                    ListEstilo.Add(aux);
                }

                return ListEstilo;
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                Datos.ConnectionClose();
            }
        }


    }
}
