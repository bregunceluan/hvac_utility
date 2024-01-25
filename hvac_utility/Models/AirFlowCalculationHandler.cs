using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hvac_utility.Models
{
    public class AirFlowCalculationHandler : IExternalEventHandler
    {
        public Action _executeAction { get; set; }
        public void Execute(UIApplication app)
        {
            try
            {
                _executeAction?.Invoke();
                return;
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Fail to execute Airflow Calculation", ex.Message);
                return;
            }
        }

        public string GetName()
        {
            return nameof(AirFlowCalculationHandler);
        }
    }
}
