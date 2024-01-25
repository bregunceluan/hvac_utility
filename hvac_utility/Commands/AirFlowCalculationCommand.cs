using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hvac_utility.Utils;
using hvac_utility.Models;
using hvac_utility.Views;

namespace hvac_utility.Commands
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    public class AirFlowCalculationCommand : IExternalCommand
    {

        public Result Execute(ExternalCommandData commandData, ref string message, Autodesk.Revit.DB.ElementSet elements)
        {
            try
            {
                var uiDoc = commandData.Application.ActiveUIDocument;
                AirFlowWindow mainWindow = new AirFlowWindow(uiDoc);
                mainWindow.Show();
                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Fail to execute Airflow Calculation",ex.Message);
                return Result.Failed;
            }
        }
    }
}
