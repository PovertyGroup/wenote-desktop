using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Wenote.Core.Models {
    public record Comment : WenoteObject {
        public User Author { get; internal set; }
        public string Content { get; internal set; }

        public DateTime Date { get; internal set; }

        public bool Blocked { get; internal set; } = false;
        public bool Deleted { get; internal set; } = false;
        public int Points { get; internal set; } = 0;

        internal static async Task<Comment> ReadFromJsonAsync(JsonElement element) {
            var deleted = element.TryGetProperty("deleted", out _);

            var authorID = element.GetProperty("authorUser").GetProperty("id").GetString();
            var json = new Comment {
                ID = element.GetProperty("id").GetString(),
                Author = await Actions.GetUserAsync(authorID),
                Blocked = element.GetProperty("blocked").GetBoolean() || element.GetProperty("blockedThread").GetBoolean(),
                Content = element.GetProperty("content").GetString(),
                Date = element.GetProperty("updatedAt").GetDateTime(),
                Points = element.GetProperty("points").GetInt32(),
                Deleted = deleted
            };
            return json;
        }
    }
}
