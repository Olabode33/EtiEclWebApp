using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestDemo.MultiTenancy.HostDashboard.Dto;

namespace TestDemo.MultiTenancy.HostDashboard
{
    public interface IIncomeStatisticsService
    {
        Task<List<IncomeStastistic>> GetIncomeStatisticsData(DateTime startDate, DateTime endDate,
            ChartDateInterval dateInterval);
    }
}