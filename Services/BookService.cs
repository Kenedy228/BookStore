using Microsoft.AspNetCore.Mvc;
using WebApplication1.Infrastructure;
using WebApplication1.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;

namespace WebApplication1.Services
{
    public class BookService : IBookService
    {
        private readonly BookContext _dbContext;

        public BookService(BookContext dbContext)
        {
            _dbContext = dbContext;
        }

        private const int MIN_TITLE_LENGTH = 5;
        private const int MAX_TITLE_LENGTH = 50;
        public async Task<string> Add(Book book)
        {
            string errorMessage = await CheckBookOnErrors(book);

            if (errorMessage.Length == 0)
            {
                Book foundBook = await _dbContext.books.FirstOrDefaultAsync(b => b.Title == book.Title);

                if (foundBook == null)
                {
                    await _dbContext.books.AddAsync(book);
                    await _dbContext.SaveChangesAsync();
                } else
                {
                    errorMessage += "Книга с таким наименованием уже существует\n";
                }
            }

            return errorMessage;
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await _dbContext.books
                .AsNoTracking()
                .Select(b => b)
                .ToListAsync();
        }

        public async Task<bool> Remove(Guid id)
        {
            Book book = await _dbContext.books
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book != null)
            {
                _dbContext.books.Remove(book);
                await _dbContext.SaveChangesAsync();
                return true;
            } else
            {
                return false;
            }
        }

        public async Task<string> Update(Book book)
        {
            string errorMessage = await CheckBookOnErrors(book);

            if (errorMessage.Length == 0)
            {
                
                Book existingBook = await _dbContext.books
                    .FirstOrDefaultAsync(b => b.Id == book.Id);

                if (existingBook == null)
                {
                    errorMessage += "Произошла ошибка. Книга которую, вы хотитет отредактировать, не существует";
                    return errorMessage;
                }

                await _dbContext.SaveChangesAsync();
            }

            return errorMessage;
        }

        public async Task<string> CheckBookOnErrors(Book book)
        {
            string errorMessage = string.Empty;

            if (book.Title.Length < MIN_TITLE_LENGTH)
            {
                errorMessage += "Минимальная длина названия книги 5 символов\n";
            }

            if (book.Title.Length > MAX_TITLE_LENGTH)
            {
                errorMessage += "Максимальная длина названия книги 50 символов\n";
            }

            if (book.Price <= 0)
            {
                errorMessage += "Стоимость книги не может быть меньше 0 рублей\n";
            }

            return errorMessage;
        }
    }
}
