using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookmarksStore.Models
{ 
    public class CatalogModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string OwnerId { get; set; }
        public int ParentId { get; set; }
        public string Description { get; set; }
    }

    public class CreateCatalogModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int OwnerId { get; set; }
        public int ParentId { get; set; }
        public string Description { get; set; }
    }

    public class UpdateCatalogModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int OwnerID { get; set; }
        public string Description { get; set; }
    }

    public class DeleteCatalogModel
    {
        public int Id { get; set; }
        public int OwnerID { get; set; }
    }
}