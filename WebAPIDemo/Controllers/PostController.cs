using Application.Posts.Commands;
using Application.Posts.Queries;
using Microsoft.AspNetCore.Mvc;
using WebAPIDemo.Filters;

namespace WebAPIDemo.Controllers;

[Route("/api/posts/")]
public class PostController : BaseController<PostController>
{
    [HttpGet]
    public ActionResult<List<Post>> GetAllPosts()
        => Ok(Mediator.Send(new GetAllPosts()));
    

    [HttpGet]
    [Route("/api/posts/{id}", Name = "GetPostById")]
    public async Task<ActionResult<Post>> GetPostById(int id)
        => Ok( await Mediator.Send(new GetPostById() {Id = id}));
    

    [HttpDelete]
    [Route("/api/posts/{id}")]
    public async Task<ActionResult> DeletePostById(int id)
    {
        await Mediator.Send(new DeletePost() {Id = id});
        Logger.LogInformation("Post deleted {Id}", id);
        return NoContent();
    }

    [HttpPost]
    [ModelStateValidation]
    public async Task<ActionResult<Post>> CreatePost([FromBody] CreatePost newPost)
    {
        Post post = await Mediator.Send(newPost);
        Logger.LogInformation("Post created");
        return CreatedAtRoute(nameof(GetPostById), new {Id = post.Id}, post);
    }

    [HttpPut]
    [Route("/api/posts/{id}")]
    [ModelStateValidation]
    public async Task<ActionResult<Post>> UpdatePost(int id, [FromBody] UpdatePost updatePost)
    {
        updatePost.PostId = id;
        Post post = await Mediator.Send(updatePost);
        Logger.LogInformation("Post updated");
        return Ok(post);
    }

    
}