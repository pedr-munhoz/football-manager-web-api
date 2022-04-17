namespace api.Services;

public class ServiceResult
{
    public ServiceResult(bool success, ServiceError? error = null)
    {
        Success = success;
        Error = error;
    }

    public bool Success { get; set; }
    public ServiceError? Error { get; set; }
}

public class ServiceResult<T> : ServiceResult
{
    public ServiceResult(T content) : base(success: true)
    {
        Content = content;
    }

    public ServiceResult(ServiceError error) : base(success: false, error)
    {
    }

    public T? Content { get; set; }
}

public class ServiceListResult<T> : ServiceResult
{
    public ServiceListResult(List<T> content, long count) : base(success: true)
    {
        Content = content;
        Count = count;
    }

    public ServiceListResult(ServiceError error) : base(success: false, error)
    {
    }

    public List<T>? Content { get; set; }
    public long Count { get; set; }
}
