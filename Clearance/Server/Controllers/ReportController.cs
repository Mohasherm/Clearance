using Clearance.Server.Data;
using FastReport.Data;
using FastReport.Export.PdfSimple;
using FastReport.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clearance.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly DataContext db;
        private readonly IConfiguration configuration;

        public ReportController(IWebHostEnvironment webHostEnvironment, DataContext db, IConfiguration configuration)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.db = db;
            this.configuration = configuration;
        }

        [HttpGet("GetReport/{Id}")]
        public IActionResult GetReport(int Id)
        {
            WebReport web = new();
            var path = Path.Combine(webHostEnvironment.ContentRootPath, "Reports", "voucher.frx");
            web.Report.Load(path);

            var connection = new MsSqlDataConnection();
            connection.ConnectionString = configuration.GetConnectionString("DefaultConnection");
            var con = connection.ConnectionString;
            web.Report.SetParameterValue("Id", Id);
            web.Report.SetParameterValue("con", con);

            web.Report.Prepare();
            Stream stream = new MemoryStream();
            web.Report.Export(new PDFSimpleExport(), stream);
            return File(stream, "Application/zip", "report.pdf");

        }
    }
}
