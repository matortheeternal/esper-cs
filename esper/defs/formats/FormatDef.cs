﻿using esper.setup;
using Newtonsoft.Json.Linq;

namespace esper.defs {
    public class FormatDef : Def {
        public FormatDef(DefinitionManager manager, JObject src, Def parent = null)
            : base(manager, src, parent) {
        }
    }
}