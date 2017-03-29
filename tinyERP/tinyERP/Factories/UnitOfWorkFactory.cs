using tinyERP.Dal;

namespace tinyERP.Factories
{
    public static class UnitOfWorkFactory
    {
        public static IUnitOfWork GetUnitOfWork()
        {
            return new UnitOfWork(new TinyErpContext());
        }
    }
}
