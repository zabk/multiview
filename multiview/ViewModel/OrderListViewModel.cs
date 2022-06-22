using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Messaging;
using multiview.Model;

namespace multiview.ViewModel
{
    public class OrderListViewModel : INotifyPropertyChanged
    {
        private FilterSettings _filterSettings;
        public FilterSettings FilterSettings
        {
            get { return _filterSettings; }
            set { _filterSettings = value; RaisePropertyChange(); GetData(_filterSettings); }
        }
        private ObservableCollection<Order> _orders;
        public ObservableCollection<Order> Orders
        {
            get { return _orders; } 
            set { _orders = value; RaisePropertyChange(); }
        }
        #region paging
        private int _pageCount;
        public int PageCount
        {
            get { return _pageCount;}
            private set { _pageCount = value; RaisePropertyChange(); }
        }

        private int _currentPage;
        public int CurrentPage
        {
            get { return _currentPage; }
            set { _currentPage = value; RaisePropertyChange(); }
        }

        
        private bool _isNextPageAvailable;
        public bool IsNextPageAvailable
        { 
            get { return _isNextPageAvailable; }
            set { _isNextPageAvailable = value; RaisePropertyChange(); }
        }

        private bool _isPrevPageAvailable;
        public bool IsPrevPageAvailable
        {
            get { return _isPrevPageAvailable; }
            set { _isPrevPageAvailable = value; RaisePropertyChange(); }
        }

        

        private ICommand _nextPage;
        public ICommand NextPage
        {
            get => _nextPage = _nextPage ?? new CommandForAction(GoToNextPage);

        }

        private ICommand _prevPage;
        public ICommand PrevPage
        {
            get => _prevPage = _prevPage ?? new CommandForAction(GoToPrevPage);

        }

        private ICommand _lastPage;
        public ICommand LastPage
        {
            get => _lastPage = _lastPage ?? new CommandForAction(GoToLastPage);

        }

        private ICommand _firstPage;
        public ICommand FirstPage
        {
            get => _firstPage = _firstPage ?? new CommandForAction(GoToFirstPage);

        }

        private void GoToNextPage()
        {
            _currentPage++;
            UpdatePageData();
        }

        private void GoToPrevPage()
        {
            _currentPage--;
            UpdatePageData();
        }

        private void GoToLastPage()
        {
            _currentPage = _pageCount;
            UpdatePageData();
        }

        private void GoToFirstPage()
        {
            _currentPage = 1;
            UpdatePageData();
        }

        private void UpdatePageData()
        {
            _isNextPageAvailable = (_currentPage < _pageCount);
            _isPrevPageAvailable = (_currentPage > 1);
            CurrentPage = _currentPage;
            IsNextPageAvailable = _isNextPageAvailable;
            IsPrevPageAvailable = _isPrevPageAvailable;
            FilterSettings.CurrentPage = _currentPage;
        }
        #endregion

        private ICommand _processCommand;
        public ICommand ProcessCommand
        {
            get => _processCommand = _processCommand ?? new ProcessingCommand(SetProcessed);

        }

        private ICommand _updateOrderDetails;
        public ICommand UpdateOrderDetails
        {
            get => _updateOrderDetails = _updateOrderDetails ?? new OrderDataViewUpdater(this);
        }

        private void SetProcessed(Guid id)
        {
            Orders.Where(x => x.Id == id).FirstOrDefault().Status = 2;
        }

        public OrderListViewModel()
        {
            
            WeakReferenceMessenger.Default.Register<FilterSettings>(this, UpdateFilterList);
            _currentPage = 1;
            _filterSettings = new FilterSettings
            {
                CurrentPage = _currentPage,
                PageSize = 10,
                SortBy="OrderDate",
                SortDirection="DESC"
            };
            _pageCount = 10;
            _isNextPageAvailable = (_currentPage < _pageCount);
            _isPrevPageAvailable = (_currentPage > 1);
            _orders = new ObservableCollection<Order>();
            var order1 = new Order { Id=Guid.NewGuid(),Serial=1,OrderDate=DateTime.Today, Status=1, Name="Test", OrderValue=12,ProductCount=3, Processable=true, Email = "testemail", };
            var order2 = new Order { Id = Guid.NewGuid(), Serial = 2, OrderDate = DateTime.Today, Status = 2, Name = "Test", OrderValue = 12, ProductCount = 3, Processable = false, Email = "testemail", };
            _orders.Add(order1);
            _orders.Add(order2);
        }

        public void UpdateFilterList(object recepient, FilterSettings message)
        {
            FilterSettings = message;
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChange([CallerMemberName] string propertyname = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
            
        }

        private void GetData(FilterSettings filter)
        {
            var order1 = new Order { Id = Guid.NewGuid(), Serial = filter.CurrentPage, OrderDate = DateTime.Today, Status = 1, Name = filter.Email+"1", OrderValue = 12, ProductCount = 3, Processable = true, Email = "testemail", };
            var order2 = new Order { Id = Guid.NewGuid(), Serial = filter.CurrentPage, OrderDate = DateTime.Today, Status = 2, Name = filter.Email+"2", OrderValue = 12, ProductCount = 3, Processable = false, Email = "testemail", };
            Orders.Add(order1);
            Orders.Add(order2);
        }
    }

    public class Order
    {
        public Guid Id { get; set; }
        public int Serial { get; set; }
        public DateTime OrderDate { get; set; }
        public int Status { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal OrderValue { get; set; }
        public int ProductCount { get; set; }
        public bool Processable { get; set; }
        
    }
}
