using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyApiProject.Data;
using MyApiProject.Interfaces;
using MyApiProject.Model;

namespace MyApiProject.Repository
{
    public class CommentRepository: ICommentRepository
    {
        private readonly ApplicationDBContext _context;
        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateAsync(Comment commenModel)
        {
            await _context.Comment.AddAsync(commenModel);
            await _context.SaveChangesAsync();
            return commenModel;
        }

        public async Task<Comment> DeleteAsync(int id)
        {
            var commenModel = await _context.Comment.FirstOrDefaultAsync(c => c.Id == id);

            if (commenModel == null)
            {
                return null;
            }
            _context.Comment.Remove(commenModel);
            return commenModel;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comment.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comment.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Comment?> UpdateAsync(int id, Comment commentModel)
        {
            var existingComment = await _context.Comment.FindAsync(id);

            if (existingComment == null)
            {
                return null;
            }

            existingComment.Title = commentModel.Title;
            existingComment.Content = commentModel.Content;

            await _context.SaveChangesAsync();

            return existingComment;
        }
    }
}