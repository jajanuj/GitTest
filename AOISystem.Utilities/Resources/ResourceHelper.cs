using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Resources;
using System.Reflection;

namespace AOISystem.Utilities.Resources
{
    public class ResourceHelper
    {
        public static ResourceManager Language = new ResourceManager("AOISystem.Utilities.Resources.Language", Assembly.GetExecutingAssembly());

        public static ResourceManager Library = new ResourceManager("AOISystem.Utilities.Resources.Library", Assembly.GetExecutingAssembly());
    }
}
