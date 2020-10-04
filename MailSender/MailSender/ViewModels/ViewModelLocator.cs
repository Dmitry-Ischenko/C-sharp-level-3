using MailSender.Data;
using MailSender.Infrastructure.Converters;
using Microsoft.Extensions.DependencyInjection;

namespace MailSender.ViewModels
{
    class ViewModelLocator
    {
        public MainWindowViewModel MainWindowModel => App.Services.GetRequiredService<MainWindowViewModel>();
        public ProgramData ProgramDataLoc => App.Services.GetRequiredService<ProgramData>();
    }
}
