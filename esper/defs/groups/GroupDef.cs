﻿using esper.data;
using esper.elements;
using esper.helpers;
using esper.plugins;
using esper.setup;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;

namespace esper.defs {
    public class GroupDef : ElementDef {
        public static string defType = "group";

        internal ReadOnlyCollection<ElementDef> childrenDefs;

        public override ReadOnlyCollection<ElementDef> childDefs => childrenDefs;
        public virtual int groupType => throw new NotImplementedException();
        public virtual bool hasRecordParent => false;
        public virtual bool isChildGroup => false;
        public virtual bool isChildGroupChild => false;
        public virtual bool isTopGroup => false;

        public GroupDef(DefinitionManager manager, JObject src)
            : base(manager, src) {
            childrenDefs = JsonHelpers.Defs<ElementDef>(
                manager, src, "children", true
            );
        }

        public virtual dynamic ConvertLabel(GroupRecord group, byte[] label) {
            throw new NotImplementedException();
        }

        public virtual string GetName(GroupRecord group) {
            return name;
        }

        public string GetDisplayName(GroupRecord group) {
            return displayName ?? GetName(group);
        }

        internal virtual byte[] ParseLabel(Container container, string name) {
            throw new NotImplementedException();
        }

        public GroupRecord CreateFromName(
            Container container, string name
        ) {
            var label = ParseLabel(container, name);
            return new GroupRecord(container, this, label);
        }

        internal void ReadChildren(GroupRecord group, PluginFileSource source) {
            var file = group.file;
            source.ReadMultiple(group.dataSize, () => {
                var sig = Signature.Read(source);
                source.stream.Position -= 4;
                if (sig == Signatures.GRUP) {
                    GroupRecord.Read(group, source);
                } else {
                    var rec = MainRecord.Read(group, source, sig);
                    file.IndexRecord(rec);
                }
            });
            group.internalElements.TrimExcess();
        }
    }
}