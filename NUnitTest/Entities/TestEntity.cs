using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTest.Entities
{
    public class TestEntity: IEntity,IHasName, IHasDescription, ITimestampedEntity,IHasFaledCount
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? Timestamp { get; set; }
        public int FailCount { get; set; }
    }
}
