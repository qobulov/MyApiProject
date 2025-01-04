using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApiProject.Data;
using MyApiProject.Dtos.Comment;
using MyApiProject.Dtos.Stock;
using MyApiProject.Interfaces;
using MyApiProject.Mappers;

namespace MyApiProject.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController: ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        public CommentController(ICommentRepository commentRepo,IStockRepository stockRepo)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task <IActionResult> GetAll()
        {
            var comments = await _commentRepo.GetAllAsync();
            var commentsDto = comments.Select(s => s.ToCommentDto());
            return Ok(commentsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _commentRepo.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentRequestDto commentDto)
        {
            if(!await _stockRepo.StockExists(stockId))
            {
                return BadRequest("Stock not found");
            }

            var commentModel = commentDto.ToCommentFromCreateDto(stockId);
            await _commentRepo.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById),new {id = commentModel}, commentModel.ToCommentDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _commentRepo.UpdateAsync(id, updateDto.ToCommentFromUpdate(id));

            if (comment == null)
            {
                return NotFound("Comment not found");
            }

            return Ok(comment.ToCommentDto());
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var commenModel = await _commentRepo.DeleteAsync(id);

            if (commenModel == null)
            {
                return NotFound("Comment not found");
            }
            return Ok(commenModel);
        }
    }
}