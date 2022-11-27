﻿using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NTC_Lego.Client.Pages.AdminPortal;
using NTC_Lego.Shared;

using Inventory = NTC_Lego.Shared.Inventory;

namespace NTC_Lego.Server.Services
{
    public class PurchaseService
    {
        private readonly DataContext _dataContext;

        public PurchaseService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // Get methods below
        public IEnumerable<PurchaseOrderVM> GetPurchaseOrders(int skip, int take)
        {
            return _dataContext.PurchaseOrder
                .Skip(skip)
                .Take(take)
                .Select(x => new PurchaseOrderVM
                {
                    PurchaseOrderId = x.PurchaseOrderId,
                    PurchaseOrderDate = x.PurchaseOrderDate,
                    OrderStatus = x.OrderStatus,
                    SupplierId = x.SupplierId,
                    Supplier = new SupplierVM
                    {
                        SupplierId = x.Supplier.SupplierId,
                        SupplierName = x.Supplier.SupplierName,
                        SupplierEmail = x.Supplier.SupplierEmail,
                    },
                    PurchaseOrderDetails = (ICollection<PurchaseOrderDetailVM>)x.PurchaseOrderDetails.Select(y => new PurchaseOrderDetailVM
                    {
                        PurchaseOrderDetailId = y.PurchaseOrderDetailId,
                        PurchaseOrderDetailQuantity = y.PurchaseOrderDetailQuantity,
                        PurchaseOrderId = y.PurchaseOrderId,
                        InventoryId = y.InventoryId,
                        Inventory = new InventoryVM
                        {
                            InventoryId = y.Inventory.InventoryId,
                            InventoryItemPrice = y.Inventory.InventoryItemPrice,
                            InventoryLocations = (ICollection<InventoryLocationVM>)y.Inventory.InventoryLocations.Select(y => new InventoryLocationVM
                            {
                                InventoryId = y.InventoryId,
                                ItemQuantity = y.ItemQuantity,
                                LocationId = y.LocationId,
                            })
                        }
                    })
                })
                .ToList();
        }

        public PurchaseOrderVM GetPurchaseOrderVM(int id)
        {
            return _dataContext.PurchaseOrder
                .Select(x => new PurchaseOrderVM
                {
                    PurchaseOrderId = x.PurchaseOrderId,
                    PurchaseOrderDate = x.PurchaseOrderDate,
                    OrderStatus = x.OrderStatus,
                    SupplierId = x.SupplierId,
                    Supplier = new SupplierVM
                    {
                        SupplierId = x.Supplier.SupplierId,
                        SupplierName = x.Supplier.SupplierName,
                        SupplierEmail = x.Supplier.SupplierEmail,
                    },
                    PurchaseOrderDetails = (ICollection<PurchaseOrderDetailVM>)x.PurchaseOrderDetails.Select(y => new PurchaseOrderDetailVM
                    {
                        PurchaseOrderDetailId = y.PurchaseOrderDetailId,
                        PurchaseOrderDetailQuantity = y.PurchaseOrderDetailQuantity,
                        PurchaseOrderId = y.PurchaseOrderId,
                        InventoryId = y.InventoryId,
                        Inventory = new InventoryVM
                        {
                            InventoryId = y.Inventory.InventoryId,
                            InventoryItemPrice = y.Inventory.InventoryItemPrice,
                            InventoryLocations = (ICollection<InventoryLocationVM>)y.Inventory.InventoryLocations.Select(y => new InventoryLocationVM
                            {
                                InventoryId = y.InventoryId,
                                ItemQuantity = y.ItemQuantity,
                                LocationId = y.LocationId,
                            })
                        }
                    })
                })
                .FirstOrDefault(x => x.PurchaseOrderId == id)!;
        }

        public PurchaseOrder GetPurchaseOrder(int id)
        {
            return _dataContext.PurchaseOrder.Include(x=>x.PurchaseOrderDetails).ThenInclude(y=>y.Inventory).FirstOrDefault(x => x.PurchaseOrderId == id)!;
        }

        public Inventory GetInventory(int id)
        {
            return _dataContext.Inventory.Include(x=>x.InventoryLocations).FirstOrDefault(x => x.InventoryId == id)!;
        }

        public IEnumerable<InventoryLocation> GetInventoryLocations(int id)
        {
            return _dataContext.InventoryLocation.Where(x => x.InventoryId == id).OrderByDescending(x=>x.ItemQuantity).ToList();
        }

        // Transaction methods below
        public PurchaseOrder AddPurchaseOrder(PurchaseOrder purchaseOrder)
        {
            _dataContext.PurchaseOrder.Add(purchaseOrder);
            _dataContext.SaveChanges();
            return purchaseOrder;
        }

        public void UpdatePurchaseOrder(PurchaseOrder old, PurchaseOrder update)
        {
            _dataContext.Entry(old).CurrentValues.SetValues(update);
            _dataContext.SaveChanges();
        }

        public PurchaseOrderDetail AddPurchaseOrderDetail(PurchaseOrderDetail purchaseOrderDetail)
        {
            _dataContext.PurchaseOrderDetail.Add(purchaseOrderDetail);
            _dataContext.SaveChanges();
            return purchaseOrderDetail;
        }

        public void UpdateInventoryLocation(InventoryLocation old, InventoryLocation update)
        {
            _dataContext.Entry(old).CurrentValues.SetValues(update);
            _dataContext.SaveChanges();
        }

        public InventoryLocation DeleteInventoryLocation(InventoryLocation inventoryLocation)
        {
            _dataContext.InventoryLocation.Remove(inventoryLocation);
            _dataContext.SaveChanges();
            return inventoryLocation;
        }
    }
}
