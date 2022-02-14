using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEP
{
    public class dataToLog

    {
        public string nodeId;
        public string columnName;
        public object value;
        public string dataType;
        public string tableName;

        public dataToLog(string nId, string cN, string tN)

        {
            nodeId = nId;
            columnName = cN;
            value = "NULL";
            dataType = "String";
            tableName = tN;
        }

        public string valueToString()
        {
            return Convert.ToString(value);
        }


    }
}
