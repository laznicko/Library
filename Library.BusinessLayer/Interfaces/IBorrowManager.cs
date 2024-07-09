using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLayer.Interfaces
{
    /// <summary>
    /// Interface for borrow manager
    /// </summary>
    public interface IBorrowManager
    {
        /// <summary>
        /// Get all borrows from database
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<BorrowModel>> GetAll();

        /// <summary>
        /// Creates a new borrow
        /// </summary>
        /// <param name="userPublicID"></param>
        /// <param name="bookPubliciD"></param>
        /// <returns></returns>
        Task<BorrowModel> Add(Guid userPublicID, Guid bookPubliciD);

        /// <summary>
        /// Ends the borrow
        /// </summary>
        /// <param name="borrowId"></param>
        /// <returns></returns>
        Task ConfirmReturn(Guid borrowId);
    }
}
