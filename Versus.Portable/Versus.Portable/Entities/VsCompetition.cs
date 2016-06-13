﻿using System;

namespace Versus.Portable.Entities
{
    public class VsCompetition
    {
        public DateTime EndingDate { get; set; }
        public string StartedBy { get; set; }
        public string BackdropUrl { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string CompetitorName1 { get; set; }
        public string CompetitorName2 { get; set; }
        public int CompetitorScore1 { get; set; }
        public int CompetitorScore2 { get; set; }
    }
}
