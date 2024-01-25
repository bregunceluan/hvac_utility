using Autodesk.Revit.DB;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hvac_utility.Models
{
    public class AirTerminalData
    {
        public AirTerminalData(double airFlowValue, string systemName, ElementId elementId)
        {
            AirFlowValue = Math.Round(airFlowValue,2);
            SystemName = systemName;
            ElementId = elementId;
        }

        public double AirFlowValue { get; set; }
        public string SystemName { get; set; }
        public ElementId ElementId { get; set; }

    }
}
