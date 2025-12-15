using InventoryV2.Models;

namespace InventoryV2.Interfaces;

public interface IUnitOfWork
{
    IGenericRepository<Supplier> Suppliers { get; }
    IGenericRepository<Customer> Customers { get; }
    IGenericRepository<Warehouse> Warehouses { get; }
    IGenericRepository<Product> Products { get; }
    IGenericRepository<Warehouse_Product> WarehouseProducts { get; }
    IGenericRepository<Supply_Order> SupplyOrders { get; }
    IGenericRepository<Release_Order> ReleaseOrders { get; }
    IGenericRepository<Transfer_Order> TransferOrders { get; }
    IGenericRepository<SO_Product> SOProducts { get; }
    IGenericRepository<RO_Product> ROProducts { get; }
    IGenericRepository<TO_Product> TOProducts { get; }

    Task<int> SaveChangesAsync();
}