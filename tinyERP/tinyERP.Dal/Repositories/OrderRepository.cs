using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using tinyERP.Dal.Entities;

namespace tinyERP.Dal.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private TinyErpContext TinyErpContext => Context as TinyErpContext;

        public OrderRepository(TinyErpContext context) : base(context)
        {
        }

        public IEnumerable<Order> GetOrdersWithCustomers()
        {
            return TinyErpContext.Orders.Include(o => o.Customer).ToList();
        }

        public IEnumerable<Offer> GetOffersAndDocumentsByOrderId(int orderId)
        {
            var offers = TinyErpContext.Offers.Where(o => o.OrderId == orderId).ToList();
            foreach (var offer in offers)
            {
                offer.Document = TinyErpContext.Documents.FirstOrDefault(d => d.Id == offer.DocumentId);
            }
            return offers;
        }

        public IEnumerable<Invoice> GetInvoicesAndDocumentsByOrderId(int orderId)
        {
            var invoices = TinyErpContext.Invoices.Where(o => o.OrderId == orderId).ToList();
            foreach (var invoice in invoices)
            {
                invoice.Document = TinyErpContext.Documents.FirstOrDefault(d => d.Id == invoice.DocumentId);
            }
            return invoices;
        }

        public IEnumerable<OrderConfirmation> GetOrderConfirmationWithDocumentByOrderId(int orderId)
        {
            var orderConfirmations = TinyErpContext.OrderConfirmations.Where(o => o.OrderId == orderId).ToList();
            foreach (var oc in orderConfirmations)
            {
                oc.Document =
                    TinyErpContext.Documents.FirstOrDefault(d => d.Id == oc.DocumentId);
            }

            return orderConfirmations;

        }
    }
}
