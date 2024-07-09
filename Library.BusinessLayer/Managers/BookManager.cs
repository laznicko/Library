using AutoMapper;
using Common.Models;
using Library.BusinessLayer.Interfaces;
using Library.Common.Exceptions;
using Library.DataLayer;
using Library.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Library.BusinessLayer.Managers
{
    /// <summary>
    /// Manage operations related to book
    /// </summary>
    public class BookManager : IBookManager
    {
        private readonly DBContext context;
        private readonly IMapper mapper;

        /// <summary>
        /// Initialization of new instance of book manager
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mapper"></param>
        public BookManager(DBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// Get all books from database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<BookModel>> GetAll()
        {
            var books = await context.Books.ToListAsync();
            return mapper.Map<IEnumerable<BookModel>>(books);
        }

        /// <summary>
        /// Get book by public id
        /// </summary>
        /// <param name="bookPublicID"></param>
        /// <returns></returns>
        public async Task<BookModel?> Get(Guid bookPublicID)
        {
            var book = await context.Books.FirstOrDefaultAsync(b => b.BookPublicID == bookPublicID);

            if (book == null)
            {
                throw new BookNotFoundException();
            }
            return mapper.Map<BookModel>(book);
        }

        /// <summary>
        /// Get book entity by public id
        /// </summary>
        /// <param name="bookPublicID"></param>
        /// <returns></returns>
        private async Task<BookEntity?> GeEntity(Guid bookPublicID)
        {
            return await context.Books.FirstOrDefaultAsync(b => b.BookPublicID == bookPublicID);
        }

        /// <summary>
        /// Get entity by book id
        /// </summary>
        /// <param name="bookID"></param>
        /// <returns></returns>
        private async Task<BookEntity?> GeEntity(long bookID)
        {
            return await context.Books.FirstOrDefaultAsync(b => b.BookID == bookID);
        }

        /// <summary>
        /// Add a new book
        /// </summary>
        /// <param name="bookModel"></param>
        public async Task Add(BookModel bookModel)
        {
            //Validate  the book model
            ValidateModel(bookModel);

            //Check if a book with the same public ID exists
            var existingBook = await context.Books.AnyAsync(b => b.BookPublicID == bookModel.BookPublicID);
            if (existingBook)
            {
                throw new InvalidOperationException("A book with the same public ID already exists.");
            }

            var bookEntity = mapper.Map<BookEntity>(bookModel);
            await context.Books.AddAsync(bookEntity);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Update the book
        /// </summary>
        /// <param name="bookModel"></param>
        /// <returns></returns>
        /// <exception cref="BookNotFoundException"></exception>
        public async Task<BookModel> Update(BookModel bookModel)
        {
            //Select a book entity from db
            var bookEntity = await context.Books.FirstOrDefaultAsync(b => b.BookPublicID == bookModel.BookPublicID);
            if (bookEntity == null)
            {
                throw new BookNotFoundException();
            }

            mapper.Map(bookModel, bookEntity);

            context.Books.Update(bookEntity);
            await context.SaveChangesAsync();

            return mapper.Map<BookModel>(bookEntity);
        }
        /// <summary>
        /// Remove book by public id
        /// </summary>
        /// <param name="bookPublicID"></param>
        public async Task Delete(Guid bookPublicID)
        {
            //Select a book entity from db
            var bookEntity = await context.Books.FirstOrDefaultAsync(b => b.BookPublicID == bookPublicID);
            if (bookEntity == null)
            {
                throw new BookNotFoundException();
            }

            context.Books.Remove(bookEntity);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Validates the book model
        /// </summary>
        /// <param name="bookModel"></param>
        /// <exception cref="ValidationException"></exception>
        private void ValidateModel(BookModel bookModel)
        {
            var validationContext = new ValidationContext(bookModel);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(bookModel, validationContext, validationResults, true);

            if (!isValid)
            {
                throw new ValidationException(string.Join("; ", validationResults.Select(r => r.ErrorMessage)));
            }
        }

        /// <summary>
        /// Saves changes into database
        /// </summary>
        private async Task SaveChangesAsync()
        {
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while saving changes to the database.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred.", ex);
            }
        }
    }
}

