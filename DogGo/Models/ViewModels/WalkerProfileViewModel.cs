using System;
using System.Collections.Generic;

namespace DogGo.Models.ViewModels
{
    public class WalkerProfileViewModel
    {
        public Walker Walker { get; set; }

        public List<Walk> Walks { get; set; }

        public int TotalTime { get; set; }
        
        public TimeSpan TS { get; set; }
    }
}
