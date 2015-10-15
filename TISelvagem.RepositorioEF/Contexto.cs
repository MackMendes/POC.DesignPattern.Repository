using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using TISelvagem.Dominio;

namespace TISelvagem.RepositorioEF
{
    [DbConfigurationType(typeof(MyConfiguration))]
    public class Contexto : DbContext
    {
        public Contexto()
            : base("TISelvagemConfig")
        {

        }


        public DbSet<Aluno> Alunos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Remove do Entity a função de criar tabelas no Plural
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //Informando que os campos dentro da Entity Aluno é Requirido, possui o tipo informado e a quantidade Maxima de Caracteres (varchar)
            modelBuilder.Entity<Aluno>().Property(x => x.Nome).IsRequired().HasColumnType("varchar").HasMaxLength(75);
            modelBuilder.Entity<Aluno>().Property(x => x.Mae).IsRequired().HasColumnType("varchar").HasMaxLength(75);
            modelBuilder.Entity<Aluno>().Property(x => x.DataNascimento).IsRequired().HasColumnType("date");

        }
    }
}
