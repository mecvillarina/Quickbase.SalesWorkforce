using System;
using System.Collections.Generic;
using System.Text;

namespace SalesWorkforce.Common.Models
{
    public class AuthToken
    {
        public string AccessToken { get; set; }
        public long ExpireAt { get; set; }
    }
}