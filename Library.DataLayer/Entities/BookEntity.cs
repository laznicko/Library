using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Library.DataLayer.Entities
{
    /// <summary>
    /// Represents a book entity for database
    /// </summary>
    public class BookEntity
    {
        /// <summary>
        /// Primary key for the book
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long BookID { get; set; }

        /// <summary>
        /// Public Id for the book
        /// </summary>
        public Guid BookPublicID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// The Internalional Standard Book Number
        /// </summary>
        [Required]
        public string ISBN { get; set; } = string.Empty;

        /// <summary>
        /// Title of the book
        /// </summary>
        [Required]
        public string BookTitle { get; set; } = string.Empty;

        /// <summary>
        /// Description of the book
        /// </summary>
        public string? BookDescription { get; set; }

        /// <summary>
        /// Publication date of the book
        /// </summary>
        public string? BookDate { get; set; }

        /// <summary>
        /// First name of the author
        /// </summary>
        public string? AuthorFirstName { get; set; }

        /// <summary>
        /// Last name of the author.
        /// </summary>
        public string? AuthorLastName { get; set; }
        
        /// <summary>
        /// Book borrow indicator
        /// </summary>
        public bool IsBorrowed { get; set; } = false;


        /// <summary>
        /// Navigation property for borrow
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<BorrowEntity>? Borrows { get; set; }
    }
}
