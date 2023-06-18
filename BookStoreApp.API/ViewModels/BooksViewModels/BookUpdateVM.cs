namespace BookStoreApp.API.ViewModels.BooksViewModels
{
    public class BookUpdateVM
    {
        public string? Title { get; set; }

        public int? Year { get; set; }

        public string? Summary { get; set; }

        public string? Image { get; set; }

        public decimal? Price { get; set; }
    }
}
