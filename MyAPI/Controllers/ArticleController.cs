using BussinessObjects;
using BussinessObjects.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Services;

namespace MyAPI.Controllers
{
    [Route("api/articles")]
    [Authorize]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet("/odata/Articles")]
        [EnableQuery]
        public ActionResult<IQueryable<Article>> GetODataArticles()
        {
            var articles = _articleService.GetArticlesAsQueryable();
            return Ok(articles);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllArticles()
        {
            var articles = await _articleService.GetArticlesForList();
            return Ok(articles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllArticles(int id)
        {
            var articles = await _articleService.GetArticleDetails(id);
            return Ok(articles);
        }

        [HttpPost]
        public async Task<IActionResult> CreateArticle([FromBody] CreateArticleDTO dto)
        {
            var articles = await _articleService.CreateArticle(dto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateArticle([FromBody] UpdateArticleDTO dto)
        {
            var articles = await _articleService.UpdateArticle(dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var articles = await _articleService.DeleteArticle(id);
            return Ok();
        }
    }
}
