﻿using esper.elements;
using esper.setup;
using Newtonsoft.Json.Linq;
using System;

namespace esper.defs.TES5 {
    public class ScriptFragmentsInfoCounter : CounterDef {
        public static string defType = "ScriptFragmentsInfoCounter";

        public ScriptFragmentsInfoCounter(
            DefinitionManager manager, JObject src, Def parent
        ) : base(manager, src, parent) {}

        public override void SetCount(Container container, UInt32 count) {
            throw new NotImplementedException();
        }

        public override UInt32 GetCount(Container container) {
            throw new NotImplementedException();
        }
    }
}