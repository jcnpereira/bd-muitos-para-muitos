using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using bd_muitos_para_muitos.Models;

namespace bd_muitos_para_muitos.Controllers {
   public class AController : Controller {
      private BaseDados db = new BaseDados();

      // GET: A
      public ActionResult Index() {
         return View(db.A.ToList());
      }

      // GET: A/Details/5
      public ActionResult Details(int? id) {
         if(id == null) {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         A a = db.A.Find(id);
         if(a == null) {
            return HttpNotFound();
         }

         // gerar a lista de objetos de B que podem ser associados a A
         ViewBag.ListaObjetosDeB = db.B.OrderBy(b => b.NomeB).ToList();

         return View(a);
      }

      // GET: A/Create
      public ActionResult Create() {

         // gerar a lista de objetos de B que podem ser associados a A
         ViewBag.ListaObjetosDeB = db.B.OrderBy(b => b.NomeB).ToList();

         return View();
      }

      // POST: A/Create
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Create([Bind(Include = "ID,NomeA1,NomeA2")] A a, string[] opcoesEscolhidasDeB) {

         /// avalia se o array com a lista das escolhas de objetos de B associados ao objeto do tipo A 
         /// é nula, ou não.
         /// Só poderá avanção se NÃO for nula
         if(opcoesEscolhidasDeB==null) {
            ModelState.AddModelError("", "Necessita escolher pelo menos um valor de B para associar ao seu objeto de A.");
            // gerar a lista de objetos de B que podem ser associados a A
            ViewBag.ListaObjetosDeB = db.B.OrderBy(b => b.NomeB).ToList();
            // devolver controlo à View
            return View(a);
         }

         // criar uma lista com os objetos escolhidos de B
         List<B> listaDeObjetosDeBEscolhidos = new List<B>();
         foreach(string item in opcoesEscolhidasDeB) {
            //procurar o objeto de B
            B b = db.B.Find(Convert.ToInt32(item));
            // adicioná-lo à lista
            listaDeObjetosDeBEscolhidos.Add(b);
         }

         // adicionar a lista ao objeto de A
         a.ListaDeObjetosDeB = listaDeObjetosDeBEscolhidos;


         if(ModelState.IsValid) {
            db.A.Add(a);
            db.SaveChanges();
            return RedirectToAction("Index");
         }

         return View(a);
      }

      // GET: A/Edit/5
      public ActionResult Edit(int? id) {
         if(id == null) {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }

         A a = db.A.Find(id);
         if(a == null) {
            return HttpNotFound();
         }

         // gerar a lista de objetos de B que podem ser associados a A
         ViewBag.ListaObjetosDeB = db.B.OrderBy(b => b.NomeB).ToList();


         return View(a);
      }


      // POST: A/Edit/5
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Edit([Bind(Include = "ID,NomeA1,NomeA2")] A a, string[] opcoesEscolhidasDeB) {
         // https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/updating-related-data-with-the-entity-framework-in-an-asp-net-mvc-application

         // ler da BD o objeto que se pretende editar
         var aa = db.A.Include(b => b.ListaDeObjetosDeB).Where(b => b.ID == a.ID).SingleOrDefault();

         // avaliar se os dados são 'bons'
         if(ModelState.IsValid) {
            aa.NomeA1 = a.NomeA1;
            aa.NomeA2 = a.NomeA2;
         }
         else {
            // gerar a lista de objetos de B que podem ser associados a A
            ViewBag.ListaObjetosDeB = db.B.OrderBy(b => b.NomeB).ToList();
            // devolver o controlo à View
            return View(a);
         }

         // tentar fazer o UPDATE
         if(TryUpdateModel(aa, "", new string[] { nameof(aa.NomeA1), nameof(aa.NomeA2), nameof(aa.ListaDeObjetosDeB) })) {

            // obter a lista de elementos de B
            var elementosDeB = db.B.ToList();

            if(opcoesEscolhidasDeB != null) {
               // se existirem opções escolhidas, vamos associá-las
               foreach(var bb in elementosDeB) {
                  if(opcoesEscolhidasDeB.Contains(bb.ID.ToString())) {
                     // se uma opção escolhida ainda não está associada, cria-se a associação
                     if(!aa.ListaDeObjetosDeB.Contains(bb)) {
                        aa.ListaDeObjetosDeB.Add(bb);
                     }
                  }
                  else {
                     // caso exista associação para uma opção que não foi escolhida, 
                     // remove-se essa associação
                     aa.ListaDeObjetosDeB.Remove(bb);
                  }
               }
            }
            else {
               // não existem opções escolhidas!
               // vamos eliminar todas as associações
               foreach(var bb in elementosDeB) {
                  if(aa.ListaDeObjetosDeB.Contains(bb)) {
                     aa.ListaDeObjetosDeB.Remove(bb);
                  }
               }
            }

            // guardar as alterações
            db.SaveChanges();

            // devolver controlo à View
            return RedirectToAction("Index");
         }

         // se cheguei aqui, é pq alguma coisa correu mal
         ModelState.AddModelError("", "Alguma coisa correu mal...");

         // gerar a lista de objetos de B que podem ser associados a A
         ViewBag.ListaObjetosDeB = db.B.OrderBy(b => b.NomeB).ToList();

         // visualizar View...
         return View(a);
      }

      // GET: A/Delete/5
      public ActionResult Delete(int? id) {
         if(id == null) {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         A a = db.A.Find(id);
         if(a == null) {
            return HttpNotFound();
         }
         return View(a);
      }

      // POST: A/Delete/5
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public ActionResult DeleteConfirmed(int id) {
         A a = db.A.Find(id);
         db.A.Remove(a);
         db.SaveChanges();
         return RedirectToAction("Index");
      }

      protected override void Dispose(bool disposing) {
         if(disposing) {
            db.Dispose();
         }
         base.Dispose(disposing);
      }
   }
}
