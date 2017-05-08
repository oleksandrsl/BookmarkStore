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
}