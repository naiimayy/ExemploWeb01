using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exemplo01.Controllers
{
    public class FrutaController : Controller
    {
        // GET: Fruta
        public ActionResult Index(string pesquisa)
        {
            FrutaRepository repository = new FrutaRepository();
            List<Fruta> frutas = repository.ObterTodos(pesquisa);

            ViewBag.Frutas = frutas;

            return View();
        }

        public ActionResult Cadastro()
        {
            return View();
        }

        public ActionResult Store(string nome, decimal preco)
        {
            Fruta fruta = new Fruta();
            fruta.Nome = nome;
            fruta.Preco = preco;

            FrutaRepository repository = new FrutaRepository();
            repository.Inserir(fruta);
            return RedirectToAction("Index");
        }

        public ActionResult Apagar(int id)
        {
            FrutaRepository repository = new FrutaRepository();
            repository.Apagar(id);
            return RedirectToAction("Index");

        }

        public ActionResult Editar(int id)
        {
            FrutaRepository repository = new FrutaRepository();
            Fruta fruta = repository.ObterPeloId(id);
            ViewBag.Fruta = fruta;
            return View();
        }

        public ActionResult Update(int id, string nome, decimal preco)
        {

            Fruta fruta = new Fruta();
            fruta.Id = id;
            fruta.Nome = nome;
            fruta.Preco = preco;
            FrutaRepository repository = new FrutaRepository();
            repository.Atualizar(fruta);
            return RedirectToAction("Index");

        }

    }
}