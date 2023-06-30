using Bulky.Models;

namespace Bulky.DataAccess.Repository.IRepository
{
    /// <summary>
    /// Implements IRepository<T> where T is category.
    /// </summary>
    public interface ICategoryRepository : IRepository<Category>
    {
        /// <summary>
        /// Updates the data.
        /// </summary>
        /// <param name="category">Type of which needs to be updated.</param>
        void Update(Category category);
    }
}
