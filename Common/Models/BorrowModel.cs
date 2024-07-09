using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    /// <summary>
    /// Represents a borrow model
    /// </summary>
    public class BorrowModel
    {
        /// <summary>
        /// Public Id for the book
        /// </summary>
        public Guid BorrowPublicID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Public Id for the User
        /// </summary>
        [Required]
        public Guid UserPublicID { get; set; }

        /// <summary>
        /// Public Id for the book
        /// </summary>
        [Required]
        public Guid BookPublicID { get; set; }

        /// <summary>
        /// Date when borrowing starded
        /// </summary>
        [Required]
        public DateTime BorrowDate { get; set; }

        /// <summary>
        /// Expected date of return
        /// </summary>
        [Required]
        public DateTime PlannedReturnDate { get; set; }

        /// <summary>
        /// Date of return
        /// </summary>
        public DateTime? ReturnDate { get; set; } = null;

        /// <summary>
        /// Borrowed book
        /// </summary>
        public BookModel? Book { get; set; }

        /// <summary>
        /// User
        /// </summary>
        public UserModel? User { get; set; }
    }
}
