using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hvac_utility.Utils
{
    public class DuctCategoryFilter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            try
            {
                if (elem.Category?.Name == Category.GetCategory(elem.Document, BuiltInCategory.OST_DuctCurves).Name) return true;
                return false;
            }
            catch (Exception)
            {
                if (elem is null) throw new Exception("Selection filter fail");
                throw new Exception($"Selection filter fail for element {elem.Id}");
            }

        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            throw new NotImplementedException();
        }
    }
}
