using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio;
using System.Windows.Forms;

namespace Negocio 
{
    public class DiscosServer
    {
        public List<Disco> ListaDisco()
        {
            List<Disco> ListaD = new List<Disco>();

            SqlConnection conexion = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            conexion.ConnectionString = "server=(local)\\SQLEXPRESS; database=DISCOS_DB; integrated security=true;";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select Titulo, FechaLanzamiento, CantidadCanciones, UrlImagenTapa as Cover, e.Descripcion as Estilo, t.Descripcion as Formato, E.Id as IdEstilo, T.Id as IdEdicion, D.Id as IdDisco, D.Activo from DISCOS D, ESTILOS E, TIPOSEDICION T where e.Id = D.IdEstilo and t.Id = D.IdTipoEdicion and D.Activo = 1";
            cmd.Connection = conexion;

            conexion.Open();
            reader = cmd.ExecuteReader(); 

            try
            {
                while (reader.Read())
                {   
                    Disco disco1 = new Disco();

                    disco1.Id = (int)reader["IdDisco"];
                    disco1.Titulo = (string)reader["Titulo"];
                    disco1.FechaLanzamiento = reader.GetDateTime(1);
                    disco1.CantidadDeCanciones = (int)reader["CantidadCanciones"];

                    //if(!(reader.IsDBNull(reader.GetOrdinal("Cover"))))
                    //disco1.UrlImagenCover = (string)reader["Cover"];
                    if (!(reader["Cover"] is DBNull))
                        disco1.UrlImagenCover = (string)reader["Cover"];

                    disco1.Estilo = new Estilos();
                    disco1.Estilo.Descripcion = (string)reader["Estilo"];
                    disco1.Estilo.Id = (int)reader["IdEstilo"];
                    disco1.Edicion = new TipoEdicion();
                    disco1.Edicion.Id = (int)reader["IdEdicion"];
                    disco1.Edicion.Description = (string)reader["Formato"];
                    disco1.Activo = (bool)reader["Activo"];


                    ListaD.Add(disco1);
                }

                return ListaD;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conexion.Close();
            }

        }

        public List<Disco> listarConSP() 
        {
            List<Disco> discosLista = new List<Disco>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "listarDiscos";

                datos.SetearConsulta(consulta);
                datos.EjecutarReader();

                while (datos.Reader.Read())
                {
                    Disco disco = new Disco();

                    disco.Id = (int)datos.Reader["IdDisco"];
                    disco.Titulo = (string)datos.Reader["Titulo"];
                    disco.FechaLanzamiento = (DateTime)datos.Reader["FechaLanzamiento"];
                    disco.CantidadDeCanciones = (int)datos.Reader["CantidadCanciones"];
                    disco.UrlImagenCover = (string)datos.Reader["Cover"];
                    disco.Estilo = new Estilos();
                    disco.Estilo.Descripcion = (string)datos.Reader["Estilo"];
                    disco.Edicion = new TipoEdicion();
                    disco.Edicion.Description = (string)datos.Reader["Formato"];

                    discosLista.Add(disco);
                }

                return discosLista;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return new List<Disco>();
            }
            finally
            {
                datos.ConnectionClose();
            }
        }

