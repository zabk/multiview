using System;

namespace multiview.Model
{
    public class FilterSettings
    {
        //, serial, _pageSize,currentPage, selectedSortDirection, selectedSortByOption
        public string Status { get; set; }
        public int PageSize { get; set; }
        public string Email { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTill { get; set; }
        public string ProductName { get; set; }
        public int? MinProdCount { get; set; }
        public int? MaxProdCount { get; set; }
        public decimal? MinOrderValue { get; set; }
        public decimal? MaxOrderValue { get; set; }
        public int? Serial { get; set; }
        public int CurrentPage { get; set; }
        public string SortDirection { get; set; }
        public string SortBy { get; set; }

    }
}
