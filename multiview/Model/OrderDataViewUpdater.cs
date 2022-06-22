using CommunityToolkit.Mvvm.Messaging;
using multiview.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace multiview.Model
{
    public class OrderDataViewUpdater :ICommand
    {
        private OrderListViewModel _orderListViewModel ;
        public OrderDataViewUpdater(OrderListViewModel orderListViewModel)
        {
            _orderListViewModel = orderListViewModel;
        }

        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            if (parameter is null)
                return;
            var order = (Order)parameter;

            WeakReferenceMessenger.Default.Send(order);

        }
    }
}
