namespace WolvenKit.Common.Interfaces;
public interface ITaskReporter
{
    void ReportError(string assetPath, string stacktrace);
    void ReportWarning(string assetPath, string message);
}
