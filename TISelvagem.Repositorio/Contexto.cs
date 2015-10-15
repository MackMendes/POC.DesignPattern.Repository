using System;
using System.Configuration;
using System.Data.SqlClient;

namespace TISelvagem.Repositorio
{
    public sealed class Contexto : IDisposable
    {
        private readonly SqlConnection minhaConexao;

        public Contexto()
        {
            try
            {
                string conectionString = String.Empty;
                conectionString = ConfigurationManager.ConnectionStrings["TISelvagemConfig"].ConnectionString;
                minhaConexao = new SqlConnection(conectionString);
                minhaConexao.Open();
            }
            catch (Exception)
            {
                minhaConexao.Close();
                throw;
            }

        }

        public void ExecutaComando(string query)
        {
            try
            {
                var cmdComando = new SqlCommand()
                {
                    CommandText = query,
                    CommandType = System.Data.CommandType.Text,
                    Connection = minhaConexao
                };

                cmdComando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                minhaConexao.Close();
                throw;
            }
        }

        public SqlDataReader ExecutaComandoComRetorno(string query)
        {
            SqlDataReader sqldtrd;

            try
            {
                var cmdComando = new SqlCommand(query, minhaConexao);
                sqldtrd = cmdComando.ExecuteReader();
            }
            catch (Exception)
            {
                minhaConexao.Close();
                throw;
            }

            return sqldtrd;
        }

        public void Dispose()
        {
            if (minhaConexao.State == System.Data.ConnectionState.Open)
            {
                minhaConexao.Close();
            }


            GC.Collect();
        }
    }
}
