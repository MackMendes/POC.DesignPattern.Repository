using System.Web.Mvc;
using TISelvagem.Aplicacao;
using TISelvagem.Dominio;

namespace TISelvagem.UI.Web.Controllers
{
    public class AlunoController : Controller
    {
        private readonly AlunoAplicacao appAluno;

        public AlunoController()
        {
            appAluno = AlunoAplicacaoConstrutor.AlunoRepositorioEF();
        }

        public ActionResult Index()
        {
            var listAluno = appAluno.ListarTodos();
            return View(listAluno);
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Gera um código no formulário, para que seja recebido aqui. Assim, quando outro pessoa de outro site não conseguir postar aqui nessa View.
        public ActionResult Cadastrar(Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                appAluno.Salvar(aluno);
                return RedirectToAction("Index");
            }

            return View(aluno);
        }

        public ActionResult Editar(string id)
        {
            var aluno = appAluno.ListarPorId(id);

            if (aluno == null)
            {
                return HttpNotFound();
            }

            return View(aluno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                appAluno.Salvar(aluno);
                return RedirectToAction("Index");
            }

            return View(aluno);
        }

        public ActionResult Detalhes(string id)
        {
            var aluno = appAluno.ListarPorId(id);

            if (aluno == null)
            {
                return HttpNotFound();
            }

            return View(aluno);
        }

        public ActionResult Excluir(string id)
        {
            var aluno = appAluno.ListarPorId(id);

            if (aluno == null)
            {
                return HttpNotFound();
            }

            return View(aluno);
        }

        [HttpPost, ActionName("Excluir")] //Isso foi colocado para que a View acima (Excluir) possa vê esse Action (ExcluirConfirmado) como uma View de retorno.
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirConfirmado(string id)
        {
            var aluno = appAluno.ListarPorId(id);
            appAluno.Excluir(aluno);
            return RedirectToAction("Index");
        }
    }
}
