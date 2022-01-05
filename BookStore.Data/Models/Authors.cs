using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Data.Models
{
    public class Authors
    {
        [Key]
        public int AuthorId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
    }
}
