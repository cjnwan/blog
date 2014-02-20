using Domain.Model;

namespace Domain.Repository
{
    public class PostCatRepository : Repository<PostCat>, IPostCatRepository
    {
        public PostCatRepository()
        {
            UnitOfWork = new BlogUnitofContext(); //UnitOfWork;
        }
    }
}