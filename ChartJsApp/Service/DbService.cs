using ChartJsApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ChartJsApp.Service
{
    public class DbService:IDbService
    {
        private readonly NorthwindContext _context;
       
        public DbService(NorthwindContext context)
        {
            _context = context;

           
        }

        public List<ViewModel> getYearMonthFreight()
        {
            var result = _context.Orders.GroupBy(o => new
            {
                Month = o.OrderDate.Value.Month,
                Year = o.OrderDate.Value.Year
            }).Select(y => new { Ay = y.Key, Toplam = y.Select(x => x.Freight.Value).Sum() });


            result = result.OrderBy(a => a.Ay.Year);


            List<ViewModel> listmodel = new List<ViewModel>();

            foreach (var item in result)
            {
                ViewModel model = new ViewModel();

                model.Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(item.Ay.Month);
                model.Year = item.Ay.Year.ToString();
                model.Freight = item.Toplam;
             

                //string datee = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(item.Ay.Month);
                //test.Add(datee, item.Toplam.ToString());


                listmodel.Add(model);

            }

            return listmodel;
        }
    }
}
