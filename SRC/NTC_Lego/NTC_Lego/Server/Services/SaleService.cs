using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NTC_Lego.Client.Pages.AdminPortal;
using NTC_Lego.Shared;

using Inventory = NTC_Lego.Shared.Inventory;

namespace NTC_Lego.Server.Services
{
    public class SaleService
    {
        private readonly DataContext _dataContext;

        public SaleService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<SaleOrderVM> GetSaleOrderRecent()
        {
            return _dataContext.SaleOrder
                .Take(3)
                .OrderByDescending(x => x.SaleOrderId)
                .Select(x => new SaleOrderVM
                {
                    SaleOrderId = x.SaleOrderId,
                    SaleOrderDate = x.SaleOrderDate,
                    OrderStatus = x.OrderStatus,
                    UserId = x.UserId,
                    User = new UserVM
                    {
                        UserId = x.User.UserId,
                        UserName = x.User.UserName,
                        UserEmail = x.User.UserEmail,
                    },
                    SaleOrderDetails = (ICollection<SaleOrderDetailVM>)x.SaleOrderDetails.Select(y => new SaleOrderDetailVM
                    {
                        SaleOrderDetailId = y.SaleOrderDetailId,
                        SaleOrderDetailQuantity = y.SaleOrderDetailQuantity,
                        SaleOrderId = y.SaleOrderId,
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

        public IEnumerable<SaleOrderVM> GetSaleOrders(int skip, int take)
        {
            return _dataContext.SaleOrder
                .Take(3)
                .OrderByDescending(x => x.SaleOrderId)
                .Select(x => new SaleOrderVM
                {
                    SaleOrderId = x.SaleOrderId,
                    SaleOrderDate = x.SaleOrderDate,
                    OrderStatus = x.OrderStatus,
                    UserId = x.UserId,
                    User = new UserVM
                    {
                        UserId = x.User.UserId,
                        UserName = x.User.UserName,
                        UserEmail = x.User.UserEmail,
                    },
                    SaleOrderDetails = (ICollection<SaleOrderDetailVM>)x.SaleOrderDetails.Select(y => new SaleOrderDetailVM
                    {
                        SaleOrderDetailId = y.SaleOrderDetailId,
                        SaleOrderDetailQuantity = y.SaleOrderDetailQuantity,
                        SaleOrderId = y.SaleOrderId,
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

        public SaleOrderVM GetSaleOrderVM(int id)
        {
            return _dataContext.SaleOrder
                .Select(x => new SaleOrderVM
                {
                    SaleOrderId = x.SaleOrderId,
                    SaleOrderDate = x.SaleOrderDate,
                    OrderStatus = x.OrderStatus,
                    UserId = x.UserId,
                    User = new UserVM
                    {
                        UserId = x.User.UserId,
                        UserName = x.User.UserName,
                        UserEmail = x.User.UserEmail,
                    },
                    SaleOrderDetails = (ICollection<SaleOrderDetailVM>)x.SaleOrderDetails.Select(y => new SaleOrderDetailVM
                    {
                        SaleOrderDetailId = y.SaleOrderDetailId,
                        SaleOrderDetailQuantity = y.SaleOrderDetailQuantity,
                        SaleOrderId = y.SaleOrderId,
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
                .FirstOrDefault(x => x.SaleOrderId == id)!;
        }


        public IEnumerable<UserVM> GetUser()
        {
            return _dataContext.User
                .Select(x => new UserVM
                {
                    UserId = x.UserId,
                    UserName = x.UserName,
                    UserEmail = x.UserEmail
                })
                .ToList();
        }

        public SaleOrder GetSaleOrder(int id)
        {
            return _dataContext.SaleOrder.Include(x => x.SaleOrderDetails).ThenInclude(y => y.Inventory).FirstOrDefault(x => x.SaleOrderId == id)!;
        }

        public Inventory GetInventory(int id)
        {
            return _dataContext.Inventory.Include(x => x.InventoryLocations).FirstOrDefault(x => x.InventoryId == id)!;
        }

        public IEnumerable<InventoryLocation> GetInventoryLocations(int id)
        {
            return _dataContext.InventoryLocation.Where(x => x.InventoryId == id).OrderByDescending(x => x.ItemQuantity).ToList();
        }

        public Inventory GetInventory(string itemId, int colorId)
        {
            return _dataContext.Inventory.FirstOrDefault(x => x.ItemId == itemId && x.ColorId == colorId)!;
        }

        public InventoryLocation GetInventoryLocation(int inventoryId, int locationId)
        {
            return _dataContext.InventoryLocation.FirstOrDefault(x => x.InventoryId == inventoryId && x.LocationId == locationId)!;
        }

        // Transaction Methods Below
        public SaleOrder AddSaleOrder(SaleOrder saleOrder)
        {
            _dataContext.SaleOrder.Add(saleOrder);
            _dataContext.SaveChanges();
            return saleOrder;
        }

        public void UpdateSaleOrder(SaleOrder old, SaleOrder update)
        {
            _dataContext.Entry(old).CurrentValues.SetValues(update);
            _dataContext.SaveChanges();
        }

        public SaleOrderDetail AddSaleOrderDetail(SaleOrderDetail saleOrderDetail)
        {
            _dataContext.SaleOrderDetail.Add(saleOrderDetail);
            _dataContext.SaveChanges();
            return saleOrderDetail;
        }

        public InventoryLocation AddInventoryLocation(InventoryLocation inventoryLocation)
        {
            _dataContext.InventoryLocation.Add(inventoryLocation);
            _dataContext.SaveChanges();
            return inventoryLocation;
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

