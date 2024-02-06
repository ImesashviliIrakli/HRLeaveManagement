namespace HRLeaveManagement.Application.Contracts.Logging;

public interface IAppLogger<T>
{
    void LogInofrmation(string message, params object[] args);
    void LogWarning(string message, params object[] args);
}
