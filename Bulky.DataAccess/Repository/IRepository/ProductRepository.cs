using Bulky.DataAccess.Data;
using Bulky.Models;

namespace Bulky.DataAccess.Repository.IRepository
{
    /// <summary>
    /// Responsible for CRUD of type product.
    /// </summary>
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }

        public void Update(Product product)
        {
            db.Update(product);
        }
    }
}
