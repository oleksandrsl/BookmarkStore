﻿using System.Collections.Generic;
using BookmarksStore.Models;
using BookmarksStore.Services.StorageService;

namespace BookmarksStore.Services
{
    public class CatalogService
    {
        IBookmarkStorageProvider _storage;

        public void Init(BookmarkStorage storage)
        {
            _storage = storage;
        }

        public IEnumerable<CatalogModel> Load()
        {
            return _storage.List();
        }

        public IEnumerable<CatalogModel> LoadByParentId(int parentId)
        {
            return _storage.FindByParentId(parentId);
        }

        public CatalogModel LoadById(int catalogId)
        {
            return _storage.FindById(catalogId);
        }

        internal CatalogModel Add(CatalogModel catalogModel)
        {
            var result = _storage.Create(catalogModel);

            return result == null ? new CatalogModel() : result;
        }
        public int Delete(int id)
        {
            return _storage.Remove(id);
        }

        internal void Update(int id, CatalogModel catalogModel)
        {
            _storage.Update(catalogModel);
        }
    }
}