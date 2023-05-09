using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FACTORINGHONOR.PE.WEB.ViewModels
{
    public class EffectiveCostSummaryViewModel
    {
        public double Rate { get; set; }

        public IEnumerable<EffectiveCostRateDetailViewModel> Details { get; set; }
    }
}
