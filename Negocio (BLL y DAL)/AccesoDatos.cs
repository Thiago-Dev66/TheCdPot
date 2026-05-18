using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Negocio
{
    public class AccesoDatos
    {
        private SqlConnection conn;
        private SqlCommand cmd;
        private SqlDataReader reader;
        public SqlDataReader Reader
        {
            get { return reader; }
        }

        public AccesoDatos()
        {
            conn = new SqlConnection("server=(local)\\SQLEXPRESS; database=DISCOS_DB; integrated security=true;");
            cmd = new SqlCommand();
        }
        public void SetearConsulta(string consulta)
        {
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = consulta;
        }
        public void SetearConsultaSP(string sp)
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = sp;
        }
        public void EjecutarReader()
        {
            cmd.Connection = conn;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public void EjecutarAccion()
        {
            cmd.Connection = conn;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void SetearParametros(string name, object value)
        {
            cmd.Parameters.AddWithValue(name, value);
        }
        public void ConnectionClose()
        {
            if (reader != null) 
                Reader.Close(); 
            conn.Close();
        }
    }
}
