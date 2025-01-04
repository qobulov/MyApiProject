using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyApiProject.Model;

namespace MyApiProject.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment> CreateAsync(Comment commenModel);
        Task<Comment> UpdateAsync(int id, Comment commenModel);
        Task<Comment> DeleteAsync(int id);
    }
}