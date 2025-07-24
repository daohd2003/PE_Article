using AutoMapper;
using BussinessObjects;
using BussinessObjects.DTOs;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;

        public ArticleService(IArticleRepository articleRepository, IMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
        }

        public async Task<bool> CreateArticle(CreateArticleDTO dto)
        {
            var article = _mapper.Map<Article>(dto);
            return await _articleRepository.CreateArticle(article);
        }

        public async Task<bool> DeleteArticle(int id)
        {
            return await _articleRepository.DeleteArticle(id);
        }

        public async Task<ArticleDetailDTO?> GetArticleDetails(int id)
        {
            var article = await _articleRepository.GetArticleById(id);
            var dto = _mapper.Map<ArticleDetailDTO?>(article);
            return dto;
        }

        public IQueryable<Article> GetArticlesAsQueryable()
        {
            return _articleRepository.GetAllArticlesAsQueryable();
        }

        public async Task<IEnumerable<ArticleDTO>> GetArticlesForList()
        {
            var articles = await _articleRepository.GetArticles();
            var dtos = _mapper.Map<IEnumerable<ArticleDTO>>(articles);

            return dtos;
        }

        public async Task<bool> UpdateArticle(UpdateArticleDTO dto)
        {
            var article = _mapper.Map<Article>(dto);
            return await _articleRepository.UpdateArticle(article);
        }
    }
}
