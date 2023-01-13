using Application.Posts.Commands;
using Application.Posts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIDemo.Controllers;

[Route("/api/posts/")]
public class PostController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PostController> _logger;

    public PostController(IMediator mediator, ILogger<PostController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<List<Post>> GetAllPosts()
    {
        return Ok(_mediator.Send(new GetAllPosts()));
    }

    [HttpGet]
    [Route("/api/posts/{id}", Name = "GetPostById")]
    public async Task<ActionResult<Post>> GetPostById(int id)
    {
        try
        {
            Post? post = await _mediator.Send(new GetPostById() {Id = id});
            return Ok(post);
        }
        catch (KeyNotFoundException keyNotFoundException)
        {
            _logger.LogError("Unable to find Post {Id}", id);
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unable to get Post {Id}", id);
            return StatusCode(500);
        }
    }

    [HttpDelete]
    [Route("/api/posts/{id}")]
    public async Task<ActionResult> DeletePostById(int id)
    {
        try
        {
            await _mediator.Send(new DeletePost() {Id = id});
            _logger.LogInformation("Post deleted {Id}", id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            _logger.LogWarning("Unable to delete {Id} Not Found ", id);
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<ActionResult<Post>> CreatePost([FromBody] CreatePost newPost)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            Post post = await _mediator.Send(newPost);
            _logger.LogInformation("Post created");
            return CreatedAtRoute(nameof(GetPostById), new {Id = post.Id}, post);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unable to create post");
            return StatusCode(500);
        }
    }

    [HttpPut]
    [Route("/api/posts/{id}")]
    public async Task<ActionResult<Post>> UpdatePost(int id, [FromBody] UpdatePost updatePost)
    {
        if (!ModelState.IsValid) return BadRequest();

        try
        {
            updatePost.PostId = id;
            Post post = await _mediator.Send(updatePost);
            _logger.LogInformation("Post updated");
            return Ok(post);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,"Unable to update post");
            return StatusCode(500);
        }
    }
}