
namespace Mame.Doci.CrossCutting.Logging.Contracts
{
    public interface ILogger
    {
        LogLevels PrintingLogLevel{get;set;}
        void LogText(LogLevels MessageType, string Text);
    }
}
