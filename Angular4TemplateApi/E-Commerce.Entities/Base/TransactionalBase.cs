using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Entities
{
    public class TransactionalBase
    {
        public bool ReturnStatus { get; set; }
        public List<String> ReturnMessage { get; set; }
        public Hashtable ValidationErrors { get; set; }
        public int TotalPages { get; set; }
        public int TotalRows { get; set; }
        public int PageSize { get; set; }
        public string SortExpression { get; set; }
        public string SortDirection { get; set; }
        public int CurrentPageNumber { get; set; }
        public string IpAddress { get; set; }

        public TransactionalBase()
        {
            ReturnMessage = new List<String>();
            ReturnStatus = true;
            ValidationErrors = new Hashtable();
            TotalPages = 0;
            TotalPages = 0;
            PageSize = 0;
            IpAddress = string.Empty;            
        }
    }
}
