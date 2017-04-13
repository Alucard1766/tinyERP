using tinyERP.Dal;

namespace tinyERP.UI.Factories
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private static readonly TinyErpContext Context = new TinyErpContext();

        public IUnitOfWork GetUnitOfWork()
        {
            return new UnitOfWork(Context);
        }
    }
}
