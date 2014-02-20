
using Domain.Model;

namespace Domain.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}