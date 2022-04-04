using System;

namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        IProductGroupRepository ProductGroups { get; }
        IAgreementRepository Agreements{ get; }

        int Complete();
    }
}
