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
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private IMapper _mapper;

        public CommentService(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public Task<bool> Comment(CommentDTO dto)
        {
            var comment = _mapper.Map<Comment>(dto);
            return _commentRepository.Comment(comment);
        }

        public async Task<IEnumerable<CommentDTO>> GetCommentByArticleId(int articleId)
        {
            var comment = await _commentRepository.GetCommentByArticleId(articleId);
            return _mapper.Map<IEnumerable<CommentDTO>>(comment);
        }
    }
}
