using AspNetCore.Reporting;
using Clearance.Server.Data;
using Microsoft.AspNetCore.Mvc;
using NetBarcode;
using System.Data;
using System.Text;


namespace Clearance.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly DataContext db;

        public ReportController(IWebHostEnvironment webHostEnvironment, DataContext db)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.db = db;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        

        [HttpGet("GetReport/{Id}")]
        public IActionResult GetReport(int Id)
        {

            var data = db.Clearances.Select(x => new {
                x.Id,
                colName = x.Collage.Name,
                stdName = x.FirstName + " " + x.Father + " " + x.LastName,
                x.UnivNum,
                x.AppointmentDate
            }).FirstOrDefault(x => x.Id == Id);

            //string MyValue = Path.Combine(webHostEnvironment.ContentRootPath, "DetailsClearance", Id.ToString());

            //var barcode = new Barcode(MyValue, NetBarcode.Type.Code128B);
            //var value = barcode.GetBase64Image();

            var dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("colName");
            dt.Columns.Add("stdName");
            dt.Columns.Add("UnivNum");
            dt.Columns.Add("AppointmentDate");
            dt.Columns.Add("Image");

            DataRow dr = dt.NewRow();
            dr["Id"] = data.Id;
            dr["colName"] = data.colName;
            dr["stdName"] = data.stdName;
            dr["UnivNum"] = data.UnivNum;
            dr["AppointmentDate"] = data.AppointmentDate;
            //dr["Image"] = value;

            dt.Rows.Add(dr);

            string mimeType = "";
            var path = Path.Combine(webHostEnvironment.ContentRootPath, "Reports", "Report1.rdlc");
            Dictionary<string, string> param = new()
            {
                { "param", "barcode here" }
            };
            LocalReport localReport = new(path);
            localReport.AddDataSource("dsClearance",dt);

            //pdf
            var result = localReport.Execute(RenderType.Pdf, 1, param, mimeType);
            return File(result.MainStream, "application/pdf");

        }
    }
}