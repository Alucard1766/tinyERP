using tinyERP.Dal;

namespace tinyERP.UI.Factories
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        public IUnitOfWork GetUnitOfWork()
        {
            return new UnitOfWork(new TinyErpContext());
        }
    }
}
