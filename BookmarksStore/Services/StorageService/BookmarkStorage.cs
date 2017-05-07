using System.Collections.Generic;
using System.Linq;
using BookmarksStore.Models;

namespace BookmarksStore.Services.StorageService
{
    public class BookmarkStorage : IBookmarkStorageProvider
    {
        private ApplicationUser _user;
        private CatalogContext db = new CatalogContext();

        public void Init(ApplicationUser user)
        {
            if (user == null)
                _user = new ApplicationUser();

            _user = user;
        }

        public virtual IEnumerable<CatalogModel> List()
        {
            if (db.CatalogModels != null)
                return UserCatalogs;
            return new List<CatalogModel>();
        }

        public virtual IEnumerable<CatalogModel> FindByParentId(int parentId)
        {
            if (db.CatalogModels != null)
                return UserCatalogs.Where(a => a.ParentId == parentId);
            return new List<CatalogModel>();
        }

        public virtual CatalogModel FindById(int id)
        {
            if (db.CatalogModels != null)
                return UserCatalogs.First(a => a.Id == id);
            return new CatalogModel();
        }

        private IEnumerable<CatalogModel> UserCatalogs
        {
            get
            {
                return db.CatalogModels.Where(a => a.OwnerId == _user.Id);
            }
        }
    }
}