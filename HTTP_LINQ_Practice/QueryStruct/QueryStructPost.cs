namespace HTTP_LINQ_Practice
{
    class QueryStructPost
    {
        public Post Post { get; set; }
        public int CountComments { get; set; }
        public Comment LongestComment{ get; set; }
        public Comment MostLikedComment { get; set; }

        public QueryStructPost(
            Post post,          
            Comment longestComment,
            Comment mostLikedComment,
            int countComments)
        {
            Post = post;
            LongestComment= longestComment;
            MostLikedComment = mostLikedComment;
            CountComments = countComments;
        }
    }
}
