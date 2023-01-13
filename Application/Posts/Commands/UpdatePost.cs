using System.ComponentModel.DataAnnotations;
using Domain.Models;
using MediatR;

namespace Application.Posts.Commands;

public class UpdatePost : IRequest<Post>
{ 
    public int PostId { get; set; }
    
    [StringLength(20, ErrorMessage = "Post content must be less then {1} characters", MinimumLength = 1)]
    public string PostContent { get; set; }
}