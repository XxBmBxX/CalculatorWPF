using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace CalculatorWPF
{
    class CalculatorViewModel : BaseViewModel
    {

        public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged;
        private static void NotifyStaticPropertyChanged([CallerMemberName] string name = null)
        {
            if (StaticPropertyChanged != null)
                StaticPropertyChanged(null, new PropertyChangedEventArgs(name));
        }

        private Window mWindow;
        private static string calculatorField = "0";
        private static decimal memory = 0;

        #region Public Propertiesww

        public static bool DecimalClicked { get; set; } = true;
        public static bool AllowOperations { get; set; } = false;
        public static string CalculatorField
        {
            get
            {
                return calculatorField;
            }
            set
            {
                calculatorField = value;
                NotifyStaticPropertyChanged();
            }
        }

        public static decimal Memory 
        {
            get 
            { 
                return memory; 
            } 
            set 
            { 
                memory = value;
                NotifyStaticPropertyChanged();
            }
        }
        public ICommand DigitClicked { get; set; }
        public ICommand MemoryClicked { get; set; }
        public ICommand OperationClicked { get; set; }
        public ICommand Close { get; set; }
        public ICommand Minimize { get; set; }

        #endregion

        public CalculatorViewModel(Window window)
        {
            mWindow = window;
            DigitClicked = new RelayParameterizedCommand((parameter) => Operations.DigitClickedUpdate(parameter));
            MemoryClicked = new RelayParameterizedCommand((parameter) => Operations.MemoryClickedUpdate(parameter));
            OperationClicked = new RelayParameterizedCommand((parameter) => Operations.OperationClickedUpdate(parameter));
            Minimize = new RelayCommand(() => mWindow.WindowState = WindowState.Minimized);
            Close = new RelayCommand(() => mWindow.Close());
        }
    }
}
