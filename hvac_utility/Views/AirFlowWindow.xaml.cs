using Autodesk.Revit.UI;
using hvac_utility.Models;
using hvac_utility.ViewModel;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace hvac_utility.Views
{
    /// <summary>
    /// Interaction logic for AirFlowWindow.xaml
    /// </summary>
    public partial class AirFlowWindow : Window
    {
        public AirFlowCalculationViewModel VM;
        public AirFlowWindow(UIDocument uIDocument)
        {
            try
            {
                var handler = new AirFlowCalculationHandler();
                VM = new AirFlowCalculationViewModel(uIDocument, handler);
                this.DataContext = VM;
                InitializeComponent();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
