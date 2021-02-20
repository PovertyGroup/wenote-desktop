using System;
using Wenote.Core;

namespace Wenote.Core.Test {
    class Program {
        static void Main(string[] args) {
            try {
                var b = Actions.RegisterAsync("dasdsadas", "123@q", "1511956322@qq.com").GetAwaiter().GetResult();
                Console.WriteLine("1511956322@qq.com: 注册成功");
            } catch (Exception e) {
                Console.WriteLine($"1511956322@qq.com: 注册失败 - {e.Message}");
            }

            Console.WriteLine();

            try {
                var user = Actions.LoginAsync("codgi", "123456").GetAwaiter().GetResult();
                Console.WriteLine("codgi: 登陆成功");
                Console.WriteLine($"codgi: username - {user.Username}");
                Console.WriteLine($"codgi: email    - {user.Email}");
                Console.WriteLine($"codgi: bio      - {user.Bio}");
                Console.WriteLine($"codgi: jwt      - {user.Token[..20]}");
            } catch (Exception e) {
                Console.WriteLine($"codgi: 登陆失败 - {e.Message}");
            }

            Console.WriteLine();

            try {
                var note = Actions.GetNoteAsync("5fb922d85703375b1ccf28dc").GetAwaiter().GetResult();
                Console.WriteLine($"note: 获取成功");
                Console.WriteLine($"note: author  - {note.Author.Username}");
                Console.WriteLine($"note: title   - {note.Title}");
                Console.WriteLine($"note: content - {(note.Content.Length > 20 ? note.Content[..20] : note.Content)}...");
                Console.WriteLine($"note: public  - {note.Public}");
                Console.WriteLine($"note: tags - {string.Join(", ", note.Tags)}");
            } catch (Exception e) {
                Console.WriteLine($"note: 获取失败 - {e.Message}");
            }

            Console.WriteLine();

            try {
                var t = Actions.GetCommentsAsync("5fb922d85703375b1ccf28dc");
                t.Wait();
                var comments = t.Result;
                var comment = comments[0];
                Console.WriteLine($"comments: 获取成功");
                Console.WriteLine($"comments: author  - {comment.Author.Username}");
                Console.WriteLine($"comments: content - {(comment.Content.Length > 20 ? comment.Content[..20] : comment.Content)}...");
                Console.WriteLine($"comments: points  - {comment.Points}");
                Console.WriteLine($"comments: date    - {comment.Date:G}");
            } catch (Exception e) {
                Console.WriteLine($"comments: 获取失败 - {e.Message}");
            }
        }
    }
}
