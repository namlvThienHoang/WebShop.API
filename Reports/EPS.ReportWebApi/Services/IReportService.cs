namespace EPS.ReportWebApi.Services
{
    public partial interface IReportService
    {
        byte[] GenerateReportAsync(string reportType);
    }
}
