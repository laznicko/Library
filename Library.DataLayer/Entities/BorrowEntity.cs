using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataLayer.Entities
{
    /// <summary>
    /// Represents borrow in database
    /// </summary>
    public class BorrowEntity
    {
        /// <summary>
        /// Primary key for the borrow
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long BorrowID { get; set; }

        /// <summary>
        /// Borrow public Id
        /// </summary>
        public Guid BorrowPublicID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Id for the book
        /// </summary>
        public long BookID { get; set; }

        /// <summary>
        /// Id for the user
        /// </summary>
        public long UserID { get; set; }

        /// <summary>
        /// Date when borrowing starded
        /// </summary>
        public DateTime BorrowDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Date of return
        /// </summary>
        public DateTime? ReturnDate { get; set; }

        /// <summary>
        /// Expected date of return
        /// </summary>
        public DateTime  PlannedReturnDate { get; set; }

        /// <summary>
        /// Navigation property for book
        /// </summary>
        [ForeignKey("BookID")]
        public virtual BookEntity? Book { get; set; }

        /// <summary>
        /// Navigation property for user
        /// </summary>
        [ForeignKey("UserID")]
        public virtual UserEntity? User { get; set; }
    }
}
