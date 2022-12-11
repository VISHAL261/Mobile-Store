using MobileStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileStore.Dto
{
    public class ProfitAndLossReport
    {

        public decimal TotalSalePrize { get; set; }
        public decimal ActualSalePrize { get; set; }
        public decimal DiscountGiven { get; set; }
        public decimal TotalProfit { get; set; }
        public decimal TotalLoss { get; set; }
        public List<MobileInformation> BrandList { get; set; }
    }

}
