using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class SortViewModel
    {
        public SortState ClientNameSort { get; }
        public SortState ManagerNameSort { get; }
        public SortState ProductNameSort { get; }
        public SortState PurchaseDateSort { get; }
        public SortState AmountSort { get; }
        public SortState Current { get; }

        public SortViewModel(SortState sortOrder)
        {
            if (sortOrder == SortState.ClientNameAsc)
                ClientNameSort = SortState.ClientNameDesc;
            else
                ClientNameSort = SortState.ClientNameAsc;

            ManagerNameSort = sortOrder == SortState.ManagerNameAsc
                ? SortState.ManagerNameDesc
                : SortState.ManagerNameAsc;

            ProductNameSort = sortOrder == SortState.ProductNameAsc
                ? SortState.ProductNameDesc
                : SortState.ProductNameAsc;

            PurchaseDateSort = sortOrder == SortState.PurchaseDateAsc
                ? SortState.PurchaseDateDesc
                : SortState.PurchaseDateAsc;

            AmountSort = sortOrder == SortState.AmountAsc
                ? SortState.AmountDesc
                : SortState.AmountAsc;

            Current = sortOrder;
        }
    }
}
