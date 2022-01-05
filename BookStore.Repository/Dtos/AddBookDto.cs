 using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Repository.Dtos
{
    public class AddBookDto
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
