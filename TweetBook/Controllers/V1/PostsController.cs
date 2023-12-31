using Microsoft.AspNetCore.Mvc;
using TweetBook.Contracts.V1;
using TweetBook.Contracts.V1.Requests;
using TweetBook.Contracts.V1.Responses;
using TweetBook.Domain;
using TweetBook.Services;

namespace TweetBook.Controllers.V1;

public class PostsController : Controller
{
    private readonly IPostService _postService;

    public PostsController(IPostService postService)
    {
        _postService = postService;
    }
    
    [HttpGet(ApiRoutes.Posts.GetAll)]
    public IActionResult GetAll()
    {
        return Ok(_postService.GetPosts());
    }

    [HttpGet(ApiRoutes.Posts.Get)]
    public IActionResult Get([FromRoute] Guid postId)
    {
        var post = _postService.GetPostById(postId);

        if (post == null)
        {
            return NotFound();
        }

        return Ok(post);
    }

    [HttpPost(ApiRoutes.Posts.Create)]
    public IActionResult Create([FromBody] CreatePostRequest postRequest)
    {
        var post = new Post { Id = postRequest.Id };
        
        if (postRequest.Id != Guid.Empty)
        {
            post.Id = Guid.NewGuid();
        }
        
        _postService.GetPosts().Add(post);

        var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
        var locationUri = baseUrl + "/" + ApiRoutes.Posts.Get.Replace("{postId}", postRequest.Id.ToString());

        var response = new PostResponse { Id = postRequest.Id };
        return Created(locationUri, response);
    }
    
    [HttpPut(ApiRoutes.Posts.Update)]
    public IActionResult Update([FromRoute] Guid postId, [FromBody] UpdatePostRequest request)
    {
        var post = new Post
        {
            Id = postId,
            Name = request.Name
        };

        var updated = _postService.UpdatePost(post);

        if (updated)
        {
            return Ok(post);
        }

        return NotFound();


    }

    [HttpDelete(ApiRoutes.Posts.Delete)]
    public IActionResult Delete([FromRoute] Guid postId)
    {
        var deleted = _postService.DeletePost(postId);

        if (deleted)
        {
            return Ok("Post deleted");
        }

        return NotFound();
    }
    
    
}