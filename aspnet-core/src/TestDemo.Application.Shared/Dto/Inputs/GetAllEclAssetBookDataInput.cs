using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.Dto.Inputs
{
    public class GetAllEclAssetBookDataInput: GetAllEclDataInputBase
    {
        public string AssetDescriptionFilter { get; set; }
        public string AssetTypeFilter { get; set; }
        public string CounterPartyFilter { get; set; }
    }
}
