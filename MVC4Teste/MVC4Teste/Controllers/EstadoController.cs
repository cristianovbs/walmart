using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using negWalMart.Models;
using negWalMart.Classes;
using System.Web.Services;

namespace MVC4Teste.Controllers
{
    public class EstadoController : Controller
    {
        //
        // GET: /Estado/

        public ActionResult Index()
        {
            ModelEstado est = new ModelEstado();
            //retornamos para as View
            return View(est.Lista());
        }

        public ActionResult Editar(int estado)
        {
            //cria uma instância da classe estado
            ModelEstado est = new ModelEstado();
            est.Codigo = estado;
            est = est.seleciona();
            return View(est);
        }

        [HttpPost]
        public ActionResult EditarEstado(FormCollection collection)
        {
            try
            {
                //cria uma instância da classe Estado
                ModelEstado est = new ModelEstado();
                //preenche os dados
                est.Codigo = (int.Parse(collection["Codigo"]));
                est.Pais = collection["Pais"];
                est.Nome = collection["Nome"];
                est.Sigla = collection["Sigla"];
                est.Regiao = collection["Regiao"];
                //dispara o método para adicionar no banco
                est.edita();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Adicionar()
        {
            return View(new ModelEstado());
        }

        [HttpPost]
        public ActionResult AdicionarEstado(FormCollection collection)
        {
            try
            {
                //cria uma instância da classe Estado
                ModelEstado est = new ModelEstado();
                //preenche os dados
                est.Codigo = (int.Parse(collection["Codigo"]));
                est.Pais = collection["Pais"];
                est.Nome = collection["Nome"];
                est.Sigla = collection["Sigla"];
                est.Regiao = collection["Regiao"];
                //dispara o método para adicionar no banco
                est.adiciona();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // POST: /Estado/Delete/5
        [HttpPost, WebMethod()]
        public ActionResult Delete(int id)
        {
            try
            {
                //criamos uma instância da classe estado
                ModelEstado est = new ModelEstado();
                //passamos o ID do estado para excluir
                est.Codigo = id;
                //exclhi o estado
                est.Excluir();
                //retorna para a view
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}
