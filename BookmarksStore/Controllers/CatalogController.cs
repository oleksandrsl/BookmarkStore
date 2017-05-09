using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using BookmarksStore.Models;
using BookmarksStore.Services;
using BookmarksStore.Services.StorageService;

namespace BookmarksStore.Controllers
{
    [Authorize]
    public class CatalogController : ApiController
    {
        private CatalogService _catalogService = new CatalogService();

        public CatalogController()
        {
            BookmarkStorage store = new BookmarkStorage();

            store.Init(new ApplicationUser()
            {
                Id = User.Identity.GetUserId()
            });

            _catalogService.Init(store);
        }

        public IEnumerable<CatalogModel> GetAllCatalogs()
        {
            return _catalogService.Load();
        }

        [ResponseType(typeof(CatalogModel))]
        public async Task<IHttpActionResult> GetCatalogById(int id)
        {
            var catalog = _catalogService.LoadById(id);

            if (catalog == null)
            {
                return NotFound();
            }

            return Ok(catalog);
        }

        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> UpdateCatalog(int id, CatalogModel catalogModel)
        {
            if (!ModelState.IsValid || id != catalogModel.Id)
                return BadRequest(ModelState);

            try
            {
                _catalogService.Update(id, catalogModel);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return StatusCode(HttpStatusCode.OK);
        }

        [ResponseType(typeof(CatalogModel))]
        public async Task<IHttpActionResult> CreateCatalog(CatalogModel catalogModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var catalog = _catalogService.Add(catalogModel);

            return CreatedAtRoute("default", new { id = catalog.Id }, catalog);
        }

        [ResponseType(typeof(CatalogModel))]
        public async Task<IHttpActionResult> DeleteCatalog(int id)
        {
            var status = _catalogService.Delete(id);

            return Ok(status);
        }
    }
}
