namespace WebApiPosRio.Models.ViewModels
{
    public class Filters
    {
        public int Page { get; set; } = 1;
        public int PerPage { get; set; } = 10;
        public string FechaI { get; set; } = string.Empty;
        public string FechaF { get; set; } = string.Empty;
    }

}
