﻿using System;

namespace Cw02
{
    public class Studies
    {
        public Studies(string name, string mode)
        {
            this.name = name;
            this.mode = mode;
        }
        public Studies()
        {
            name = null;
            mode = null;
        }

        public String name { get; set; }
        public String mode { get; set; }
    }
}
