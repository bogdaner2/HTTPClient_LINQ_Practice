using System;

namespace HTTP_LINQ_Practice
{
    class Post
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }
        public int Likes { get; set; }
    }
}
