using System;
using System.Collections.Generic;

namespace LIBUtilities.Core
{
    public class ErrorHelper
    {
        public static Dictionary<string, object> GetError(
            string label = "", Exception ex = null)
        {
            var response = new Dictionary<string, object>();
            if (string.IsNullOrEmpty(label))
                label = "lbPleaseTry";
            response["Error"] = label;
            if (ex != null)
                response["Description"] = ex.ToString();
            response["Response"] = "ERROR";
            return response;
        }
    }
}