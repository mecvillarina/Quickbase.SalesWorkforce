using SalesWorkforce.Common.Models;
using System.Collections.Generic;

namespace SalesWorkforce.Common.DataContracts
{
    public class SalesAgentContract
    {
        public long RecordId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string AgentId { get; set; }
        public string EmailAddress { get; set; }
        public string DisplayName { get; set; }

        public SalesAgentContract()
        {

        }

        public SalesAgentContract(Dictionary<string, Datum> data)
        {
            RecordId = (long)data["3"].Value;
            FirstName = data["6"].Value.ToString();
            MiddleName = data["7"].Value.ToString();
            LastName = data["8"].Value.ToString();
            AgentId = data["9"].Value.ToString();
            EmailAddress = data["11"].Value.ToString();
            DisplayName = data["15"].Value.ToString();
        }
    }
}
