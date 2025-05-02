namespace Monitor.Infrastructure.DTO
{
    public class AppDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string? Descricao { get; set; }
        public string LinkInteracao { get; set; }
        public DateTime? DataCriacao { get; set; }
    }
}
