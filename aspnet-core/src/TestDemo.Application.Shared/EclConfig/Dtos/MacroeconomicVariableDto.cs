
using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace TestDemo.EclShared.Dtos
{
    public class MacroeconomicVariableDto : EntityDto
    {
		public string Name { get; set; }
    }

    public class OverrideMacroeconomicVariableDto
    {
        public int MacroId { get; set; }
        public List<OverrideSelectedMacroeconomicVariableDto> MacroeconomicVariables { get; set; }
    }

    public class OverrideSelectedMacroeconomicVariableDto: EntityDto
    {
        public int MacroeconomicVariableId { get; set; }
        public long AffiliateId { get; set; }
        public int BackwardOffset { get; set; }
    }

    public class GetSelectedMacroeconomicVariableDto
    {
        public MacroeconomicVariableDto MacroeconomicVariable { get; set; }
        public OverrideSelectedMacroeconomicVariableDto SelectedMacroeconomicVariable { get; set; }
    }


}