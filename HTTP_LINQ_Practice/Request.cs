using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HTTP_LINQ_Practice
{
    static class Request
    {
        public static Dictionary<Post,int> GetCommentsInPost(int userId,List<User> users)
        {     
            var result = new Dictionary<Post,int>();
            var user = users.FirstOrDefault(u => u.Id == userId);
            user.Posts.ForEach(p => result.Add(p,p.Comments.Count));
            return result;
        }

        public static List<Comment> GetUserComments(int userId, List<User> users)
        {
            var user = users.FirstOrDefault(u => u.Id == userId);
            var result = from post in user.Posts
                let p = post.Comments
                from comment in p
                where comment.Body.Length > 50
                select comment;
            return result.ToList();
        }

        public static List<(int, string)> UserTODOdone(int userId, List<User> users)
        {
            var user = users.FirstOrDefault(u => u.Id == userId);
            var result = from toDo in user.ToDos
                where toDo.IsComplete == true
                select (toDo.Id, toDo.Name);
            return result.ToList();
        }
        //Получить список пользователей по алфавиту(по возрастанию) с отсортированными todo items по длине name(по убыванию)
        //Получить следующую структуру(передать Id пользователя в параметры)
        //User
        //    Последний пост пользователя(по дате)
        //Количество комментов под последним постом
        //    Количество невыполненных тасков для пользователя
        //Самый популярный пост пользователя(там где больше всего комментов с длиной текста больше 80 символов)
        //Самый популярный пост пользователя(там где больше всего лайков)
        //Получить следующую структуру(передать Id поста в параметры)
        //Пост
       //    Самый длинный коммент поста
        //    Самый залайканный коммент поста
        //    Количество комментов под постом где или 0 лайков или длина текста< 80
    }
}
