using System.Net;

namespace InventoryV2.Shares
{

    public class Response
    {
        public bool IsSuccess { get; set; }
        public string Message { get; }
        public HttpStatusCode StatusCode { get; }

        public Response(bool isSuccess, string message, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            IsSuccess = isSuccess;
            Message = message;
            StatusCode = statusCode;
        }

        public static Response Success(string message = "", HttpStatusCode statusCode = HttpStatusCode.OK)
            => new Response(true, message, statusCode);
        public static Response Failure(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
            => new Response(false, message, statusCode);

    }

    public class Response<T>:Response
    {
        public T Data { get; }
        public Response(bool isSuccess, string message, HttpStatusCode statusCode = HttpStatusCode.OK, T data = default!)
            :base(isSuccess, message , statusCode)
         {
            Data = data;
         }
        public static Response<T> Success(T data, string message = "", HttpStatusCode statusCode = HttpStatusCode.OK)
            => new Response<T>(true, message, statusCode, data);
        public static new Response<T> Failure(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
      => new Response<T>(false, message, statusCode, default!);
    }

}


//Controller Layer

//if (result.IsSuccess) return Ok(result.Value);
//return StatusCode(result.StatusCode.Value, new { message = result.ErrorMessage }


//Service Layer

//return Result<User>.Failure("User not found", StatusCodes.Status404NotFound);