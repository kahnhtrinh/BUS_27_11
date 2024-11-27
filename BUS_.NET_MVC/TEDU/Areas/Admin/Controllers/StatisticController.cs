using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TEDU.Areas.Admin.Controllers
{
    public class StatisticController : Controller
    {
        // GET: Admin/Statistic
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void ExportPDF(string imageBase64)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Chuỗi Base64 nhận được: " + imageBase64);

                // Tạo file PDF
                Document document = new Document(PageSize.A4, 10, 10, 10, 10);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                    document.Open();

                    // Thêm tiêu đề
                    Paragraph title = new Paragraph("Báo cáo Thống kê", new Font(Font.FontFamily.HELVETICA, 18, Font.BOLD));
                    title.Alignment = Element.ALIGN_CENTER;
                    document.Add(title);

                    // Chuyển đổi ảnh base64 thành Image
                    if (!string.IsNullOrEmpty(imageBase64) && imageBase64.Contains(","))
                    {
                        var base64Data = imageBase64.Split(',')[1];
                        System.Diagnostics.Debug.WriteLine("Chuỗi Base64 sau khi tách: " + base64Data);

                        byte[] imageBytes = Convert.FromBase64String(base64Data);
                        System.Diagnostics.Debug.WriteLine("Số byte của ảnh: " + imageBytes.Length);

                        iTextSharp.text.Image chartImage = iTextSharp.text.Image.GetInstance(imageBytes);
                        chartImage.ScaleToFit(500f, 300f);
                        chartImage.Alignment = Element.ALIGN_CENTER;
                        document.Add(chartImage);
                    }

                    document.Close();
                    System.Diagnostics.Debug.WriteLine("Đã đóng document sau khi ghi.");

                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=Report.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.AddHeader("Content-Length", memoryStream.ToArray().Length.ToString());
                    Response.BinaryWrite(memoryStream.ToArray());
                    Response.Flush();
                    Response.Close();

                    System.Diagnostics.Debug.WriteLine("Đã trả về file PDF thành công.");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Export PDF Error: " + ex.Message);
            }
        }






    }
}