using System;
using Microsoft.AspNetCore.Mvc;

namespace EvacuationPlanningMonitoring.Models.DTOs.Base
{
	public class ErrorReponseDto : ProblemDetails
    {
		public ErrorReponseDto() { }
        public string Message { get; set; }
		public ErrorReponseDto(string message, string type = "")
		{
			Message = message;
			Type = type;
        }
		public ErrorReponseDto(string message, int status, string type = "")
		{
			Message = message;
			Type = type;
			Status = status;
        }
	}
}

