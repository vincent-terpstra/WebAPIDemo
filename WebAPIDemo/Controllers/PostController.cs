using Application.Posts.Commands;
using Application.Posts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIDemo.Controllers;

[Route("/api/posts/")]
public class PostController : ControllerBase
{
    private readonly IMediator _mediator;

    public PostController(IMediator mediator)
    {
        _mediator = mediator;
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
        Post? post = await _mediator.Send(new GetPostById() {Id = id});
        return post is null ? NotFound() : Ok(post);
    }

    [HttpDelete]
    [Route("/api/posts/{id}")]
    public async Task<ActionResult> DeletePostById(int id)
    {
        try
        {
            await _mediator.Send(new DeletePost() {Id = id});
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        
    }

    [HttpPost]
    public async Task<ActionResult<Post>> CreatePost([FromBody]CreatePost newPost)
    {
        Post post = await _mediator.Send(newPost);
        return CreatedAtRoute(nameof(GetPostById), new {Id = post.Id}, post);
    }

    [HttpPut]
    [Route("/api/posts/{id}")]
    public async Task<ActionResult<Post>> UpdatePost(int id, [FromBody]UpdatePost updatePost)
    {
        updatePost.PostId = id;
        Post post = await _mediator.Send(updatePost);
        return Ok(post);
    }
}