using System.Net;

namespace EvacuationPlanningMonitoring.Models.DTOs.Base.Interfaces
{
    public interface IBaseAPIResponseDto
    {
        bool IsSuccess { get; set; }
        int StatusCode { get; set; }
        string Message { get; set; }
        ErrorReponseDto? Error { get; set; }
    }

    public interface IBaseAPIResponseDto<T> : IBaseAPIResponseDto
    {
        public T Data { get; set; }
        BaseAPIResponseDto<T> ErrorRespose(ErrorReponseDto result, int httpStatusCode, string message = "");

        BaseAPIResponseDto<T> ErrorRespose(ErrorReponseDto result = default,
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError, string message = "");
    }
}