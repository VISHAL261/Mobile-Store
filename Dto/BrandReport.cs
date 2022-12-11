using MobileStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileStore.Dto
{
    public class BrandReport
    {
        public string Brand { get; set; }
        public List<MobileInformation> BrandList { get; set; }

    }
}
