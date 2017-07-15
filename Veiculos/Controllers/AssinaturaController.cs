using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using Veiculos.Models;

namespace Veiculos.Controllers
{
    /*
    A classe WebApiConfig pode requerer alterações adicionais para adicionar uma rota para esse controlador. Misture essas declarações no método Register da classe WebApiConfig conforme aplicável. Note que URLs OData diferenciam maiúsculas e minúsculas.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using Veiculos.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<AssinaturaModel>("Assinatura");
    builder.EntitySet<PessoaModel>("Pessoa"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class AssinaturaController : ODataController
    {
        private ContextoDb db = new ContextoDb();

        // GET: odata/Assinatura
        [EnableQuery]
        public IQueryable<AssinaturaModel> GetAssinatura()
        {
            return db.Assinatura;
        }

        // GET: odata/Assinatura(5)
        [EnableQuery]
        public SingleResult<AssinaturaModel> GetAssinaturaModel([FromODataUri] int key)
        {
            return SingleResult.Create(db.Assinatura.Where(assinaturaModel => assinaturaModel.id == key));
        }

        // PUT: odata/Assinatura(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<AssinaturaModel> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AssinaturaModel assinaturaModel = db.Assinatura.Find(key);
            if (assinaturaModel == null)
            {
                return NotFound();
            }

            patch.Put(assinaturaModel);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssinaturaModelExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(assinaturaModel);
        }

        // POST: odata/Assinatura
        public IHttpActionResult Post(AssinaturaModel assinaturaModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Assinatura.Add(assinaturaModel);
            db.SaveChanges();

            return Created(assinaturaModel);
        }

        // PATCH: odata/Assinatura(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<AssinaturaModel> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AssinaturaModel assinaturaModel = db.Assinatura.Find(key);
            if (assinaturaModel == null)
            {
                return NotFound();
            }

            patch.Patch(assinaturaModel);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssinaturaModelExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(assinaturaModel);
        }

        // DELETE: odata/Assinatura(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            AssinaturaModel assinaturaModel = db.Assinatura.Find(key);
            if (assinaturaModel == null)
            {
                return NotFound();
            }

            db.Assinatura.Remove(assinaturaModel);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Assinatura(5)/Pessoa
        [EnableQuery]
        public SingleResult<PessoaModel> GetPessoa([FromODataUri] int key)
        {
            return SingleResult.Create(db.Assinatura.Where(m => m.id == key).Select(m => m.Pessoa));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AssinaturaModelExists(int key)
        {
            return db.Assinatura.Count(e => e.id == key) > 0;
        }
    }
}
