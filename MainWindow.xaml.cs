using MoonSharp.Interpreter;
using MoonSharpRepl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MoonSharpTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
        }
    }

    public class MainViewModel : INotifyPropertyChanged
    {
        private IAppContainer _appContainer;
        private readonly IScriptingService _scriptingService;

        public MainViewModel()
        {
            _appContainer = new AppContainer();
            _scriptingService = _appContainer.ScriptingService;
            _scriptingService.DebugPrint += _scriptingService_DebugPrint;
            _commandText = string.Empty;
            _outputText = string.Empty;
            _runCommand = new DelegateCommand(OnRunCommand);
        }

        private void _scriptingService_DebugPrint(object sender, string e)
        {
            OutputText += $"{e}\n";
        }

        private void OnRunCommand()
        {
            OutputText += $"> {CommandText}\n";
            try
            {
                var result = _scriptingService.RunCommand(_commandText);
            }
            catch (SyntaxErrorException ex)
            {
                OutputText += $"{ex.DecoratedMessage}\n";
            }
            catch (Exception ex)
            {

            }
            CommandText = string.Empty;
        }

        private readonly DelegateCommand _runCommand;
        public  DelegateCommand RunCommand { get { return _runCommand; } }

        private string _commandText;
        public string CommandText 
        { 
            get { return _commandText; }
            set { _commandText = value; NotifyPropertyChanged(nameof(CommandText)); }
        }

        private string _outputText;
        public string OutputText 
        { 
            get { return _outputText; }
            set { _outputText = value; NotifyPropertyChanged(nameof(OutputText)); }
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
