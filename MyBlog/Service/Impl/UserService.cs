using Domain.Model;
using Domain.Repository;

namespace Service
{
    public class UserService:ServiceBase<User>,IUserService
    {
        private readonly IRepository<User> _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        protected override Domain.Repository.IRepository<User> CurrRepository
        {
            get { return _repository; }
        }
    }
}