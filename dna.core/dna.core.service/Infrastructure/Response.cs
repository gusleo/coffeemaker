namespace dna.core.service.Infrastructure
{
    public class Response<T>
    {
        public Response() { }

        public Response(bool success, string message){
            Success = success;
            Message = message;
            
        }
        public T Item { get; set; }

        public string Message { get; set; }

        public bool Success { get; set; }
    }
    
}
