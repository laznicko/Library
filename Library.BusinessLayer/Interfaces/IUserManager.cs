using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLayer.Interfaces
{
    /// <summary>
    /// Interface for user manager
    /// </summary>
    public interface IUserManager
    {
        /// <summary>
        /// Get all users from database
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<UserModel>> GetAll();
    }
}
