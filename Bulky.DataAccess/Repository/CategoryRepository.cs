using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;

namespace Bulky.DataAccess.Repository
{
    /// <summary>
    /// Responsible for CRUD of type category.
    /// </summary>
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext db;

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Category category)
        {
            db.Update(category);
        }
    }
}
