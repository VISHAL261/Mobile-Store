using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileStore.Dto
{
    public class ReportDto
    {
        public string BrandName { get; set; }

        public string BrandModel { get; set; }

        public string StoreName { get; set; }

        public string StoreOwnerName { get; set; }

        public string CustomerName { get; set; }

        public decimal MobilePrize { get; set; }

        public decimal SalePrize { get; set; }
    }
}
