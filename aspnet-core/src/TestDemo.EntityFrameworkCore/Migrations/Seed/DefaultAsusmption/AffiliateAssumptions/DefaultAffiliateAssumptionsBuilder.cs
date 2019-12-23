using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestDemo.EclShared;
using TestDemo.EntityFrameworkCore;

namespace TestDemo.Migrations.Seed.DefaultAsusmption.AffiliateAssumptions
{
    public class DefaultAffiliateAssumptionsBuilder
    {
        private readonly TestDemoDbContext _context;

        public DefaultAffiliateAssumptionsBuilder(TestDemoDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            long[] ous = _context.OrganizationUnits.IgnoreQueryFilters().Select(x => x.Id).ToArray();

            foreach (var ou in ous)
            {
                var c = _context.AffiliateAssumptions.IgnoreQueryFilters().FirstOrDefault(x => x.OrganizationUnitId == ou);
                if (c == null)
                {
                    _context.AffiliateAssumptions.Add(new AffiliateAssumption()
                    {
                        OrganizationUnitId = ou,
                        LastAssumptionUpdate = DateTime.Now,
                        LastWholesaleReportingDate = DateTime.Now,
                        LastRetailReportingDate = DateTime.Now,
                        LastObeReportingDate = DateTime.Now,
                        LastSecuritiesReportingDate = DateTime.Now
                    });
                }
            }
        }
    }
}
