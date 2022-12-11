using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileStore.Dto
{
    public class BestPrizeReportDto
    {
        public string Brand { get; set; }

        public decimal BestPrize { get; set; }

        public List<BrandPrizeinformationDto> BrandList { get; set; }

    }

    public class BrandPrizeinformationDto
    {
        public DateTime DateOfSale { get; set; }

        public decimal SalePrize { get; set; }
    }

}
