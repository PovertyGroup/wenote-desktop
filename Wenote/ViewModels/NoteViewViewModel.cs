using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Markdig;
using Neo.Markdig.Xaml;

namespace Wenote.ViewModels {
    class NoteViewViewModel {
        public string NoteMd { get; set; }
        public string NoteTitle { get; set; }

        public FlowDocument NoteDocument => MarkdownXaml.ToFlowDocument(this.NoteMd,
            new MarkdownPipelineBuilder()
                .UseXamlSupportedExtensions()
                .Build()
        );
    }
}
