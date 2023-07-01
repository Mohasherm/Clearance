using AspNetCore.Reporting;
using Clearance.Server.Data;
using FastReport;
using FastReport.Data;
using FastReport.Export.PdfSimple;
using FastReport.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

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
            System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        //[HttpGet("GetReport/{Id}")]
        //public IActionResult GetReport(int Id)
        //{
        //    WebReport web = new();
        //    var path = Path.Combine(webHostEnvironment.ContentRootPath, "Reports", "voucher.frx");
        //    web.Report.Load(path);

        //    var connection = new MsSqlDataConnection();
        //    connection.ConnectionString = configuration.GetConnectionString("DefaultConnection");
        //    var con = connection.ConnectionString;
        //    web.Report.SetParameterValue("Id", Id);
        //    web.Report.SetParameterValue("con", con);

        //    web.Report.Prepare();
        //    Stream stream = new MemoryStream();
        //    web.Report.Export(new PDFSimpleExport(), stream);
        //    return File(stream, "Application/zip", "report.pdf");

        //}

        [HttpGet("GetReport/{Id}")]
        public IActionResult GetReport(int Id)
        {
      //      IEnumerable<DataRow> query =
      //(IEnumerable<DataRow>)(from clearance in db.Clearances.AsEnumerable()
      //                       where clearance.Id == Id
      //                       select new { 
      //                           clearance.Id,
      //                           colName = clearance.Collage.Name,
      //                           stdName = clearance.FirstName+" "+clearance.Father+" "+ clearance.LastName,
      //                           clearance.UnivNum,
      //                           clearance.AppointmentDate
      //                       });

            var data = db.Clearances.Select(x => new {
                x.Id,
                colName = x.Collage.Name,
                stdName = x.FirstName + " " + x.Father + " " + x.LastName,
                x.UnivNum,
                x.AppointmentDate
            }).FirstOrDefault(x => x.Id == Id);

            // Create a table from the query.
            //DataTable dt = query.CopyToDataTable<DataRow>();

            var dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("colName");
            dt.Columns.Add("stdName");
            dt.Columns.Add("UnivNum");
            dt.Columns.Add("AppointmentDate");

            DataRow dr = dt.NewRow();
            dr["Id"] = data.Id;
            dr["colName"] = data.colName;
            dr["stdName"] = data.stdName;
            dr["UnivNum"] = data.UnivNum;
            dr["AppointmentDate"] = data.AppointmentDate;

            dt.Rows.Add(dr);

            string mimeType = "";
            var path = Path.Combine(webHostEnvironment.ContentRootPath, "Reports", "Report1.rdlc");
            Dictionary<string, string> param = new()
            {
                { "param", "barcode here" }
            };
            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("dsClearance",dt);

            //pdf
            var result = localReport.Execute(RenderType.Pdf, 1, param, mimeType);
            return File(result.MainStream, "application/pdf");


            //excel
            // var result = localReport.Execute(RenderType.Excel, 1, param, mimeType);
            //return File(result.MainStream , "application/pdf");

        }


    }
}
