using Domain.Model;

namespace Domain.Repository
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}