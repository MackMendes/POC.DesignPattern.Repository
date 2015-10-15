using System;
using TISelvagem.Aplicacao;
using TISelvagem.Dominio;

namespace UI.Dos
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var appAluno = AlunoAplicacaoConstrutor.AlunoAplicacaoADO();

                Console.Write("Digite o nome do aluno: ");
                string nome = Console.ReadLine();

                Console.Write("Digite o nome da mãe do aluno: ");
                string mae = Console.ReadLine();

                Console.Write("Digite a data de nascimento do aluno: ");
                string data = Console.ReadLine(); //Data formato americano

                var aluno = new Aluno() { Nome = nome, Mae = mae, DataNascimento = Convert.ToDateTime(data) };

                appAluno.Salvar(aluno);

                foreach (var aluno1 in appAluno.ListarTodos())
                {
                    Console.WriteLine("Id: {0}, Nome: {1}, Mãe: {2}, Mãe: DataNascimento: {3}", aluno1.Id, aluno1.Nome, aluno1.Mae, aluno1.DataNascimento);
                }
            }
            catch (Exception ex)
            {
                Console.Write("Erro: " + ex.Message);
            }

            Console.Read();
        }
    }
}
