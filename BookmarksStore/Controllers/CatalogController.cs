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
using BookmarksStore.Services;
using BookmarksStore.Services.StorageService;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Threading;
using Microsoft.Owin.Host.SystemWeb;
using Microsoft.AspNet.Identity.Owin;

namespace BookmarksStore.Controllers
{
    [Authorize]
    public class CatalogController : ApiController
    {
        CatalogContext db = new CatalogContext();
        private CatalogService _catalogService = new CatalogService();
        // GET: api/CatalogModels
        public CatalogController()
        {
            BookmarkStorage store = new BookmarkStorage();
            store.Init(new ApplicationUser() { Id = User.Identity.GetUserId() });
            _catalogService.Init(store);
        }

        public IEnumerable<CatalogModel> GetCatalogModel()
        {
            return _catalogService.Load();
        }

        // GET: api/CatalogModels/5
        [ResponseType(typeof(CatalogModel))]
        public async Task<IHttpActionResult> GetCatalogModel(int id)
        {
            var catalog = _catalogService.LoadById(id);
            
            if (catalog == null)
            {
                return NotFound();
            }

            return Ok(catalog);
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

            try
            {
                _catalogService.Update(id, catalogModel);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
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

            CatalogModel catalog = _catalogService.Add(catalogModel);

            return CreatedAtRoute("DefaultApi", new { id = catalog.Id }, catalog);
        }

        // DELETE: api/CatalogModels/5
        [ResponseType(typeof(CatalogModel))]
        public async Task<IHttpActionResult> DeleteCatalogModel(int id)
        {
          
            var res = _catalogService.Delete(id);

            return Ok(res);
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