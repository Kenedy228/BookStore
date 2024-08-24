using Microsoft.AspNetCore.Mvc;
using WebApplication1.Entities;

namespace WebApplication1.Infrastructure
{
    public interface IBookService
    {
        public Task<string> Add(Book book);
        public Task<IEnumerable<Book>> GetAll();
        public Task<bool> Remove(Guid id);
        public Task<string> Update(Book book);
        public Task<string> CheckBookOnErrors(Book book);
    }
}
