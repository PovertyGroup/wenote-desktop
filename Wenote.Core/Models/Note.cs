using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Wenote.Core.Models {
    public record Note : WenoteObject {
        public User Author { get; internal set; }

        public string Title { get; internal set; }
        public string Content { get; internal set; }

        public DateTime Date { get; internal set; }

        public List<string> Likers { get; internal set; } = new();
        public List<string> Starers { get; internal set; } = new();
        public List<string> Tags { get; internal set; } = new();

        public bool Public { get; internal set; } = true;

        internal static async Task<Note> ReadFromJsonAsync(JsonElement element) {
            var authorElement = element.GetProperty("author");
            var author = await Actions.GetUserAsync(authorElement.GetProperty("id").GetString());

            var note = new Note {
                Author = author,
                ID = element.GetProperty("id").GetString(),
                Title = element.GetProperty("title").GetString(),
                Content = element.GetProperty("content").GetString(),
                Public = element.GetProperty("public").GetBoolean(),
                Date = element.GetProperty("updatedAt").GetDateTime(),
            };

            foreach (var liker in element.GetProperty("likers").EnumerateArray())
                note.Likers.Add(liker.GetString());
            foreach (var starer in element.GetProperty("starers").EnumerateArray())
                note.Starers.Add(starer.GetString());
            foreach (var tag in element.GetProperty("tags").EnumerateArray())
                note.Tags.Add(tag.GetString());

            return note;
        }

        internal new static Note ReadFromJson(JsonElement element) {
            var authorElement = element.GetProperty("author");
            var author = Actions.GetUser(authorElement.GetProperty("id").GetString());

            var note = new Note {
                Author = author,
                ID = element.GetProperty("id").GetString(),
                Title = element.GetProperty("title").GetString(),
                Content = element.GetProperty("content").GetString(),
                Public = element.GetProperty("public").GetBoolean(),
                Date = element.GetProperty("updatedAt").GetDateTime(),
            };

            foreach (var liker in element.GetProperty("likers").EnumerateArray())
                note.Likers.Add(liker.GetString());
            foreach (var starer in element.GetProperty("starers").EnumerateArray())
                note.Starers.Add(starer.GetString());
            foreach (var tag in element.GetProperty("tags").EnumerateArray())
                note.Tags.Add(tag.GetString());

            return note;
        }
    }
}
