using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using hvac_utility.Models;
using hvac_utility.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Markup;

namespace hvac_utility.ViewModel
{
    public class AirFlowCalculationViewModel : ObservableObject
    {
        private readonly Document _doc;
        private readonly UIDocument _uiDoc;
        private readonly AirFlowCalculationHandler _handler;
        private readonly ExternalEvent _event;


        private ObservableCollection<AirTerminalData> _datas;
        public ObservableCollection<AirTerminalData> Datas
        {
            get => _datas;
            set => SetProperty(ref _datas, value);
        }

        private double _total;
        public double Total
        {
            get => _total;
            set => SetProperty(ref _total, value);
        }

        public ICommand ExecutionClickCommand { get; set; }

        public AirFlowCalculationViewModel(UIDocument uiDoc, AirFlowCalculationHandler handler)
        {
            _doc = uiDoc.Document;
            _uiDoc = uiDoc;
            _handler = handler;
            _event = ExternalEvent.Create(handler);
            ExecutionClickCommand = new RelayCommand(ClickExecute);
        }

        private void ClickExecute()
        {
            try
            {
                _handler._executeAction = HvacCalculation;
                _event.Raise();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void HvacCalculation()
        {
            try
            {
                var airflowsList = new List<AirTerminalData>();
                var reference = _uiDoc.Selection.PickObject(ObjectType.Element, new DuctCategoryFilter(), "Pick a duct to calculate the airflow.");
                var selectedElement = _doc.GetElement(reference.ElementId) as Duct;
                var elementSet = (selectedElement.MEPSystem as MechanicalSystem).DuctNetwork;
                var it = elementSet.ForwardIterator();
                it.Reset();
                while (it.MoveNext())
                {
                    Total = 0;
                    var element = it.Current as Element;
                    if (element.Category.Name == "Air Terminals")
                    {
                        var parameterAirFlow = element.get_Parameter(new Guid("b947683d-5fa9-4a90-850f-535c1234d9bc"));
                        if (parameterAirFlow == null) throw new Exception("Fail to get the airflow value parameter");
                        var value = parameterAirFlow.AsDouble();

                        var parameterSystemName = element.get_Parameter(BuiltInParameter.RBS_SYSTEM_NAME_PARAM);
                        var name = parameterSystemName.AsValueString();
                        if (parameterSystemName == null) throw new Exception("Fail to get the air system name parameter");

                        var dt = new AirTerminalData(UnitUtils.ConvertFromInternalUnits(value, UnitTypeId.Millimeters), name, element.Id);
                        airflowsList.Add(dt);
                    }
                }
                Total = Math.Round(airflowsList.Sum(a => a.AirFlowValue), 2);
                Datas = new ObservableCollection<AirTerminalData>(airflowsList);
            }
            catch (Exception ex)
            {
                throw ex;
            }     
        }
    }
}
