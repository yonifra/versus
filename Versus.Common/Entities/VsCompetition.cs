using System;

namespace Versus.Common.Entities
{
    public class VsCompetition
    {
        public DateTime EndingDate { get; set; }
        public string StartedBy { get; set; }
        public string BackdropUrl { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
    }
}
