using System;
using System.Collections.Generic;
using System.Text;

namespace SalesWorkforce.Common.DataContracts
{
    public class AuthLoginRequestContract
    {
        public string AgentId { get; set; }
        public string BadgeCode { get; set; }
    }
}
