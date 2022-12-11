using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MobileStore.Data;
using MobileStore.Dto;
using MobileStore.Models;

namespace MobileStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MobileStoreController : ControllerBase
    {
        private readonly MobileStoreContext _context;
        private readonly ILogger<MobileStoreController> _logger;


        public MobileStoreController(MobileStoreContext context, ILogger<MobileStoreController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("Report")]
        public async Task<ActionResult<IEnumerable<MobileInformation>>> Get()
        {
            return await _context.MobileInformation.ToListAsync();
        }

        [HttpPost]
        [Route("Report/Monthly")]
        public async Task<ActionResult<IEnumerable<MobileInformation>>> GetMonthlyReport(TimeReportDto dto)
        {
            DateTime fromDate = DateTime.Parse(dto.FromDate);
            DateTime ToDate = DateTime.Parse(dto.ToDate);
            return await _context.MobileInformation.Where(x => x.DateOfSale.Date >= fromDate.Date && x.DateOfSale.Date <= ToDate.Date).ToListAsync();
        }

        [HttpPost]
        [Route("Report/Monthly/Brand")]
        public async Task<dynamic> GetMonthlyBrandReport(TimeReportDto dto)
        {
            DateTime fromDate = DateTime.Parse(dto.FromDate);
            DateTime ToDate = DateTime.Parse(dto.ToDate);

            var allReport = await _context.MobileInformation.Where(x => x.DateOfSale.Date >= fromDate.Date && x.DateOfSale.Date <= ToDate.Date).ToListAsync();

            var brands = BrandList(allReport);

            var Branlist = new List<dynamic>();

            foreach (var brand in brands)
            {
                List<MobileInformation> list = new List<MobileInformation>();
                foreach (var report in allReport)
                {
                    if (report.BrandName == brand)
                    {
                        list.Add(report);
                    }
                }

                var brandReport = new BrandReport
                {
                    Brand = brand,
                    BrandList = list
                };

                Branlist.Add(brandReport);
            }

            return Branlist;
        }




        [HttpGet]
        [Route("Report/ProfitLoss")]
        public async Task<dynamic> GetProfitandLossReport()
        {
            var allReport = await _context.MobileInformation.ToListAsync();

            var brands = BrandList(allReport);
            
            var profitLossReport = new List<dynamic>();

            foreach (var brand in brands)
            {
                List<MobileInformation> list = new List<MobileInformation>();
                Decimal totalSaleprize = 0;
                Decimal actualPrize= 0;
                
                foreach (var report in allReport)
                {
                    if (report.BrandName == brand)
                    {
                        totalSaleprize += report.SalePrize;
                        actualPrize += report.MobilePrize;
                        list.Add(report);
                    }
                }

                var brandReport = new BrandbaseReport
                {
                    Brand = brand,
                    TotalSalePrize = totalSaleprize,
                    ActualSalePrize = actualPrize,
                    DiscountGiven = actualPrize - totalSaleprize,
                    TotalProfit = totalSaleprize - actualPrize,
                    TotalLoss = actualPrize - totalSaleprize,
                    BrandList = list
                };

                if (brandReport.DiscountGiven < 0) brandReport.DiscountGiven = 0;
                if (brandReport.TotalProfit < 0) brandReport.TotalProfit = 0;
                if (brandReport.TotalLoss < 0) brandReport.TotalLoss = 0;
                profitLossReport.Add(brandReport);
            }

            return profitLossReport;
        }

        private List<string> BrandList(List<MobileInformation> allReport)
        {
            List<string> brands = new List<string>();

            foreach (var item in allReport)
            {
                if (!(brands.Contains(item.BrandName)))
                {
                    brands.Add(item.BrandName);
                }
            }

            return brands;
        }

        [HttpGet]
        [Route("Report/{id}")]
        // GET: MobileStore/Details/5
        public async Task<ActionResult<MobileInformation>> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mobileInformation = await _context.MobileInformation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mobileInformation == null)
            {
                return NotFound();
            }

            return mobileInformation;
        }

        

        [HttpPost]
        [Route("NewSale")]
        public async Task<ActionResult<MobileInformation>> PostUser(ReportDto report)
        {
            var mobileInformation = new MobileInformation();
            mobileInformation.BrandName = report.BrandName;
            mobileInformation.BrandModel = report.BrandModel;
            mobileInformation.CustomerName = report.CustomerName;
            mobileInformation.DateOfSale = DateTime.Now;
            mobileInformation.SalePrize = report.SalePrize;
            mobileInformation.MobilePrize = report.MobilePrize;
            mobileInformation.StoreName = report.StoreName;
            mobileInformation.StoreOwnerName = report.StoreOwnerName;
            mobileInformation.DiscountPrize = report.MobilePrize - report.SalePrize;
            mobileInformation.ProfitPrize = report.SalePrize - report.MobilePrize;
            mobileInformation.LossPrize = report.MobilePrize - report.SalePrize;
            if(mobileInformation.ProfitPrize < 0) mobileInformation.ProfitPrize = 0;
            if(mobileInformation.DiscountPrize < 0) mobileInformation.DiscountPrize = 0;
            if(mobileInformation.LossPrize < 0) mobileInformation.LossPrize = 0;
            
            _context.Add(mobileInformation);
            await _context.SaveChangesAsync();
            return mobileInformation;
        }


        //// POST: MobileStore/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut]
        [Route("Report/Edit/{id}")]
        public async Task<ActionResult<MobileInformation>> Edit(int id, ReportDto report)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mobileInformation = await _context.MobileInformation
                .FirstOrDefaultAsync(m => m.Id == id);

            mobileInformation.BrandName = report.BrandName;
            mobileInformation.BrandModel = report.BrandModel;
            mobileInformation.CustomerName = report.CustomerName;
            mobileInformation.DateOfSale = DateTime.Now;
            mobileInformation.SalePrize = report.SalePrize;
            mobileInformation.MobilePrize = report.MobilePrize;
            mobileInformation.StoreName = report.StoreName;
            mobileInformation.StoreOwnerName = report.StoreOwnerName;
            mobileInformation.DiscountPrize = report.MobilePrize - report.SalePrize;
            mobileInformation.ProfitPrize = report.SalePrize - report.MobilePrize;
            mobileInformation.LossPrize = report.MobilePrize - report.SalePrize;
            if (mobileInformation.ProfitPrize < 0) mobileInformation.ProfitPrize = 0;
            if (mobileInformation.DiscountPrize < 0) mobileInformation.DiscountPrize = 0;
            if (mobileInformation.LossPrize < 0) mobileInformation.LossPrize = 0;

            _context.Update(mobileInformation);
            await _context.SaveChangesAsync();

            return mobileInformation;
        }

        [HttpDelete]
        [Route("Report/Delete/{id}")]
        //// GET: MobileStore/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mobileInformation = await _context.MobileInformation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mobileInformation == null)
            {
                return NotFound();
            }
            _context.Remove(mobileInformation);
            await _context.SaveChangesAsync();

            return Ok();
        }


    }
}
