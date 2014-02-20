
using Domain.Model;
using Domain.Repository;

namespace Service
{

    public class CategoryService : ServiceBase<Category>,ICategoryService
    {
        private readonly IRepository<Category> _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        protected override IRepository<Category> CurrRepository
        {
            get { return _repository; }
           
        } 
    }
}