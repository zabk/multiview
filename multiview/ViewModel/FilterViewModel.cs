using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using multiview.Model;

namespace multiview.ViewModel
{
    public class FilterViewModel:INotifyPropertyChanged
    {
        public string[] _possibleStatuses = { "All", "Ordered","Processed" };
        public string[] PossibleStatuses
        {
            get { return _possibleStatuses; }
            set { _possibleStatuses = value; }
        }

        public string[] _possibleSortDirection = { "ASC", "DESC" };
        public string[] PossibleSortDirection
        {
            get { return _possibleSortDirection; }
            set { _possibleSortDirection = value; }
        }

        public string[] _possibleSortBy = { "OrderDate", "Serial", "Email" };
        public string[] PossibleSortBy
        {
            get { return _possibleSortBy; }
            set { _possibleSortBy = value; }
        }

        private int[] _possiblePageSizes = { 10, 25, 50 };
        public int[] PossiblePageSizes
        {
            get { return _possiblePageSizes; }
            set { _possiblePageSizes = value; }
        }

        FilterSettings _filters;
        public FilterSettings Filters
        {
            get { return _filters; }
            set { _filters = value; RaisePropertyChange(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChange([CallerMemberName] string propertyname = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        private ICommand actionCommand;
        public ICommand ActionCommand
        {
            get => actionCommand = actionCommand ?? new FilterUpdateCommand(this);
        }

        public FilterViewModel()
        {
            Filters=new FilterSettings();
            Filters.Status=_possibleStatuses[0];
            Filters.PageSize = _possiblePageSizes[0];
            Filters.SortBy = _possibleSortBy[0];
            Filters.SortDirection = _possibleSortDirection[0];
            Filters.DateFrom = null;
            Filters.DateTill = null;
            Filters.CurrentPage = 1;
            var command=new FilterUpdateCommand(this);
            command.Execute(Filters);

        }

        
    }
}
