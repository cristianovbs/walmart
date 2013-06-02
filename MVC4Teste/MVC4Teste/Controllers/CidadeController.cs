using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using negWalMart.Classes;
using negWalMart.Models;

namespace MVC4Teste.Controllers
{
    public class CidadeController : Controller
    {
        //
        // GET: /Cidade/

        public ActionResult Index()
        {
            //Instância da Classe Cidade
            ModelCidade lst = new ModelCidade();
            //Criamos uma instância da classe estado para carregarmos a combo de estados
            ModelEstado est = new ModelEstado();
            //passamos uma lista usando a ViewBag, para saber o nome do estado
            ViewBag.Estado = new SelectList(est.Lista(), "Codigo", "Nome", "");
            //retornamos para as View
            return View(lst.Lista());
        }

        [HttpPost]
        public ActionResult EditarCidade(FormCollection collection)
        {
            try
            {
                //cria uma instância da classe Cidade
                ModelCidade cid = new ModelCidade();
                //preenche os dados
                cid.Codigo = (int.Parse(collection["Codigo"]));
                cid.Nome = collection["Nome"];
                cid.Estado = (int.Parse(collection["Estado"]));
                cid.Capital = (bool.Parse(collection["Capital"].Split(',')[0]));

                //dispara o método para adicionar no banco
                cid.edita();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Cidade/Editar/5
        public ActionResult Editar(int cidade)
        {
            //criamos uma instância da classe ModelCidade
            ModelCidade cid = new ModelCidade();
            //definimos o id da cidade
            cid.Codigo = cidade;
            //preenchemos a classe com a cidade selecionada
            cid = cid.seleciona();
            //Criamos uma instância da classe estado para carregarmos a combo de estados
            ModelEstado est = new ModelEstado();
            //passamos uma lista usando a ViewBag, para preencher a combo
            ViewBag.Estado = new SelectList(est.Lista(), "Codigo", "Nome", cid.Estado);
            //libera a memória
            est = null;

            return View(cid);
        }

        public ActionResult Adicionar()
        {
            //Criamos uma instância da classe estado para carregarmos a combo de estados
            ModelEstado est = new ModelEstado();
            //passamos uma lista usando a ViewBag, para preencher a combo
            ViewBag.Estado = new SelectList(est.Lista(), "Codigo", "Nome", "Selecione");
            return View(new ModelCidade());
        }

        [HttpPost]
        public ActionResult AdicionarCidade(FormCollection collection)
        {
            try
            {
                //cria uma instância da classe Cidade
                ModelCidade cid = new ModelCidade();
                //preenche os dados
                cid.Codigo = (int.Parse(collection["Codigo"]));
                cid.Nome = collection["Nome"];
                cid.Estado = (int.Parse(collection["Estado"]));
                cid.Capital = (bool.Parse(collection["Capital"].Split(',')[0]));
                //dispara o método para adicionar no banco
                cid.adiciona();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // POST: /Cidade/Delete/5
        [HttpPost, WebMethod()]
        public ActionResult Delete(int id)
        {
            try
            {
                //criamos uma instância da classe cidade
                ModelCidade cid = new ModelCidade();
                //passamos o ID da cidade para excluir
                cid.Codigo = id;
                //exclui a cidade
                cid.Excluir();
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
