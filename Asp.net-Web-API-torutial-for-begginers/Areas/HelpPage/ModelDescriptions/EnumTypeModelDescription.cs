using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Asp.net_Web_API_torutial_for_begginers.Areas.HelpPage.ModelDescriptions
{
    public class EnumTypeModelDescription : ModelDescription
    {
        public EnumTypeModelDescription()
        {
            Values = new Collection<EnumValueDescription>();
        }

        public Collection<EnumValueDescription> Values { get; private set; }
    }
}