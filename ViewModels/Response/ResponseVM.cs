namespace API.ViewModels.Response
{
    public class ResponseVM<TEntity>
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public string Messages { get; set; }
        public TEntity? Data { get; set; }
    }
}
