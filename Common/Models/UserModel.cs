using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class UserModel
    {
        /// <summary>
        /// Public Id for the user
        /// </summary>
        public virtual Guid? UserPublicID { get; set; }

        /// <summary>
        /// First name of the user
        /// </summary>
        public virtual string? FiratName { get; set; }

        /// <summary>
        /// Last name of the user
        /// </summary>
        public virtual string? LastName { get; set; }

        /// <summary>
        /// Email of the user
        /// </summary>
        public virtual string? Email { get; set; }

        /// <summary>
        /// User borrows
        /// </summary>
        public virtual ICollection<BorrowModel>? Borrows { get; set; }
    }
}
