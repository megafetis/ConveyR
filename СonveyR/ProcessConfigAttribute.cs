using System;

namespace ConveyR
{
    /// <summary>
    /// Process config to set ordering and handling cases
    /// </summary>
    public class ProcessConfigAttribute:Attribute
    {
        /// <summary>
        /// Grouping of processes. Required if needed to create different cases of handling the same entity
        /// </summary>
        public string Group { get; set; } = null;
        /// <summary>
        /// Setting to specify order of execution handlers. Sort process handlers by Order
        /// </summary>
        public int Order { get; set; } = 0;
    }
}
