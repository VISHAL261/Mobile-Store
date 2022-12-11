using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileStore.Models
{
    public class MobileInformation
    {
        public int Id { get; set; }

        public DateTime DateOfSale { get; set; }

        public string BrandName { get; set; }

        public string BrandModel { get; set; }

        public string StoreName { get; set; }

        public string StoreOwnerName { get; set; }

        public string CustomerName { get; set; }

        public decimal MobilePrize { get; set; }

        public decimal DiscountPrize { get; set; }

        public decimal SalePrize { get; set; }

        public decimal ProfitPrize { get; set; }

        public decimal LossPrize { get; set; }
    }
}
