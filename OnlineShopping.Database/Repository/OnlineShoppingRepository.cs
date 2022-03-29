using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnlineShopping.Database.Repository
{
    public class OnlineShoppingRepository<T> : IReponsitory<T> where T : class
    {
        protected OnlineShoppingContext OnlineShoppingContext { get; set; }

        public OnlineShoppingRepository(OnlineShoppingContext onlineShoppingContext)
        {
            this.OnlineShoppingContext = onlineShoppingContext;
        }

        public IQueryable<T> FindAll()
        {
            return this.OnlineShoppingContext.Set<T>();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.OnlineShoppingContext.Set<T>().Where(expression);
        }

        public T FindOneByCondition(Expression<Func<T, bool>> expression)
        {
            return this.OnlineShoppingContext.Set<T>().Where(expression).FirstOrDefault();
        }

        public async Task<T> FindByIdAsync(long id)
        {
            return await this.OnlineShoppingContext.Set<T>().FindAsync(id);
        }

        public void Create(T entity)
        {
            this.OnlineShoppingContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            this.OnlineShoppingContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            this.OnlineShoppingContext.Set<T>().Remove(entity);
        }

        public async Task SaveAsync()
        {
            await this.OnlineShoppingContext.SaveChangesAsync();
        }
    }
}
