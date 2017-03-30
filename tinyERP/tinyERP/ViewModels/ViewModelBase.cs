using System;
using System.Runtime.InteropServices;
using tinyERP.Dal;
using tinyERP.UI.Factories;

namespace tinyERP.UI.ViewModels
{
    public abstract class ViewModelBase
    {
        private readonly UnitOfWorkFactory _factory;
        protected IUnitOfWork UnitOfWork { get; set; }

        protected ViewModelBase(UnitOfWorkFactory factory)
        {
            this._factory = factory;
        }

        ~ViewModelBase()
        {
            UnitOfWork?.Dispose();
        }

        public void Init()
        {
            UnitOfWork = _factory.GetUnitOfWork();
            Load();
        }
        public abstract void Load();
    }
}
