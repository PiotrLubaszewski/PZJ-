using System.Threading.Tasks;

namespace Timesheet.Core.Interfaces.Services
{
    public interface IViewRenderService
    {
        Task<string> RenderAsync<TModel>(string name, TModel model);
    }
}
