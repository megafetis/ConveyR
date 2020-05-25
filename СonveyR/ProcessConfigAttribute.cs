using System;


namespace СonveyoR
{
    public class ProcessConfigAttribute:Attribute
    {
        public string ProcessCase { get; }
        public int Order { get; }
        /// <summary>
        /// Process config to set ordering and handling cases
        /// </summary>
        /// <param name="processCase">Case of process. Required if needed to create different cases of handling the same entity</param>
        /// <param name="order"></param>
        public ProcessConfigAttribute(string processCase, int order = 0)
        {
            ProcessCase = processCase;
            Order = order;
        }
    }
}
