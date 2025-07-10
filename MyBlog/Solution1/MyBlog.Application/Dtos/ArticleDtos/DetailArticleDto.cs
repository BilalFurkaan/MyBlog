using MyBlog.Application.Dtos.CommentDtos;

namespace MyBlog.Application.Dtos.ArticleDtos;

public class DetailArticleDto : ResultArticleDto
{
    public string AuthorUserName { get; set; }
    public string AuthorJob { get; set; }
    public string AuthorAbout { get; set; }
    public List<ResultCommentDto> Comments { get; set; } = new();
}