using Article.Shared.Commands.Contracts;
using Flunt.Notifications;
using System.Collections.Generic;

namespace Article.Shared.Commands
{
    public class GenericCommandResult : ICommandResult
    {
        public GenericCommandResult() { }
        public GenericCommandResult(bool success, string message, object data, int statusCode, IEnumerable<Notification> notifications)
        {
            Success = success;
            Message = message;
            Data = data;
            StatusCode = statusCode;
            Notifications = notifications;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public int StatusCode { get; set; }
        public IEnumerable<Notification> Notifications { get; set; }
    }
}
