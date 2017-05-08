using System.Data.Entity;

namespace BookmarksStore.Models
{
    public class CatalogContext : DbContext
    {
        public CatalogContext() : base("name=DefaultConnection")
        {
        }

        public System.Data.Entity.DbSet<BookmarksStore.Models.CatalogModel> CatalogModels { get; set; }
    }
}
