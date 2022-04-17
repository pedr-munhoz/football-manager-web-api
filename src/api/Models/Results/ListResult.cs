namespace api.Models.Results;

public class ListResult<T>
{
    public ListResult(IEnumerable<T>? itens, long count)
    {
        Itens = itens ?? new List<T>();
        Count = count;
    }

    public IEnumerable<T> Itens { get; set; } = new List<T>();
    public long Count { get; set; }
}
