using System;
using System.Collections.Generic;
using System.Text;

namespace СonveyoR
{
    public class ProcessOrderAttribute:Attribute
    {
        public ProcessCase ProcessCase { get; }
        public int Order { get; }

        public ProcessOrderAttribute(ProcessCase processCase, int order = 0)
        {
            ProcessCase = processCase;
            Order = order;
        }
    }
}
