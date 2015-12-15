using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Plugin.Misc.VendorMembership.Domain;
using Nop.Core;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Payments;
using Nop.Core.Domain.Shipping;
using Nop.Core.Data;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Customers;
using Nop.Services.Events;

namespace Nop.Plugin.Misc.VendorMembership.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<OrderItem> _orderItemRepository;
        private readonly IRepository<OrderNote> _orderNoteRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<RecurringPayment> _recurringPaymentRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<ReturnRequest> _returnRequestRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<Invoice> _invoiceRepository;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="orderRepository">Order repository</param>
        /// <param name="orderItemRepository">Order item repository</param>
        /// <param name="orderNoteRepository">Order note repository</param>
        /// <param name="productRepository">Product repository</param>
        /// <param name="recurringPaymentRepository">Recurring payment repository</param>
        /// <param name="customerRepository">Customer repository</param>
        /// <param name="returnRequestRepository">Return request repository</param>
        /// <param name="eventPublisher">Event published</param>
        public InvoiceService(IRepository<Order> orderRepository,
            IRepository<OrderItem> orderItemRepository,
            IRepository<OrderNote> orderNoteRepository,
            IRepository<Product> productRepository,
            IRepository<RecurringPayment> recurringPaymentRepository,
            IRepository<Customer> customerRepository,
            IRepository<ReturnRequest> returnRequestRepository,
            IEventPublisher eventPublisher,
            IRepository<Invoice> invoiceRepository)
        {
            this._orderRepository = orderRepository;
            this._orderItemRepository = orderItemRepository;
            this._orderNoteRepository = orderNoteRepository;
            this._productRepository = productRepository;
            this._recurringPaymentRepository = recurringPaymentRepository;
            this._customerRepository = customerRepository;
            this._returnRequestRepository = returnRequestRepository;
            this._eventPublisher = eventPublisher;
            this._invoiceRepository = invoiceRepository;
        }
        
        public void DeleteInvoice(Invoice invoice)
        {
            throw new NotImplementedException();
        }

        public IList<Invoice> GetAllInvoices()
        {
            throw new NotImplementedException();
        }

        public Invoice GetInvoiceById(int invoiceId)
        {
            return _invoiceRepository.GetById(invoiceId);
        }

        public IList<Invoice> GetInvoicesByVendorId()
        {
            throw new NotImplementedException();
        }

        public Invoice GetInvoicesByOrderId(int orderId)
        {
            return _invoiceRepository.Table.SingleOrDefault(inv => inv.OrderId == orderId);
        }

        public void InsertInvoice(Invoice invoice)
        {
            if(invoice == null)
                throw new ArgumentNullException("invoice");

            _invoiceRepository.Insert(invoice);

            _eventPublisher.EntityInserted(invoice);
        }

        public void UpdateInvoice(Invoice invoice)
        {
            if (invoice == null)
                throw new ArgumentNullException("invoice");

            _invoiceRepository.Update(invoice);

            _eventPublisher.EntityUpdated(invoice);
        }

        public virtual IPagedList<Invoice> SearchInvoices(int storeId = 0,
            int vendorId = 0, int customerId = 0,
            int productId = 0, int affiliateId = 0, int warehouseId = 0,
            int billingCountryId = 0, string paymentMethodSystemName = null,
            DateTime? createdFromUtc = null, DateTime? createdToUtc = null,
            OrderStatus? os = null, PaymentStatus? ps = null, ShippingStatus? ss = null,
            string billingEmail = null, string orderNotes = null, string orderGuid = null,
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            int? orderStatusId = null;
            if (os.HasValue)
                orderStatusId = (int)os.Value;

            int? paymentStatusId = null;
            if (ps.HasValue)
                paymentStatusId = (int)ps.Value;

            int? shippingStatusId = null;
            if (ss.HasValue)
                shippingStatusId = (int)ss.Value;

            var query = _invoiceRepository.Table;
            if (storeId > 0)
                query = query.Where(o => o.StoreId == storeId);
            if (vendorId > 0)
            {
                query = query.Where(inv => inv.VendorId == vendorId);
            }
            //if (customerId > 0)
            //    query = query.Where(o => o.CustomerId == customerId);
            //if (productId > 0)
            //{
            //    query = query
            //        .Where(o => o.OrderItems
            //        .Any(orderItem => orderItem.Product.Id == productId));
            //}
            //if (warehouseId > 0)
            //{
            //    var manageStockInventoryMethodId = (int)ManageInventoryMethod.ManageStock;
            //    query = query
            //        .Where(o => o.OrderItems
            //        .Any(orderItem =>
            //            //"Use multiple warehouses" enabled
            //            //we search in each warehouse
            //            (orderItem.Product.ManageInventoryMethodId == manageStockInventoryMethodId &&
            //            orderItem.Product.UseMultipleWarehouses &&
            //            orderItem.Product.ProductWarehouseInventory.Any(pwi => pwi.WarehouseId == warehouseId))
            //            ||
            //            //"Use multiple warehouses" disabled
            //            //we use standard "warehouse" property
            //            ((orderItem.Product.ManageInventoryMethodId != manageStockInventoryMethodId ||
            //            !orderItem.Product.UseMultipleWarehouses) &&
            //            orderItem.Product.WarehouseId == warehouseId))
            //            );
            //}
            //if (billingCountryId > 0)
            //    query = query.Where(o => o.BillingAddress != null && o.BillingAddress.CountryId == billingCountryId);
            if (!String.IsNullOrEmpty(paymentMethodSystemName))
                query = query.Where(o => o.PaymentMethodSystemName == paymentMethodSystemName);
            //if (affiliateId > 0)
            //    query = query.Where(o => o.AffiliateId == affiliateId);
            //if (createdFromUtc.HasValue)
            //    query = query.Where(o => createdFromUtc.Value <= o.CreatedOnUtc);
            //if (createdToUtc.HasValue)
            //    query = query.Where(o => createdToUtc.Value >= o.CreatedOnUtc);
            //if (orderStatusId.HasValue)
            //    query = query.Where(o => orderStatusId.Value == o.OrderStatusId);
            //if (paymentStatusId.HasValue)
            //    query = query.Where(o => paymentStatusId.Value == o.PaymentStatusId);
            //if (shippingStatusId.HasValue)
            //    query = query.Where(o => shippingStatusId.Value == o.ShippingStatusId);
            //if (!String.IsNullOrEmpty(billingEmail))
            //    query = query.Where(o => o.BillingAddress != null && !String.IsNullOrEmpty(o.BillingAddress.Email) && o.BillingAddress.Email.Contains(billingEmail));
            //if (!String.IsNullOrEmpty(orderNotes))
            //    query = query.Where(o => o.OrderNotes.Any(on => on.Note.Contains(orderNotes)));
            //query = query.Where(o => !o.Deleted);
            query = query.OrderByDescending(o => o.CreatedOnUtc);



            //if (!String.IsNullOrEmpty(orderGuid))
            //{
            //    //filter by GUID. Filter in BLL because EF doesn't support casting of GUID to string
            //    var orders = query.ToList();
            //    orders = orders.FindAll(o => o.OrderGuid.ToString().ToLowerInvariant().Contains(orderGuid.ToLowerInvariant()));
            //    return new PagedList<Order>(orders, pageIndex, pageSize);
            //}

            //database layer paging
            return new PagedList<Invoice>(query, pageIndex, pageSize);
        }
    }
}
