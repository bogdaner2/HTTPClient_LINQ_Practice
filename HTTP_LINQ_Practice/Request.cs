using System;
using System.Collections.Generic;
using System.Linq;

namespace HTTP_LINQ_Practice
{
     static class Request
     {
        public static List<User> Users { get; set; }
        public static List<Post> Posts { get; set; }
        public static List<(Post,int)> GetCommentsInPost(int userId)
        {     
            var result = new List<(Post,int)>();
            var user = Users.FirstOrDefault(u => u.Id == userId);
            user?.Posts.ForEach(p => result.Add((p,p.Comments.Count)));
            return result;
        }

        public static List<Comment> GetUserComments(int userId)
        {
            var user = Users.FirstOrDefault(u => u.Id == userId);
            var result = from post in user?.Posts
                let p = post.Comments
                from comment in p
                where comment.Body.Length > 50
                select comment;
            return result.ToList();
        }

        public static List<(int, string)> UserTODOdone(int userId)
        {
            var user = Users.FirstOrDefault(u => u.Id == userId);
            var result = from toDo in user.ToDos
                where toDo.IsComplete == true
                select (toDo.Id, toDo.Name);
            return result.ToList();
        }

        public static List<User> UserSortByTODO()
        {
            var result = Users.OrderBy(u => u.Name)
                .ToList()
                .Select(x => { x.ToDos =
                x.ToDos.OrderByDescending(t => t.Name.Length).ToList();
                return x;
            });
        return result.ToList();
        }

        public static (User,Post,int,int,Post,Post) GetStruct_User(int userId)
        {
            var user = Users.FirstOrDefault(u => u.Id == userId);
            var lastPost = (Post) from post in user?.Posts
                where post.CreatedAt == user?.Posts.Max(p => p.CreatedAt)
                select post;
            var countPostComment = lastPost.Comments.Count;
            var nonCompleteTasks = user.ToDos.Where(t => t.IsComplete == false).ToList().Count;
            var popularPostLike = user.Posts.OrderByDescending(p => p.Likes).First();
            var popularPostComm = user.Posts.Select(p =>
            {
                p.Comments = p.Comments.Where(c => c.Body.Length > 80).ToList();
                return p;
            }).OrderByDescending(p => p.Comments.Count).First();
            return ( user,lastPost,countPostComment,nonCompleteTasks,popularPostLike,popularPostComm); 
        }

        public static (Post,Comment,Comment,int) GetStruct_Post(int postId)
        {
            var post = Posts.FirstOrDefault(p => p.Id == postId);
            if (post == null)
            {
                throw new Exception("No such Post");
            }
            var longestComment = post.Comments.OrderByDescending(c => c.Body.Length).FirstOrDefault();
            var likestComment = post.Comments.OrderByDescending(c => c.Likes).FirstOrDefault();
            var commCount = post.Comments.Where(c => c.Likes == 0 || c.Body.Length < 80).ToList().Count;
            return (post,longestComment,likestComment,commCount);
        }
    }
}
