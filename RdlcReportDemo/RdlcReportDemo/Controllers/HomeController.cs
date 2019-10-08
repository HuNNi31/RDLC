using Microsoft.Reporting.WebForms;
using RdlcReportDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RdlcReportDemo.Controllers
{
    public class HomeController : Controller
    {
        MVCTutorialEntities db = new MVCTutorialEntities();
        ///itt az Index helyébe szerettem volna egy új view-t meghívni (StudentList), de error404.
        public ActionResult Index()
        {
            return View(db.StudentInfoes);
        }
        public ActionResult Reports(string ReportType)
        {
            LocalReport localreport = new LocalReport();
            localreport.ReportPath = Server.MapPath("~/Report/StudentReport.rdlc");

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "StudentDataSet";
            reportDataSource.Value = db.StudentInfoes.ToList();
            localreport.DataSources.Add(reportDataSource);
            string reportType = ReportType;
            string mimeType;
            string encoding;
            string fileNameExtension;
            if (reportType == "Excel")
            {
                fileNameExtension = "xlsx";
            }
            if (reportType == "Word")
            {
                fileNameExtension = "docx";
            }
            if (reportType == "PDF")
            {
                fileNameExtension = "pdf";
            }
            else
            {
                fileNameExtension = "jpg";
            }
            string[] streams;
            Warning[] warnings;
            byte[] renderByte;
            renderByte = localreport.Render(reportType, "", out mimeType, out encoding, out fileNameExtension,
             out streams, out warnings);
            Response.AddHeader("content-disposition", "attachment; filename= student_report." + fileNameExtension);
            return File(renderByte, fileNameExtension);
           
        }
    }
}