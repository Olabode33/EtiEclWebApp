using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.BackgroundJobs;
using TestDemo.Storage;

namespace TestDemo.Web.Controllers
{
    public class EclRawDataController : EclRawDataControllerBase
    {
        public EclRawDataController(
            IBinaryObjectManager binaryObjectManager, 
            IBackgroundJobManager backgroundJobManager) 
            : base(binaryObjectManager, backgroundJobManager)
        {
        }
    }
}
