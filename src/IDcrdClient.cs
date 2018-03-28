using System.Threading.Tasks;

namespace DcrdClient
{
    public interface IDcrdClient
    {
        // Returns estimated fee as dcr/kb
        Task<decimal> EstimateFeeAsync(int numBlocks);
        Task<GetBestBlockResult> GetBestBlockAsync();
        Task<DcrdRpcResponse> PingAsync();
        Task<DcrdRpcResponse> SendRawTransactionAsync(string hexTransaction);
   }
}