﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storm.Config
{
    public class Config : Dictionary<string, object>
    {
        public const string STORM_LOCAL_HOSTNAME = "storm.local.hostname";

        public bool GetBoolean(string name)
        {
            return true;
        }
        public int GetInt(string name)
        {
            return 100;
        }
    }
}