        public void Agregar(Disco disco)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("INSERT INTO DISCOS (Titulo, FechaLanzamiento, CantidadCanciones, IdEstilo, IdTipoEdicion, UrlImagenTapa) VALUES ('" + disco.Titulo + "', '" + disco.FechaLanzamiento + "', " + disco.CantidadDeCanciones + ", @IdEstilo, @IdTipoEdicion, @UrlImagenTapa)");
                datos.SetearParametros("@IdEstilo", disco.Estilo.Id);
                datos.SetearParametros("@IdTipoEdicion", disco.Edicion.Id);
                datos.SetearParametros("@UrlImagenTapa", disco.UrlImagenCover);
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.ConnectionClose();

            }
        }

        public void Modificar(Disco disco) 
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("update DISCOS set Titulo = @titulo, FechaLanzamiento = @fecha, CantidadCanciones = @cantidad, UrlImagenTapa = @url, IdEstilo = @idestilo, IdTipoEdicion = @idedicion where Id = @id");
                datos.SetearParametros("@titulo", disco.Titulo);
                datos.SetearParametros("@fecha", disco.FechaLanzamiento);
                datos.SetearParametros("@cantidad", disco.CantidadDeCanciones);
                datos.SetearParametros("@url", disco.UrlImagenCover);
                datos.SetearParametros("@idestilo", disco.Estilo.Id);
                datos.SetearParametros("@idedicion", disco.Edicion.Id);
                datos.SetearParametros("@id", disco.Id);
                datos.EjecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally 
            {
                datos.ConnectionClose();
            }
            
        }

        public void Eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            Disco disco = new Disco();

            try
            {
                datos.SetearConsulta("delete DISCOS where Id = @id");
                datos.SetearParametros("@id", id);
                datos.EjecutarAccion();

            }
            catch (Exception ex)
            {

                ex.ToString();
            }
        }

        public void EliminarLogico(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            Disco disco = new Disco();
            try
            {
                datos.SetearConsulta("update discos set activo = 0 where id = @Id");
                datos.SetearParametros("@Id", id);
                datos.EjecutarAccion();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            finally
            {
                datos.ConnectionClose();
            }
        }

        public List<Disco> Filtrar(string parametro, string criterio, string filtro)
        {
            List<Disco> discosListaFiltrada = new List<Disco>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "select Titulo, convert(date, d.FechaLanzamiento) as FechaLanzamiento, CantidadCanciones, UrlImagenTapa as Cover, e.Descripcion as Estilo, t.Descripcion as Formato, E.Id as IdEstilo, T.Id as IdEdicion, D.Id as IdDisco, D.Activo " +
                                  "from DISCOS D, ESTILOS E, TIPOSEDICION T " +
                                  "where e.Id = D.IdEstilo " +
                                  "and t.Id = D.IdTipoEdicion " +
                                  "and D.Activo = 1 and ";

                if (filtro == "")
                    return ListaDisco();

                switch (parametro)
                {
                    case "Titulo":
                        switch (criterio)
                        {
                            case "Comienza con...":
                                consulta += "Titulo like '" + filtro + "%'";
                                break;
                            case "Termina con...":
                                consulta += "Titulo like '%" + filtro + "'";
                                break;
                            case "Contiene...":
                                consulta += "Titulo like '%" + filtro + "%'";
                                break;
                        }
                        break;

                    case "Fecha de Lanzamiento":
                        switch (criterio)
                        {
                            case "Mayor a...":
                                consulta += "CONVERT(date, d.FechaLanzamiento) > " + "'" + filtro + "'";
                                break;
                            case "Menor a...":
                                consulta += "CONVERT(date, d.FechaLanzamiento) < " + "'" + filtro + "'";
                                break;
                            case "Igual a...":
                                consulta += "CONVERT(date, d.FechaLanzamiento) = " + "'" + filtro + "'";
                                break;
                        }
                        break;

                    case "Cantidad de Canciones":
                        switch (criterio)
                        {
                            case "Mayor a...":
                                consulta += "CantidadCanciones >  " + filtro;
                                break;
                            case "Menor a...":
                                consulta += "CantidadCanciones < " + filtro;
                                break;
                            case "Igual a...":
                                consulta += "CantidadCanciones = " + filtro; 
                                break;
                        }
                        break;
                }

                datos.SetearConsulta(consulta);
                datos.EjecutarReader();

                while (datos.Reader.Read())
                {
                    Disco disco = new Disco();

                    disco.Titulo = (string)datos.Reader["Titulo"];
                    disco.FechaLanzamiento = (DateTime)datos.Reader["FechaLanzamiento"];
                    disco.CantidadDeCanciones = (int)datos.Reader["CantidadCanciones"];
                    disco.UrlImagenCover = (string)datos.Reader["Cover"];
                    disco.Estilo = new Estilos();
                    disco.Estilo.Descripcion = (string)datos.Reader["Estilo"];
                    disco.Edicion = new TipoEdicion();
                    disco.Edicion.Description = (string)datos.Reader["Formato"];

                    discosListaFiltrada.Add(disco);    
                }

                return discosListaFiltrada;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return new List<Disco>();
            }
            finally
            {
                datos.ConnectionClose();
            }
        }
    }

}
