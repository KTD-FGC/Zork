using System;
using System.Collections.Generic;
using System.Text;

namespace Zork.Common
{
    public class Enemy
    {
        public string Name { get; }

        public string LivingDescription { get; }

        public string DeadDescription { get; }

        public string PKText { get; }

        public string EKText { get; }

        public bool IsAlive { get; set; }

        public bool Encounter { get; set; }

        public Enemy (string name, string livingDescription, string deadDescription, string pkText, string ekText, bool isAlive)
        {
            Name = name;
            LivingDescription = livingDescription;
            DeadDescription = deadDescription;
            PKText = pkText;
            EKText = ekText;
            IsAlive = isAlive;
        }
    }
}
