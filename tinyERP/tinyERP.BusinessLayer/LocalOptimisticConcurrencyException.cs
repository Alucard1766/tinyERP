using System;

namespace tinyERP.BusinessLayer
{
    public class LocalOptimisticConcurrencyException<T> : Exception
    {
        public LocalOptimisticConcurrencyException(string message) : base(message) { }
        public LocalOptimisticConcurrencyException(string message, T mergedEntity) : base(message)
        {
            MergedEntity = mergedEntity;
        }

        public T MergedEntity { get; set; }
    }
}
