using System.Collections.Generic;
using BookmarksStore.Models;

namespace BookmarksStore.Services.StorageService
{
    public interface IBookmarkStorageProvider
    {
        IEnumerable<CatalogModel> List();
        CatalogModel FindById(int id);
        IEnumerable<CatalogModel> FindByParentId(int parentId);
    }
}