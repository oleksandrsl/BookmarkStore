using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using BookmarksStore.Models;

namespace BookmarksStore.Controllers
{
    public class CatalogController : ApiController
    {
        private CatalogContext db = new CatalogContext();

        // GET: api/CatalogModels
        public IQueryable<CatalogModel> GetCatalogModels()
        {
            return db.CatalogModels;
        }

        // GET: api/CatalogModels/5
        [ResponseType(typeof(CatalogModel))]
        public async Task<IHttpActionResult> GetCatalogModel(int id)
        {
            CatalogModel catalogModel = await db.CatalogModels.FindAsync(id);
            if (catalogModel == null)
            {
                return NotFound();
            }

            return Ok(catalogModel);
        }

        // PUT: api/CatalogModels/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCatalogModel(int id, CatalogModel catalogModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != catalogModel.Id)
            {
                return BadRequest();
            }

            db.Entry(catalogModel).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CatalogModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/CatalogModels
        [ResponseType(typeof(CatalogModel))]
        public async Task<IHttpActionResult> PostCatalogModel(CatalogModel catalogModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CatalogModels.Add(catalogModel);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = catalogModel.Id }, catalogModel);
        }

        // DELETE: api/CatalogModels/5
        [ResponseType(typeof(CatalogModel))]
        public async Task<IHttpActionResult> DeleteCatalogModel(int id)
        {
            CatalogModel catalogModel = await db.CatalogModels.FindAsync(id);
            if (catalogModel == null)
            {
                return NotFound();
            }

            db.CatalogModels.Remove(catalogModel);
            await db.SaveChangesAsync();

            return Ok(catalogModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CatalogModelExists(int id)
        {
            return db.CatalogModels.Count(e => e.Id == id) > 0;
        }
    }
}