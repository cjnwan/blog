using Domain.Model;

namespace Domain.Repository
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        public TagRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}