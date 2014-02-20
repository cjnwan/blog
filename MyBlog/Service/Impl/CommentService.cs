using Domain.Model;
using Domain.Repository;

namespace Service
{
    public class CommentService:ServiceBase<Comment>,ICommentService
    {
        private readonly IRepository<Comment> _repository;

        public CommentService(ICommentRepository repository)
        {
            _repository = repository;
        }
        protected override Domain.Repository.IRepository<Comment> CurrRepository
        {
            get { return _repository; }
        }
    }
}