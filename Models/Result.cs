namespace EMS.Amaanat.Web.Models
{
    public class Result
    {
        public Result() { }
        public Result(string message, bool isSuccess = true)
        {
            Message = message;
            IsSuccess = isSuccess;
        }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
