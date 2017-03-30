using tinyERP.Dal;

namespace tinyERP.UI.Factories
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork GetUnitOfWork();
    }
}
