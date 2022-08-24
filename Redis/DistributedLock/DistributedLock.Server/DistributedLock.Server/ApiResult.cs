namespace DistributedLock.Server
{

    public class ApiResult : ApiResult<object>
    {

    }

    public class ApiResult<T>
    {

        public ResultCode Code { get; set; } = ResultCode.Fail;

        public string Msg { get; set; }

        public T Data { get; set; }
    }

    public enum ResultCode
    {
        Success = 1,
        Fail = 0
    }
}
