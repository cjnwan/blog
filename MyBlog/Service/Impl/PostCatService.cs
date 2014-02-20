using Domain.Model;
using Domain.Repository;

namespace Service
{
    public class PostCatService:ServiceBase<PostCat>,IPostCatService
    {
        private readonly IRepository<PostCat> _repository;

        public PostCatService(IPostCatRepository repository)
        {
            _repository = repository;
        }
        protected override Domain.Repository.IRepository<PostCat> CurrRepository
        {
            get { return _repository; }
        }
    }
    
}