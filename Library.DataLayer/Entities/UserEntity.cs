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
    public class UserEntity
    {
        /// <summary>
        /// Primary key for the user
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserID { get; set; }

        /// <summary>
        /// Public Id for the user
        /// </summary>
        public Guid UserPublicID { get; set; } = Guid.NewGuid();

        /// <summary>
        /// First name of the user
        /// </summary>
        [Required]
        public string FirstName { get; set; } = string.Empty;
       
        /// <summary>
        /// Last name of the user
        /// </summary>
        [Required] 
        public string LastName { get; set;} = string.Empty;

        /// <summary>
        /// Email of the user
        /// </summary>
        [Required]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Navigation property for borrow
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<BorrowEntity>? Borrows { get; set; }
    }
}
