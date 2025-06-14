namespace LMSWebApi.Models.DTO
{
    public record CommonPrams
    {
        public int skip { get; init; } = 0;
        public int take { get; init; } = 10;
    }
}
