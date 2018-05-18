using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP.PL.Models
{
    public enum Status : byte { Success = 1, Error, Warning}

    public struct AjaxResponse
    {
        public Status Status { get; set; }
        public string Key { get; set; }
        public string Message { get; set; }
        public string JSON { get; set; }
    }
}