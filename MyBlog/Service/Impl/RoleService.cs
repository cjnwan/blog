using Domain.Model;
using Domain.Repository;

namespace Service
{
    public class RoleService:ServiceBase<Role>,IRoleService
    {
        private readonly IRepository<Role> _repository;

        public RoleService(IRoleRepository repository)
        {
            _repository = repository;
        }

        protected override Domain.Repository.IRepository<Role> CurrRepository
        {
            get { return _repository; }
        }
    }
}