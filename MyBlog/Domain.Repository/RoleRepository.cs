using Domain.Model;

namespace Domain.Repository
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        } 
    }
}