using System.Collections.Generic;
using AspNetCoreDemoApp.Utils;

namespace AspNetCoreDemoApp.Models
{
    public class FilterCommand
    {
        public string FilterKey { get; set; }

        public QueryOperator Operator { get; set; }

        public object FilterValue { get; set; }
    }
}