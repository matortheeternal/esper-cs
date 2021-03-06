﻿using esper.helpers;
using esper.setup;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace esper.defs {
    public class MembersDef : ElementDef {
        private readonly bool _canContainFormIds;

        public ReadOnlyCollection<ElementDef> memberDefs;
        public List<string> signatures;
        public override bool canContainFormIds => _canContainFormIds;
        public override ReadOnlyCollection<ElementDef> childDefs => memberDefs;

        public MembersDef(DefinitionManager manager, JObject src)
            : base(manager, src) {
            memberDefs = JsonHelpers.Defs<ElementDef>(manager, src, "members");
            signatures = GetSignatures();
            _canContainFormIds = memberDefs.Any(d => d.canContainFormIds);
        }

        public ElementDef GetMemberDef(string signature, ref int defIndex) {
            if (!signatures.Contains(signature)) return null;
            for (; defIndex < memberDefs.Count; defIndex++) {
                var memberDef = memberDefs[defIndex];
                if (memberDef.ContainsSignature(signature)) return memberDef;
            }
            return null;
        }

        public override bool ContainsSignature(string signature) {
            return signatures.Contains(signature);
        }

        public override List<string> GetSignatures(List<string> sigs = null) {
            if (sigs == null) sigs = new List<string>();
            foreach (var def in memberDefs)
                def.GetSignatures(sigs);
            return sigs;
        }
    }
}
