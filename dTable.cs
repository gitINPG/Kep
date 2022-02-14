using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEP
{
    public class dTable

    {
        public DataTable table = new DataTable();

        public dTable()

        {
            table.Columns.Add("Column", typeof(string));
            table.Columns.Add("Table", typeof(string));
            table.Columns.Add("NodeID", typeof(string));
            table.Columns.Add("Value", typeof(string));

        }

        public void addRecord(dataToLog data)
        {
            table.Rows.Add(data.columnName, data.tableName, data.nodeId, data.valueToString());
        }

        public void removeRecord(dataToLog data)
        {

        }

        public void editRecord(dataToLog data)
        {
            foreach (DataRow dr in table.Rows) 
            {
                if (dr["NodeID"] == data.nodeId) 
                {
                    dr["Value"] = data.valueToString(); 
                    break;
                }
            }
        }

    }
}
