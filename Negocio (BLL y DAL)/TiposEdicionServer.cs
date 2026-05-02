using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Negocio
{
    public class TiposEdicionServer
    {
        public List<TipoEdicion> ListEdicion()
        {
            List<TipoEdicion> tipoEdicionLista = new List<TipoEdicion>();
            AccesoDatos edicionDatos = new AccesoDatos();

            edicionDatos.SetearConsulta("select Id, Descripcion as Formato from TIPOSEDICION");
            edicionDatos.EjecutarReader();

            try
            {
                while (edicionDatos.Reader.Read()) 
                {
                    TipoEdicion edicion = new TipoEdicion();

                    if(!(edicionDatos.Reader.IsDBNull(edicionDatos.Reader.GetOrdinal("Id"))))
                        edicion.Id = (int)edicionDatos.Reader["Id"];
                    edicion.Description = (string)edicionDatos.Reader["Formato"];

                    tipoEdicionLista.Add(edicion);
                }

                return tipoEdicionLista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                edicionDatos.ConnectionClose();
            }
        }
    }
}
