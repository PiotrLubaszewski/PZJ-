using IronPdf;
using System.Threading.Tasks;
using Timesheet.Core.Models.Salaries;

namespace Timesheet.Core.Interfaces.Services
{
    public interface IPdfService
    {
        Task<PdfDocument> GenerateMonthlySalaryDocumentAsync(MonthlySalaryModel model);
        Task<PdfDocument> GenerateonthlyTimeConsumingSummaryDocumentAsync(MonthlyTimeConsumingSummaryModel model);
    }
}
