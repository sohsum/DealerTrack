using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace csvToTableDisply.Controllers
{
    public class DealerController : ApiController
    {

        public DataTable Get(string strFilePath)
        {
            string JSONString = string.Empty;

            if (File.Exists(strFilePath))
            {

                StreamReader sReader = new StreamReader(strFilePath);
                string[] tableHeaders = sReader.ReadLine().Split(',');
                DataTable dataTable = new DataTable();
                foreach (string tHeader in tableHeaders)
                {
                    dataTable.Columns.Add(tHeader);
                }
                while (!sReader.EndOfStream)
                {
                    string[] dataRows = Regex.Split(sReader.ReadLine(), ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                    DataRow dataRow = dataTable.NewRow();
                    for (int i = 0; i < tableHeaders.Length; i++)
                    {
                        dataRow[i] = dataRows[i];
                    }
                    dataTable.Rows.Add(dataRow);
                }
                JSONString = JsonConvert.SerializeObject(dataTable);
                return (dataTable);


            }

            return null;
        }

    }
}