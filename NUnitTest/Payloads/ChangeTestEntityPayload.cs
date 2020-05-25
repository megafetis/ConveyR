using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTest.Payloads
{
    public class ChangeTestEntityPayload:IHasNamePayload,IHasDescriptionPayload
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
