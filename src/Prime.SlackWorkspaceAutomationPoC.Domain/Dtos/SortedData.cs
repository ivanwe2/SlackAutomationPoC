using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prime.SlackWorkspaceAutomationPoC.Domain.Dtos
{
    public class SortedData
    {
        public bool Empty { get; set; } = true;
        public bool Sorted { get; set; } = false;
        public bool Unsorted { get; set; } = true;
    }

}
