using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Wenote.Core.Models {
    public record WenoteObject {
        public string ID { get; internal set; }

        internal static WenoteObject ReadFromJson(JsonElement element) {
            return new WenoteObject {
                ID = element.GetProperty("id").GetString()
            };
        }
    }
}
