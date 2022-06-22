using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace multiview.ViewModel
{
    public class OrderDetailsViewModel
    {
        private ObservableCollection<OrderData> _orderDetails;
        public ObservableCollection<OrderData> OrderDetails
        {
            get { return _orderDetails; }
            set { _orderDetails = value; }
        }

        private Guid _orderId;

        public OrderDetailsViewModel()
        {
            _orderDetails=new ObservableCollection<OrderData>();
            GetData();
        }
        private void GetData()
        {
            WeakReferenceMessenger.Default.Register<Order>(this, UpdateOrderId);
            var orderData1 = new OrderData { ProductCode = "ABC", ProductName = "CDE", Quantity = 10, TotalPrice = 5, UnitPrice = 0.5M };
            var orderData2 = new OrderData { ProductCode = "ABC", ProductName = "CDE", Quantity = 10, TotalPrice = 5, UnitPrice = 0.5M };
            OrderDetails.Add(orderData1);
            OrderDetails.Add(orderData2);
        }

        private void UpdateOrderId(object recipient, Order message)
        {
            _orderId = message.Id;
        }
    }

    

    public class OrderData
    {
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
