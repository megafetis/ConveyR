using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTest.Entities
{
    public interface IHasFaledCount
    {
        int FailCount { get; set; }
    }
}
