using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Wenote.Core.Models;

namespace Wenote.Core {
    public static class Actions {

        #region Asychronized Methods

        public static async Task<bool> RegisterAsync(string username, string password, string email) {
            const string url = "/auth/local/register";
            Dictionary<string, string> payload = new() {
                {"username", username},
                {"password", password},
                {"email", email}
            };
            var res = await HttpInteraction.PostAsync(
                Utils.ComposeUrl(Configuration.ServerUrl, url),
                payload
            );
            if (res.IsSuccessStatusCode) {
                return true;
            } else {
                var content = res.Content.ReadAsStream();
                var reader = JsonDocument.Parse(content);
                var errMsg = reader.RootElement.GetProperty("message").GetProperty("message").GetString();
                throw new Exception(errMsg);
            }
        }

        public static async Task<User> LoginAsync(string identifier, string password) {
            const string url = "/auth/local";
            Dictionary<string, string> payload = new() {
                { "identifier", identifier },
                { "password", password }
            };
            var res = await HttpInteraction.PostAsync(
                Utils.ComposeUrl(Configuration.ServerUrl, url),
                payload
            );

            var content = res.Content.ReadAsStream();
            var reader = JsonDocument.Parse(content);
            var root = reader.RootElement;

            if (res.IsSuccessStatusCode) {
                var userElement = root.GetProperty("user");
                return User.ReadFromJson(userElement) with { Token = root.GetProperty("jwt").GetString() };
            } else {
                var errMsg = root.GetProperty("message").GetProperty("message").GetString();
                throw new Exception(errMsg);
            }
        }

        public static async Task<User> GetUserAsync(string id, string jwt = "") {
            string url = $"/users/{id}";
            var res = await HttpInteraction.GetAsync(
                Utils.ComposeUrl(Configuration.ServerUrl, url),
                jwt
            );

            var content = res.Content.ReadAsStream();
            var reader = JsonDocument.Parse(content);
            var root = reader.RootElement;

            if (res.IsSuccessStatusCode) {
                return User.ReadFromJson(root);
            } else {
                var errMsg = root.GetProperty("message").GetString();
                throw new Exception(errMsg);
            }
        }

        public static async Task<Note> GetNoteAsync(string id, string jwt = "") {
            string url = $"/notes/{id}";
            var res = await HttpInteraction.GetAsync(
                Utils.ComposeUrl(Configuration.ServerUrl, url),
                jwt
            );

            var content = res.Content.ReadAsStream();
            var reader = JsonDocument.Parse(content);
            var root = reader.RootElement;

            if (res.IsSuccessStatusCode) {
                return await Note.ReadFromJsonAsync(root);
            } else {
                var errMsg = root.GetProperty("message").GetString();
                throw new Exception(errMsg);
            }
        }

        public static async Task<List<Note>> GetRecommendNotesAsync(string jwt = "") {
            string url = $"/recommand";
            var res = await HttpInteraction.GetAsync(
                Utils.ComposeUrl(Configuration.ServerUrl, url),
                jwt
            );

            var content = res.Content.ReadAsStream();
            var reader = JsonDocument.Parse(content);
            var root = reader.RootElement;

            if (res.IsSuccessStatusCode) {
                var noteElements = root.EnumerateArray();

                var notes = new List<Note>();

                var tasks = new Task[noteElements.Count()];
                int i = 0;

                foreach (var noteElement in noteElements) {
                    tasks[i++] = Actions.GetNoteAsync(noteElement.GetString()).ContinueWith(t => {
                        notes.Add(t.Result);
                    });
                }

                Task.WaitAll(tasks);

                return notes;
            } else {
                var errMsg = root.GetProperty("message").GetString();
                throw new Exception(errMsg);
            }
        }

        public static async Task<List<Comment>> GetCommentsAsync(string noteID, string jwt = "") {
            string url = $"/comments/note:{noteID}";
            var res = await HttpInteraction.GetAsync(
                Utils.ComposeUrl(Configuration.ServerUrl, url),
                jwt
            );

            var content = res.Content.ReadAsStream();
            var reader = JsonDocument.Parse(content);
            var root = reader.RootElement;

            if (res.IsSuccessStatusCode) {
                var ls = new List<Comment>();
                foreach (var comment in root.EnumerateArray())
                    ls.Add(await Comment.ReadFromJsonAsync(comment));
                return ls;
            } else {
                var errMsg = root.GetProperty("message").GetString();
                throw new Exception(errMsg);
            }
        }

        #endregion

        #region Sychronized Methods

        public static Note GetNote(string id, string jwt = "") {
            string url = $"/notes/{id}";
            var res = HttpInteraction.Get(
                Utils.ComposeUrl(Configuration.ServerUrl, url),
                jwt
            );

            var content = res.Content.ReadAsStream();
            var reader = JsonDocument.Parse(content);
            var root = reader.RootElement;

            if (res.IsSuccessStatusCode) {
                return Note.ReadFromJson(root);
            } else {
                var errMsg = root.GetProperty("message").GetString();
                throw new Exception(errMsg);
            }
        }

        public static User GetUser(string id, string jwt = "") {
            string url = $"/users/{id}";
            var res = HttpInteraction.Get(
                Utils.ComposeUrl(Configuration.ServerUrl, url),
                jwt
            );

            var content = res.Content.ReadAsStream();
            var reader = JsonDocument.Parse(content);
            var root = reader.RootElement;

            if (res.IsSuccessStatusCode) {
                return User.ReadFromJson(root);
            } else {
                var errMsg = root.GetProperty("message").GetString();
                throw new Exception(errMsg);
            }
        }

        #endregion
    }
}