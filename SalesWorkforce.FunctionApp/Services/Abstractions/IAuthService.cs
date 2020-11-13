namespace SalesWorkforce.FunctionApp.Services.Abstractions
{
    public interface IAuthService
    {
        long? Login(string agentId, string badgeCode);
    }
}