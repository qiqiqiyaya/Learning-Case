using Extract.Domain.Share;

namespace Extract.Domain
{
    public interface IFileDataChecker
    {
        /// <summary>
        /// 数据校验与转换
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        ConvertResult ConvertAndCheck(string str);
    }
}
