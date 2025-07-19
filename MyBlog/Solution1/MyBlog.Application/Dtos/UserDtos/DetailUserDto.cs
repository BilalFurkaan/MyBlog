using MyBlog.Application.Dtos.ArticleDtos;
using MyBlog.Application.Dtos.CommentDtos;

namespace MyBlog.Application.Dtos.UserDtos;

public class DetailUserDto : ResultUserDto
{
    public List<ResultArticleDto> Articles { get; set; } = new();
    public List<ResultCommentDto> Comments { get; set; } = new();
}