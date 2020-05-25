using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTest.Entities
{
    public interface ITimestampedEntity
    {
        DateTime? Timestamp { get; set; }
    }
}
