using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TISelvagem.Dominio;
using TISelvagem.Dominio.contrato;
using TISelvagem.Repositorio;

namespace TISelvagem.RepositorioADO
{
    public class AlunoRepositorioADO : IRepositorio<Aluno>
    {
        private Contexto contexto;

        private void Inserir(Aluno aluno)
        {
            StringBuilder strQuery = new StringBuilder();
            strQuery.Append(" INSERT INTO Aluno (Nome, Mae, DataNascimento) VALUES ('");
            strQuery.Append(aluno.Nome);
            strQuery.Append("','");
            strQuery.Append(aluno.Mae);
            strQuery.Append("','");
            strQuery.Append(aluno.DataNascimento.ToString("yyyy-MM-dd"));
            strQuery.Append("');");

            using (contexto = new Contexto())
            {
                contexto.ExecutaComando(strQuery.ToString());
            }

        }

        private void Alterar(Aluno aluno)
        {
            StringBuilder strQuery = new StringBuilder();
            strQuery.Append("UPDATE Aluno SET");
            strQuery.Append(" Nome = '");
            strQuery.Append(aluno.Nome);
            strQuery.Append("', Mae = '");
            strQuery.Append(aluno.Mae);
            strQuery.Append("', DataNascimento = '");
            strQuery.Append(aluno.DataNascimento.ToString("yyyy-MM-dd"));
            strQuery.Append("' WHERE Id = ");
            strQuery.Append(aluno.Id);

            using (contexto = new Contexto())
            {
                contexto.ExecutaComando(strQuery.ToString());
            }

        }

        public void Salvar(Aluno aluno)
        {
            if (aluno.Id > 0)
            {
                Alterar(aluno);
            }
            else
            {
                Inserir(aluno);
            }
        }

        public void Excluir(Aluno aluno)
        {
            using (contexto = new Contexto())
            {
                string strQuery = "DELETE FROM Aluno WHERE Id = " + aluno.Id.ToString();
                contexto.ExecutaComando(strQuery);
            }
        }

        public IEnumerable<Aluno> ListarTodos()
        {
            using (contexto = new Contexto())
            {
                string strQuery = "SELECT * FROM Aluno";
                var retornoDataReader = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaReaderEmListaDeObjeto(retornoDataReader);
            }
        }

        public Aluno ListarPorId(string id)
        {
            using (contexto = new Contexto())
            {
                string strQuery = "SELECT * FROM Aluno WHERE Id = " + id;
                var retornoDataReader = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaReaderEmListaDeObjeto(retornoDataReader).FirstOrDefault();
            }
        }

        private List<Aluno> TransformaReaderEmListaDeObjeto(SqlDataReader reader)
        {
            List<Aluno> listaAluno = new List<Aluno>();

            while (reader.Read())
            {
                Aluno aluno = new Aluno();
                aluno.Id = Convert.ToInt32(reader["Id"]);
                aluno.Nome = reader["Nome"].ToString();
                aluno.Mae = reader["Mae"].ToString();
                aluno.DataNascimento = Convert.ToDateTime(reader["DataNascimento"]);

                listaAluno.Add(aluno);
            }
            reader.Close();

            return listaAluno;

        }
    }
}
