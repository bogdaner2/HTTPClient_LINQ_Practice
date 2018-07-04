﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HTTP_LINQ_Practice
{
    class Menu
    {
        public Menu()
        {
            Console.WriteLine("Loading...");
            LoadData();
            Console.Clear();
        }
        public void GetMenu()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Select request and enter its number : \n" +
                                  "1) Show count of comments in post \n" +
                                  "2) Show user comments which less than 50 characters\n" +
                                  "3) Show user's todo which are done\n" +
                                  "4) Show sorted users by name with descending sort of comments length \n" +
                                  "5) Get struct by user id \n" +
                                  "6) Get struct by post id ");
                int.TryParse(Console.ReadLine(), out int choise);
                switch (choise)
                {
                    case 1:
                        Console.Clear();
                        GetCommentsInPost();
                        break;
                    case 2:
                        Console.Clear();
                        GetUserComments();
                        break;
                    case 3:
                        Console.Clear();
                        UserToDoDone();
                        break;
                    case 4:
                        Console.Clear();
                        UserSortByTodo();
                        break;
                    case 5:
                        Console.Clear();
                        GetStructUser();
                        break;
                    case 6:
                        Console.Clear();
                        GetStructPost();
                        break;
                    default:
                        throw new Exception("Incorrect data.Please,try it again");
                }
                Console.WriteLine("Please, for exit enter ESCAPE and to continue - any other key ");
            }
            while (Console.ReadKey().Key != ConsoleKey.Escape);
            Environment.Exit(0);
    }

        private void LoadData()
        {
            var users = DownloadDataAsync<List<User>>("https://5b128555d50a5c0014ef1204.mockapi.io/users").Result;
            var posts = DownloadDataAsync<List<Post>>("https://5b128555d50a5c0014ef1204.mockapi.io/posts").Result;
            var todos = DownloadDataAsync<List<ToDo>>("https://5b128555d50a5c0014ef1204.mockapi.io/todos").Result;
            var comments = DownloadDataAsync<List<Comment>>("https://5b128555d50a5c0014ef1204.mockapi.io/comments").Result;
            posts.ForEach(p => p.Comments = comments.Where(c => p.Id == c.Id).ToList());
            users.ForEach(user => user.Posts = posts.Where(x => user.Id == x.UserId).ToList());
            users.ForEach(user => user.ToDos = todos.Where(x => user.Id == x.UserId).ToList());
            Request.Users = users;
            Request.Posts = posts;
        }
        private async Task<T> DownloadDataAsync<T>(string url)
        {
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(url))
            using (HttpContent content = response.Content)
            {
                string responsJson = await content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responsJson);
            }
        }
        private void GetCommentsInPost()
        {
            Console.WriteLine("Input Id");
            int.TryParse(Console.ReadLine(), out int id);
            var result = Request.GetCommentsInPost(id);
            if (result.Count == 0)
            {
                throw new Exception("No posts or comments");
            }
            foreach (var res in result)
            {
                Console.WriteLine($"Post : {res.Item1.Title} - Count of comments : {res.Item2}");
            }
        }
        private void GetUserComments()
        {
            Console.WriteLine("Input Id");
            int.TryParse(Console.ReadLine(), out int id);
            var result = Request.GetUserComments(id);
            foreach (var res in result)
            {
                Console.WriteLine($"Comment : {res.Body}");
            }
        }
        private void UserSortByTodo()
        {
            var result = Request.UserSortByTODO();
            foreach (var res in result)
            {
                Console.WriteLine($"Name : {res.Name}");
                if (res.ToDos.Count == 0)
                {
                    Console.WriteLine("No ToDo");
                    continue;
                }
                foreach (var com in res.ToDos)
                {
                    Console.WriteLine($"ToDo --- {com.Name}");
                }
            }
        }
        private void UserToDoDone()
        {
            Console.WriteLine("Input Id");
            int.TryParse(Console.ReadLine(), out int id);
            var result = Request.UserTODOdone(id);
            foreach (var res in result)
            {
                Console.WriteLine($"Id : {res.Item1} - Name : {res.Item2}");
            }
        }
        private void GetStructUser()
        {
            Console.WriteLine("Input Id");
            int.TryParse(Console.ReadLine(), out int id);
            var result = Request.GetStruct_User(id);
            Console.WriteLine(result.Item1);
            Console.WriteLine(result.Item2);
            Console.WriteLine(result.Item3);
            Console.WriteLine(result.Item4);
            Console.WriteLine(result.Item5);
            Console.WriteLine(result.Item6);
        }

        private void GetStructPost()
        {

        }
    }
}
