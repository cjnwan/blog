using Domain.Model;

namespace Domain.Repository
{
    public class ImageRepository : Repository<Image>, IImageRepository
    {
        public ImageRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }  
    }
}