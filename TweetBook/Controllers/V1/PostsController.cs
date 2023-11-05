using Microsoft.AspNetCore.Mvc;
using TweetBook.Contracts.V1;
using TweetBook.Contracts.V1.Requests;
using TweetBook.Contracts.V1.Responses;
using TweetBook.Domain;

namespace TweetBook.Controllers.V1;

public class PostsController : Controller
{
    private List<Post> _posts;

    public PostsController()
    {
        _posts = new List<Post>();
        for (int i = 0; i < 5; i++)
        {
            _posts.Add(new Post
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Post Name"
            });
        }
    }
    
    [HttpGet(ApiRoutes.Posts.GetAll)]
    public IActionResult GetAll()
    {
        return Ok(_posts);
    }

    [HttpPost(ApiRoutes.Posts.Create)]
    public IActionResult Create([FromBody] CreatePostRequest postRequest)
    {
        var post = new Post { Id = postRequest.Id };
        
        if (string.IsNullOrEmpty(postRequest.Id))
        {
            postRequest.Id = Guid.NewGuid().ToString();
            _posts.Add(new Post { Id = Guid.NewGuid().ToString() });
        }

        var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
        var locationUri = baseUrl + "/" + ApiRoutes.Posts.Get.Replace("{postId}", postRequest.Id);

        var response = new PostResponse { Id = postRequest.Id };
        return Created(locationUri, response);
    }
}