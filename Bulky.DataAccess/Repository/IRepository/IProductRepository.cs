using Bulky.Models;

namespace Bulky.DataAccess.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        /// <summary>
        /// Updates the data.
        /// </summary>
        /// <param name="product">Type of which needs to be updated.</param>
        void Update(Product product);
    }
}
