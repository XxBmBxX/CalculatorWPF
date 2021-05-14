using PropertyChanged;
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace CalculatorWPF
{
    [AddINotifyPropertyChangedInterface]
    class BaseViewModel : INotifyPropertyChanged
    {

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
        /// <summary>
        /// Propertychanged even handler
        /// </summary>
        /// <param name="property"></param>


        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        #region Commands

        /// <summary>
        /// Method for executing a command
        /// </summary>
        /// <param name="updatingFlag"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        protected async Task RunCommand(Expression<Func<bool>> updatingFlag, Func<Task> action)
        {
            //Checks if the flag is true
            if (updatingFlag.GetPropertyValue())
                return;
            //Set the property flag to true to indicate we are running
            updatingFlag.SetPropertyValue(true);

            try
            {
                //Run the passed action
                await action();
            }
            finally
            {
                //Set the porperty flag back to false
                updatingFlag.SetPropertyValue(false);
            }
        }

        #endregion

    }
}
