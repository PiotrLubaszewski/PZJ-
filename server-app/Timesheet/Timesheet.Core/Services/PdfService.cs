using IronPdf;
using System.Threading.Tasks;
using Timesheet.Core.Interfaces.Services;
using Timesheet.Core.Models.Salaries;

namespace Timesheet.Core.Services
{
    public class PdfService : IPdfService
    {
        private readonly IViewRenderService _viewRenderService;

        public PdfService(IViewRenderService viewRenderService)
        {
            _viewRenderService = viewRenderService;
        }

        public async Task<PdfDocument> GenerateMonthlySalaryDocumentAsync(MonthlySalaryModel model)
        {
            var renderer = new HtmlToPdf();

            var tickets = await _viewRenderService.RenderAsync("MonthlySalaryTemplate", model);
            var pdf = renderer.RenderHtmlAsPdf(tickets);

            return pdf;
        }

        public async Task<PdfDocument> GenerateonthlyTimeConsumingSummaryDocumentAsync(MonthlyTimeConsumingSummaryModel model)
        {
            var renderer = new HtmlToPdf();

            var tickets = await _viewRenderService.RenderAsync("MonthlyTimeConsumingSummaryTemplate", model);
            var pdf = renderer.RenderHtmlAsPdf(tickets);

            return pdf;
        }
    }
}
