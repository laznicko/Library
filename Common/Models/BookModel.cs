using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Common.Models
{
    /// <summary>
    /// Represents a book model
    /// </summary>
    public class BookModel
    {
        /// <summary>
        /// Public Id for the book
        /// </summary>
        public Guid BookPublicID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// The Internalional Standard Book Number
        /// </summary>
        [Required(ErrorMessage = "ISBN is Required")]
        public string ISBN { get; set; } = string.Empty;

        /// <summary>
        /// Title of the book
        /// </summary>
        [Required(ErrorMessage = "Book title is Required")]
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
        /// Full name of the author
        /// </summary>
        public string AuthorFullName => $"{AuthorFirstName} {AuthorLastName}";

        /// <summary>
        /// Navigation property for borrow
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<BorrowModel>? Borrows { get; set; }
    }
}
