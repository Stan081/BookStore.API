using System;
namespace BookStore.Repository.Dtos
{
    public class UpdateBookDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}
