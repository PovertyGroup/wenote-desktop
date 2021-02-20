using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Wenote.Core.Models {
    public partial record User : WenoteObject {
        public string Token { get; set; }

        /// <summary>
        /// Username of the user.
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Url of the user's avatar.
        /// </summary>
        public string AvatarUrl { get; set; }
        /// <summary>
        /// Bio of user.
        /// </summary>
        public string Bio { get; set; }
        /// <summary>
        /// Email of user.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Gender of the user.
        /// </summary>
        public Gender Gender { get; set; }

        public List<string> Followers { get; set; } = new();
        public List<string> Followees { get; set; } = new();
        public List<string> LikedNotes { get; set; } = new();
        public List<string> StaredNotes { get; set; } = new();
        public List<string> Notes { get; set; } = new();

        public User() { }

        internal static User ReadFromJson(JsonElement userElement, bool forceRefresh = false) {
            var genderStr = userElement.GetProperty("gender").GetString();
            var id = userElement.GetProperty("id").GetString();

            if (UserCache.ContainsKey(id) && !forceRefresh) {
                return UserCache[id];
            }

            var hasAvatar = userElement.TryGetProperty("avatar", out var avatarElement);

            var user = new User {
                ID = id,
                Username = userElement.GetProperty("username").GetString(),
                AvatarUrl = Utils.ComposeUrl(
                    Configuration.ServerUrl,
                    hasAvatar ? avatarElement.GetProperty("url").GetString() : Configuration.DefaultAvatarUrl),
                Bio = userElement.GetProperty("bio").GetString(),
                Email = userElement.GetProperty("email").GetString(),
                Gender = genderStr switch {
                    "girl" => Gender.Girl,
                    "boy" => Gender.Boy,
                    _ => Gender.Unknown
                }
            };

            foreach (var followee in userElement.GetProperty("followees").EnumerateArray())
                user.Followees.Add(followee.GetString());
            foreach (var follower in userElement.GetProperty("followers").EnumerateArray())
                user.Followers.Add(follower.GetString());
            foreach (var like in userElement.GetProperty("likes").EnumerateArray())
                user.LikedNotes.Add(like.GetProperty("id").GetString());
            foreach (var note in userElement.GetProperty("notes").EnumerateArray())
                user.Notes.Add(note.GetString());
            foreach (var staredNote in userElement.GetProperty("stared_notes").EnumerateArray())
                user.StaredNotes.Add(staredNote.GetProperty("id").GetString());

            UserCache[id] = user;

            return user;
        }
    }
}
