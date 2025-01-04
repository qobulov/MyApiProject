using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using MyApiProject.Dtos.Comment;
using MyApiProject.Model;

namespace MyApiProject.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment commentModel)
        {
            return new CommentDto
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedAt = commentModel.CreatedAt,
                StockId = commentModel.StockId
            };
        }

        public static Comment ToCommentFromCreateDto(this CreateCommentRequestDto commentRequestDto,int stockId)
        {
            return new Comment
            {
                Title = commentRequestDto.Title,
                Content = commentRequestDto.Content,
                StockId = stockId
            };
        }

        public static Comment ToCommentFromUpdate(this UpdateCommentRequestDto commentDto, int stockId)
        {
            return new Comment
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
                StockId = stockId
            };
        }
    }
}