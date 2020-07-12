﻿using esper.data;
using esper.parsing;
using esper.setup;
using Newtonsoft.Json.Linq;
using System;

namespace esper.defs {
    public class BytesDef : ValueDef {
        public static string defType = "bytes";

        public int size {
            get {
                if (!src.ContainsKey("size")) return 0;
                return src.Value<int>("size");
            }
        }

        public BytesDef(DefinitionManager manager, JObject src, Def parent = null)
            : base(manager, src, parent) {
            if (size < 0) throw new Exception("Def source has invalid size" + size);
        }

        public DataContainer ReadData(PluginFileSource source) {
            return new BytesData(source, size);
        }
    }
}
