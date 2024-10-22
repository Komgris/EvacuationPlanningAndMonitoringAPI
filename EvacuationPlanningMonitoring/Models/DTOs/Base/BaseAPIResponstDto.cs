using System.Net;
using EvacuationPlanningMonitoring.Models.DTOs.Base.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EvacuationPlanningMonitoring.Models.DTOs.Base
{
    public class BaseAPIResponseDto : IActionResult, IBaseAPIResponseDto
    {
        public bool IsSuccess { get; set; } = false;
        public int StatusCode { get; set; } = (int)HttpStatusCode.InternalServerError;
        public string Message { get; set; }
        public int DataTotalCount { get; set; } = 0;
        public ErrorReponseDto? Error { get; set; }

        #region Ctor

        public BaseAPIResponseDto(int statusCode, string message = default)
        {
            StatusCode = statusCode;
            Message = message;
        }

        public BaseAPIResponseDto(string message = default)
        {
            IsSuccess = true;
            Message = message;
        }
        #region Error response 
        public virtual BaseAPIResponseDto ErrorRespose(int httpStatusCode, string message = default, ErrorReponseDto error = default) // base
        {
            this.IsSuccess = false;
            this.StatusCode = (int)httpStatusCode;
            this.Error = error;
            this.Message = message;

            return this;
        }
        public virtual BaseAPIResponseDto ErrorRespose(HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError, string message = "", ErrorReponseDto error = default)
        {
            return ErrorRespose((int)httpStatusCode, message, error);
        }
        #endregion
        public virtual async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(this);
            await objectResult.ExecuteResultAsync(context);
        }
        #endregion

    }

    public class BaseAPIResponseDto<T> : BaseAPIResponseDto, IBaseAPIResponseDto<T>
    {
        public T Data { get; set; }

        #region Ctor
        public BaseAPIResponseDto() { }


        public BaseAPIResponseDto(T data, string message = null)
        {
            IsSuccess = true;
            Message = message;
            Data = data;
            StatusCode = 200;
        }

        public BaseAPIResponseDto(int statusCode, T data, string message = null)
        {
            IsSuccess = true;
            Message = message;
            Data = data;
            StatusCode = statusCode;
        }

        // 200 - 299 is well response
        //     // 300 - 399 is redirect section
        public BaseAPIResponseDto(int statusCode = 0, string message = null) : base(statusCode, message) => IsSuccess = statusCode < 399;

        public BaseAPIResponseDto(string message = default) : base(message) { }
        #endregion

        #region Helper Function
       
        #region Error response
        public virtual BaseAPIResponseDto<T> ErrorRespose(ErrorReponseDto error, int httpStatusCode, string message = "") // base
        {
            base.ErrorRespose(httpStatusCode, message);
            this.Error = error;

            return this;
        }
        public virtual BaseAPIResponseDto<T> ErrorRespose(ErrorReponseDto error = default, HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError, string message = "")
        {
            return ErrorRespose(error, (int)httpStatusCode, message);
        }
        #endregion

        #endregion

        #region ActionResult        
        public override async Task ExecuteResultAsync(ActionContext context)
        {
            //var objectResult = new ObjectResult(this);
            //await objectResult.ExecuteResultAsync(context);
            await base.ExecuteResultAsync(context);
        }
        #endregion
    }
}