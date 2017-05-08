using System.Collections.Generic;
using System.Linq;
using BookmarksStore.Models;
using System.Web;
using System;
using System.Web.Http.ModelBinding;
using System.Data.Entity;

namespace BookmarksStore.Services.StorageService
{
    public class BookmarkStorage : IBookmarkStorageProvider
    {
        private ApplicationUser _user;
        private CatalogContext db = new CatalogContext();
        private CatalogModel _catalogModel = new CatalogModel();
        public void Init(ApplicationUser user)
        {
            if (user == null)
                _user = new ApplicationUser();

            _user = user;
        }
 

        public virtual IEnumerable<CatalogModel> List()
        {
            return UserCatalogs();
        }

        public virtual IEnumerable<CatalogModel> FindByParentId(int parentId)
        {
            try
            {
                return UserCatalogs().Where(a => a.ParentId == parentId);
            }
            catch (Exception ex)
            {
                //TODO: Unexpected exception
                return new List<CatalogModel>();
            }
        }

        public virtual CatalogModel FindById(int id)
        {
            try
            {

                return UserCatalogs().First(a => a.Id == id);
            }
            catch (Exception ex)
            {
                //TODO: Unexpected exception
                return new CatalogModel();
            }
        }

        private IEnumerable<CatalogModel> UserCatalogs()
        {
            try
            {
                return db.CatalogModels.Where(a => a.OwnerId == _user.Id);
            }
            catch (Exception ex)
            {
                //TODO: Unexpected exception
                return new List<CatalogModel>();
            }
        }

        public CatalogModel Create(CatalogModel catalogModel)
        {
            try
            {
                db.Entry(catalogModel).State = EntityState.Added;
                var result = db.SaveChanges();
                return db.CatalogModels.First(a => a.Id == catalogModel.Id);
            }   
            catch (Exception ex)
            {
                return new CatalogModel();
            }
        }

        public int Remove(int id)
        {
            try
            {
                var catalog = db.CatalogModels.First(c => c.Id == id && c.OwnerId == _user.Id);

                db.CatalogModels.Remove(catalog);
                return db.SaveChanges();
            
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public void Update(CatalogModel catalogModel)
        {
            try
            {
                db.Entry(catalogModel).State = EntityState.Modified;
                var result = db.SaveChanges();
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}