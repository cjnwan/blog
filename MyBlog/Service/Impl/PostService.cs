using Domain.Model;
using Domain.Repository;

namespace Service
{
    public class PostService:ServiceBase<Post>,IPostService
    {
        private readonly IRepository<Post> _repository;

        public PostService(IPostRepository repository)
        {
            _repository = repository;
        }
        protected override Domain.Repository.IRepository<Post> CurrRepository
        {
            get { return _repository; }
        }
    }
}