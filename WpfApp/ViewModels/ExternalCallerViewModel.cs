using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp.ViewModels.Commands;

namespace WpfApp.ViewModels
{
    public class ExternalCallerViewModel : ViewModelBase
    {
        private readonly Process _callerProcess;
        private string _domainName = string.Empty;
        private string _externalResult = string.Empty;

        public string DomainName
        {
            get => _domainName;
            set => SetProperty(ref _domainName, value);
        }

        public ICommand LookUpCommand { get; }

        public string ExternalResult
        {
            get => _externalResult;
            private set => SetProperty(ref _externalResult, value);
        }

        public ExternalCallerViewModel()
        {
            _callerProcess = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = "nslookup",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                }
            };

            LookUpCommand = new RelayCommand(
                parameter =>
                {
                    ExternalResult = string.Empty;
                    _callerProcess.StartInfo.Arguments = _domainName?.Trim();
                    _callerProcess.Start();
                    Task.Run(() => ReadOutput(_callerProcess.StandardOutput));
                    Task.Run(() => ReadOutput(_callerProcess.StandardError));
                    Task.Run(() => { _callerProcess.WaitForExit(); _callerProcess.Close(); });
                },
                parameter => !string.IsNullOrWhiteSpace(_domainName));
        }

        private void ReadOutput(TextReader reader)
        {
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                ExternalResult += line + '\n';
            }
        }
    }
}
