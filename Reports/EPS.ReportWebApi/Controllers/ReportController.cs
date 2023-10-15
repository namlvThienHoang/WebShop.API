using EPS.ReportWebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace EPS.ReportWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("{reportType}")]
        public ActionResult Get(string reportType)
        {
            var reportFileByteString = _reportService.GenerateReportAsync(reportType);
            return File(reportFileByteString, MediaTypeNames.Application.Octet, getReportName("baocaotuybien", reportType));
        }
        private string getReportName(string reportName, string reportType)
        {
            _ = reportName + ".pdf";

            string? outputFileName = reportType.ToUpper() switch
            {
                "XLS" => reportName + ".xls",
                "XLSX" => reportName + ".xlsx",
                "WORD" => reportName + ".doc",
                "WORDX" => reportName + ".docx",
                _ => reportName + ".pdf",
            };
            return outputFileName;
        }
    }
}
