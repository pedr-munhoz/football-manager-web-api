namespace api.Models.Results;

public class ListResult<T>
{
    public ListResult(List<T> itens, int count)
    {
        Itens = itens;
        Count = count;
    }

    public List<T> Itens { get; set; } = new List<T>();
    public int Count { get; set; }
}
