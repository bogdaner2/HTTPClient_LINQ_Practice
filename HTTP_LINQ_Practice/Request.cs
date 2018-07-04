using System.Collections.Generic;
using System.Linq;

namespace HTTP_LINQ_Practice
{
     static class Request
     {
        public static List<User> Users { get; set; }
        public static List<(Post,int)> GetCommentsInPost(int userId)
        {     
            var result = new List<(Post,int)>();
            Users
                .FirstOrDefault(u => u.Id == userId)?
                .Posts
                .ForEach(p => result.Add((p, p.Comments.Count)));
            return result;
        }
        public static List<Comment> GetUserComments(int userId)
        {
            var user = Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                var result = from post in user.Posts
                    let p = post.Comments
                    from comment in p
                    where comment.Body.Length < 50
                    select comment;
                return result.ToList();
            }
            return null;
        }
        public static List<(int, string)> UserTodoDone(int userId)
        {
            var user = Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                var result = from toDo in user.ToDos
                    where toDo.IsComplete == true
                    select (toDo.Id, toDo.Name);
                return result.ToList();
            }        
            return null;
        }
        public static List<User> UserSortByTodo()
        {
            var result = Users.OrderBy(u => u.Name)
                .Select(x => { x.ToDos =
                x.ToDos.OrderByDescending(t => t.Name.Length).ToList();
                return x;
            });
        return result.ToList();
        }
        public static QueryStructUser GetStruct_User(int userId)
        {
            var user = Users.FirstOrDefault(u => u.Id == userId);
            var lastPost = user?.Posts
                .Where(post => post.CreatedAt == user.Posts.Max(p => p.CreatedAt))
                .FirstOrDefault();
            if (lastPost == null) return null;
            var countPostComment = lastPost.Comments.Count;
            var nonCompleteTasks = user.ToDos.Where(t => t.IsComplete == false).ToList().Count;
            var popularPostLike = user.Posts.OrderByDescending(p => p.Likes).FirstOrDefault();
            var popularPostComm = user.Posts
                .Select(p =>
            {
                p.Comments = p.Comments.Where(c => c.Body.Length > 80).ToList();
                return p;
            })
                .OrderByDescending(p => p.Comments.Count)
                .FirstOrDefault();
            return new QueryStructUser(user,lastPost,countPostComment,nonCompleteTasks,popularPostLike,popularPostComm); 
        }
        public static QueryStructPost GetStruct_Post(int postId)
        {
            var post = Users.SelectMany(x => x.Posts).FirstOrDefault(p => p.Id == postId);
            if (post == null) return null;
            var longestComment = post.Comments.OrderByDescending(c => c.Body.Length).FirstOrDefault();
            var mostLikestComment = post.Comments.OrderByDescending(c => c.Likes).FirstOrDefault();
            var commCount = post.Comments.Where(c => c.Likes == 0 || c.Body.Length < 80).ToList().Count;
            return new QueryStructPost(post,longestComment,mostLikestComment,commCount);
        }
    }
}
