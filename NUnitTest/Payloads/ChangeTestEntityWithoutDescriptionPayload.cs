using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTest.Payloads
{
    public class ChangeTestEntityWithoutDescriptionPayload: IHasNamePayload
    {
        public string Name { get; set; }
    }
}
