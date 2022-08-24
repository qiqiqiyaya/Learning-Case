using Microsoft.AspNetCore.Mvc;

namespace DistributedLock.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueueController : ControllerBase
    {
        [HttpGet("EnqueueMsg")]
        public async Task<ApiResult> EnqueueMsgAsync(string rediskey, string redisValue)
        {
            ApiResult obj = new ApiResult();

            try
            {
                long enqueueLong = default;
                for (int i = 0; i < 1000; i++)
                {
                    enqueueLong = await MyRedisSubPublishHelper.EnqueueListLeftPushAsync(rediskey, redisValue + i);
                }
                obj.Code = ResultCode.Success;
                obj.Data = "入队的数据长度：" + enqueueLong;
                obj.Msg = "入队成功！";
            }
            catch (Exception ex)
            {

                obj.Msg = $"入队异常，原因：{ex.Message}";
            }
            return obj;
        }

        [HttpGet("DequeueMsg")]
        public async Task<ApiResult> DequeueMsgAsync(string rediskey)
        {
            ApiResult obj = new ApiResult();
            try
            {
                string dequeueMsg = await MyRedisSubPublishHelper.DequeueListPopRightAsync(rediskey);
                obj.Code = ResultCode.Success;
                obj.Data = $"出队的数据是：{dequeueMsg}";
                obj.Msg = "出队成功！";
            }
            catch (Exception ex)
            {
                obj.Msg = $"出队异常，原因：{ex.Message}";
            }
            return obj;
        }
    }
}
