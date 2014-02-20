using Domain.Model;

namespace Domain.Repository
{
    public class CommentRepository:Repository<Comment>,ICommentRepository
    {
        public CommentRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}