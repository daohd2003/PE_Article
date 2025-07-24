using BussinessObjects.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace MyAPI.Controllers
{
    [Route("api/comments")]
    [Authorize]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(CommentDTO dto)
        {
            var comments = await _commentService.Comment(dto);
            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComments(int id)
        {
            var comments = await _commentService.GetCommentByArticleId(id);
            return Ok(comments);
        }
    }
}
