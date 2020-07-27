﻿using esper.parsing;
using esper.setup;
using esper.elements;
using System.Text;
using System;
using esper.resolution;

namespace esper.plugins {
    public class PluginFile : Container, IMasterManager, IRecordManager {
        public MainRecord header;
        public Session session;
        public string filename;
        public PluginFileOptions options;
        public PluginFileSource source;
        public Encoding stringEncoding { get => session.options.encoding; }

        PluginFile IMasterManager.file => this;
        ReadOnlyMasterList IMasterManager.originalMasters { get; set; }
        MasterList IMasterManager.masters { get; set; }

        PluginFile IRecordManager.file => this;
        RecordMap<UInt32> IRecordManager.localRecordsByFormId { get; set; }
        PluginRecordMap<UInt32> IRecordManager.remoteRecordsByFormId { get; set; }

        public string filePath => source?.filePath;
        public new DefinitionManager manager => session.definitionManager;
        public bool localized => header.GetRecordFlag("Localized");

        public PluginFile(Session session, string filename, PluginFileOptions options)
            : base() {
            this.session = session;
            this.filename = filename;
            this.options = options;
        }

        public bool IsEsl() {
            return false; // TODO
        }

        public bool IsDummy() {
            return source == null;
        }

        internal void ReadFileHeader() {
            if (header != null) return;
            source.ReadFileHeader(this);
            this.InitMasters();
        }

        internal void ReadGroups() {
            throw new NotImplementedException();
        }

        internal string GetString(uint id) {
            throw new NotImplementedException();
        }
    }
}
