namespace TweetBook.Contracts.V1;

public static class ApiRoutes
{
    private const string Root = "api";
    private const string Version = "v1";
    private const string Base = $"{Root}/{Version}";
    
    public static class Posts
    {
        public const string GetAll = $"{Base}/posts";
        public const string Get = Base + "/posts/{postId}";
        public const string Create = $"{Base}/create";
        public const string Update = Base + "/posts/{postId}";
        public const string Delete = Base + "/posts/{postId}";
    }
}