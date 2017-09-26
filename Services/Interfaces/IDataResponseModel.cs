namespace Service.Interfaces
{
    public interface IDataResponseModel
    {
        bool Success { get; set; }
        object Data { get; set; }
    }
}
