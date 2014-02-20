using Domain.Model;
using Domain.Repository;

namespace Service
{
    public class TagService:ServiceBase<Tag>,ITagService
    {
        private readonly IRepository<Tag> _repository;

        public TagService(ITagRepository repository)
        {
            _repository = repository;
        }
        protected override Domain.Repository.IRepository<Tag> CurrRepository
        {
            get { return _repository; }
        }
    }
}