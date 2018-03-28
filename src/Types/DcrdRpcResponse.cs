namespace DcrdClient
{
    public class DcrdRpcResponse
    {
        public string Id { get; set; }
        public string Jsonrpc { get; set; }
        public DcrdRpcError Error { get; set; }
        
        public class DcrdRpcError
        {
            public int? Code { get; set; }
            public string Message { get; set; }
        }

    }
    
    public class DcrdRpcResponse<T> : DcrdRpcResponse
    {
        public T Result { get; set; }
    }
}