﻿using esper.elements;
using esper.parsing;
using esper.setup;
using Newtonsoft.Json.Linq;
using System;

namespace esper.defs {
    public class MainRecordDef : MembersDef {
        public static string defType = "record";
        public StructDef headerDef;

        public MainRecordDef(DefinitionManager manager, JObject src, Def parent)
            : base(manager, src, parent) {
            headerDef = manager.BuildMainRecordHeaderDef(src, this);
        }
    }
}
