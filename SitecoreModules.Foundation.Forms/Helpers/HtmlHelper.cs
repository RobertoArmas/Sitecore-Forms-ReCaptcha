using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Mvc.Helpers;

namespace SitecoreModules.Foundation.Forms.Helpers
{
    public static class HtmlHelper
    {
        public static bool IsExperienceFormsEditMode(this SitecoreHelper helper) => Sitecore.Context.Request.QueryString["sc_formmode"] != null;
    }
}
