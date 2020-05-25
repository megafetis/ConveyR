using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTest.Entities
{
    public class TestEntity1: IEntity, IHasName
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
