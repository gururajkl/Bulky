using System.Linq.Expressions;

namespace Bulky.DataAccess.Repository.IRepository
{
    /// <summary>
    /// Base interface for all the repos.
    /// </summary>
    /// <typeparam name="T">Any class.</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Gives the entire data.
        /// </summary>
        /// <returns>IEnumerable of any type.</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Gives the specified data.
        /// </summary>
        /// <param name="predicate">Lambda expression for filtering.</param>
        /// <returns></returns>
        T Get(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Adds the data to the dbContext.
        /// </summary>
        /// <param name="entity">Single data of particular type.</param>
        void Add(T entity);

        /// <summary>
        /// Removes the data to the dbContext.
        /// </summary>
        /// <param name="entity">Single data of particular type.</param>
        void Remove(T entity);

        /// <summary>
        /// Removes the entire collection of data.
        /// </summary>
        /// <param name="entities">Collection of data.</param>
        void RemoveRange(IEnumerable<T> entities);
    }
}
