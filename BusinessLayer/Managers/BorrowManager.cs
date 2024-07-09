using AutoMapper;
using Common.Models;
using Library.BusinessLayer.Interfaces;
using Library.Common.Exceptions;
using Library.DataLayer;
using Library.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.BusinessLayer.Managers
{
    public class BorrowManager : IBorrowManager
    {
        private readonly DBContext context;
        private readonly IMapper mapper;

        /// <summary>
        /// Initialization of new instance of borrow manager
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mapper"></param>
        public BorrowManager(DBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// Get all borrows from database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<BorrowModel>> GetAll()
        {
            var borrows = await context.Borrows.ToListAsync();
            return mapper.Map<IEnumerable<BorrowModel>>(borrows);
        }

        /// <summary>
        /// Creates a new borrow
        /// </summary>
        /// <param name="userPublicID"></param>
        /// <param name="bookPublicID"></param>
        /// <returns></returns>
        /// <exception cref="UserNotFoundException"></exception>
        /// <exception cref="BookNotFoundException"></exception>
        /// <exception cref="BookAlreadyBorrowedException"></exception>
        public async Task<BorrowModel> Add(Guid userPublicID, Guid bookPublicID)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.UserPublicID == userPublicID);
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var book = await context.Books.FirstOrDefaultAsync(b => b.BookPublicID == bookPublicID);
            if (book == null)
            {
                throw new BookNotFoundException();
            }

            if (book.IsBorrowed)
            {
                var borrowRecord = await context.Borrows.FirstOrDefaultAsync(b => b.BookID == book.BookID && b.ReturnDate == null);
                if (borrowRecord != null)
                {
                    var borrowedByUser = await context.Users.FirstOrDefaultAsync(u => u.UserID == borrowRecord.UserID);
                    if (borrowedByUser != null)
                    {
                        throw new BookAlreadyBorrowedException($"Book is already borrowed by {borrowedByUser.FirstName} until {borrowRecord.PlannedReturnDate}");
                    }
                    else
                    {
                        throw new BookAlreadyBorrowedException($"Book is already borrowed until {borrowRecord.PlannedReturnDate}");
                    }
                }
            }

            book.IsBorrowed = true;
            context.Books.Update(book);

            var borrowEntity = new BorrowEntity
            {
                BookID = book.BookID,
                UserID = user.UserID,
                BorrowDate = DateTime.Now,
                PlannedReturnDate = DateTime.Now.AddMonths(1),
                BorrowPublicID = Guid.NewGuid(),
                Book = book,
                User = user
            };

            context.Borrows.Add(borrowEntity);
            await context.SaveChangesAsync();

            return mapper.Map<BorrowModel>(borrowEntity);
        }

        /// <summary>
        ///  Ends the borrow
        /// </summary>
        /// <param name="borrowPublicID"></param>
        /// <returns></returns>
        /// <exception cref="BorrowNotFoundException"></exception>
        /// <exception cref="BookNotFoundException"></exception>
        public async Task ConfirmReturn(Guid borrowPublicID)
        {
            // Check if borrow exists
            var borrowEntity = await context.Borrows.FirstOrDefaultAsync(b => b.BorrowPublicID == borrowPublicID);
            if (borrowEntity == null)
            {
                throw new BorrowNotFoundException();
            }

            // Set the date of return for now
            borrowEntity.ReturnDate = DateTime.UtcNow;

            // Check if book exists
            var book = await context.Books.FirstOrDefaultAsync(b => b.BookID == borrowEntity.BookID);
            if (book == null)
            {
                throw new BookNotFoundException();
            }

            // Set that book is not borrowed
            book.IsBorrowed = false;
            context.Books.Update(book);

            context.Borrows.Update(borrowEntity);
            await context.SaveChangesAsync();
        }
    }
}
