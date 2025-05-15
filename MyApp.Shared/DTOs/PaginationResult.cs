namespace MyApp.Shared.DTOs
{
    public class PaginationResult<T> where T : class
    {
        public int RowsCount { get; set; } // cuantos elementos hay en total
        public int PageCount { get; set; } // cuanta spaginas va a tener
        public int PageSize { get; set; } // cuantos datos tiene cada pagina
        public int CurrentPage { get; set; } // pagina actual
        public IEnumerable<T> Results { get; set; } = []; // datos de la pagina actual
    }
}