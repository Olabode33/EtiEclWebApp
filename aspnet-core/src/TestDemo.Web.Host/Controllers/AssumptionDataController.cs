using Abp.BackgroundJobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestDemo.Storage;

namespace TestDemo.Web.Controllers
{
    public class AssumptionDataController : AssumptionDataControllerBase
    {
        public AssumptionDataController(
           IBinaryObjectManager binaryObjectManager,
           IBackgroundJobManager backgroundJobManager)
           : base(binaryObjectManager, backgroundJobManager)
        {
        }
    }
}
