using TISelvagem.RepositorioADO;
using TISelvagem.RepositorioEF;

namespace TISelvagem.Aplicacao
{
    public class AlunoAplicacaoConstrutor
    {
        public static AlunoAplicacao AlunoAplicacaoADO()
        {
            return new AlunoAplicacao(new AlunoRepositorioADO());
        }

        public static AlunoAplicacao AlunoRepositorioEF()
        {
            return new AlunoAplicacao(new AlunoRepositorioEF());
        }
    }
}
