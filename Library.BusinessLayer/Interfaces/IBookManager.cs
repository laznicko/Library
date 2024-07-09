using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLayer.Interfaces
{
    public interface IBookManager
    {
        /// <summary>
        /// Get all books from database
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<BookModel>> GetAll();

        /// <summary>
        /// Get book by public id
        /// </summary>
        /// <param name="bookPublicID"></param>
        /// <returns></returns>
        Task<BookModel?> Get(Guid bookPublicID);

        /// <summary>
        /// Add a new book
        /// </summary>
        /// <param name="bookModel"></param>
        /// <returns></returns>
        Task Add(BookModel bookModel);

        /// <summary>
        /// Update the book
        /// </summary>
        /// <param name="bookModel"></param>
        /// <returns></returns>
        Task<BookModel> Update(BookModel bookModel);

        /// <summary>
        /// Remove book by public id
        /// </summary>
        /// <param name="bookPublicID"></param>
        /// <returns></returns>
        Task Delete(Guid bookPublicID);
    }
}
