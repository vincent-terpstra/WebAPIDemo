using Application.Posts.Commands;
using Application.Posts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Npgsql.PostgresTypes;
using WebAPIDemo.Filters;

namespace WebAPIDemo.Controllers;

[Route("/api/posts/")]
public class PostController : BaseController<PostController>
{
    [HttpGet]
    public ActionResult<List<Post>> GetAllPosts()
    {
        return Ok(Mediator.Send(new GetAllPosts()));
    }

    [HttpGet]
    [Route("/api/posts/{id}", Name = "GetPostById")]
    public async Task<ActionResult<Post>> GetPostById(int id)
    {
        try
        {
            Post? post = await Mediator.Send(new GetPostById() {Id = id});
            return Ok(post);
        }
        catch (KeyNotFoundException)
        {
            Logger.LogError("Unable to find Post {Id}", id);
            return NotFound();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Unable to get Post {Id}", id);
            return StatusCode(500);
        }
    }

    [HttpDelete]
    [Route("/api/posts/{id}")]
    public async Task<ActionResult> DeletePostById(int id)
    {
        try
        {
            await Mediator.Send(new DeletePost() {Id = id});
            Logger.LogInformation("Post deleted {Id}", id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            Logger.LogWarning("Unable to delete {Id} Not Found ", id);
            return NotFound();
        }
    }

    [HttpPost]
    [ModelStateValidation]
    public async Task<ActionResult<Post>> CreatePost([FromBody] CreatePost newPost)
    {
        try
        {
            Post post = await Mediator.Send(newPost);
            Logger.LogInformation("Post created");
            return CreatedAtRoute(nameof(GetPostById), new {Id = post.Id}, post);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Unable to create post");
            return StatusCode(500);
        }
    }

    [HttpPut]
    [Route("/api/posts/{id}")]
    [ModelStateValidation]
    public async Task<ActionResult<Post>> UpdatePost(int id, [FromBody] UpdatePost updatePost)
    {
        try
        {
            updatePost.PostId = id;
            Post post = await Mediator.Send(updatePost);
            Logger.LogInformation("Post updated");
            return Ok(post);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex,"Unable to update post");
            return StatusCode(500);
        }
    }
}