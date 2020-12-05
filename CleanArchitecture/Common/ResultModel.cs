using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.ViewModels.Common
{
    public class ResultModel
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public ResultModel(int code = 0, string message = "", object data = null)
        {
            Code = code;
            Message = message;
            Data = data;
        }
        public Result result { get; set; }
        public Result GetFormatObject()
        {
            return new Result
            {
                status = new Status
                {
                    code = Code,
                    message = Message
                },
                data = Data ?? ""
            };
        }
        public class Result
        {
            public Status status { get; set; }
            public object data { get; set; }
        }
        public class Status
        {
            public int code { get; set; }
            public string message { get; set; }
        }
    }
}
