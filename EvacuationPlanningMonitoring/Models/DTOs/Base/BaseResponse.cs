using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace EvacuationPlanningMonitoring.Models.DTOs.Base
{
    public class BaseResponse : BaseAPIResponseDto
    {
        //public List<string> Errors { get; set; }
        public BaseResponse() { }
        public BaseResponse(int statusCode, string message) : base(statusCode, message) { }
        public BaseResponse(HttpStatusCode httpStatusCode, string message) : base((int)httpStatusCode, message) { }

        #region Error
        public BaseResponse ErrorRespose(int httpStatusCode, string message = "", ErrorReponseDto error = null)
        {
            return (BaseResponse)base.ErrorRespose(httpStatusCode, message, error);
        }

        public BaseResponse ErrorRespose(HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError, string message = "", ErrorReponseDto error = null)
        {
            return ErrorRespose((int)httpStatusCode, message, error);
        }
        #endregion
    }

    public class BaseResponse<T> : BaseAPIResponseDto<T>
    {
        #region Ctor
        //public List<string> Errors { get; set; }
        //public int ErrorCode { get; set; }
        public BaseResponse() : base() { }
        public BaseResponse(T data, string message = null) : base(data, message) { }

        public BaseResponse(string message = null) : base(message) { }

        public BaseResponse(int statusCode, string message = "") : base(statusCode, message) { }


        #endregion

       

        #region Error
        public BaseResponse<T> ErrorRespose(ErrorReponseDto result, int httpStatusCode, string message = "")
        {
            return (BaseResponse<T>)base.ErrorRespose(result, httpStatusCode, message);
        }

        public BaseResponse<T> ErrorRespose(ErrorReponseDto result, HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError, string message = "", object error = null)
        {
            return ErrorRespose(result, (int)httpStatusCode, message);
        }
        public BaseResponse<T> ErrorRespose(HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError, string message = "", object error = null)
        {
            return ErrorRespose(default, (int)httpStatusCode, message);
        }
        #endregion
    }
}