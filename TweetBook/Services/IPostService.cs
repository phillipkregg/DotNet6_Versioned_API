using TweetBook.Domain;

namespace TweetBook.Services;

public interface IPostService
{
    List<Post> GetPosts();
    
    Post? GetPostById(Guid postId);

    bool UpdatePost(Post postToUpdate);

    bool DeletePost(Guid postId);
}