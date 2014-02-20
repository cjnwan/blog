using Domain.Model;
using Domain.Repository;

namespace Service
{
    public class ImageService:ServiceBase<Image>,IImageService
    {
        private readonly IRepository<Image> _repository; 

        protected override IRepository<Image> CurrRepository
        {
            get { return _repository; }
           
        }

        public ImageService(IImageRepository repository)
        {
            _repository = repository;
        }
    }
}