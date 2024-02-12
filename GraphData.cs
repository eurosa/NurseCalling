using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NurseCalling
{
    public class GraphData
    {


        SQLiteDataReader reader;
        public List<string> x = new List<string>();
        public List<double> y = new List<double>();
        public Dictionary<String, Double> dateTemperature = new Dictionary<String, Double>();
        // Dictionary<Double, String> dateTimeArrayList = new Dictionary<Double, String>();
        public Dictionary<String, Double> hubNTime = new Dictionary<String, Double>();
        private string lastCallStatus;
        private string dateTime;
        private string _date;
        private string elapseTime;
        private string registerId;
        private string totalMinutes;
        SQLiteCommand comm;
        public void getGraphData(SQLiteConnection mbConnection, string keyregisterId,int comboBoxSiteNameIndex, string startDate, string endDate)
        {
            TimeSpan result = new TimeSpan();

            if (comboBoxSiteNameIndex>0) {
                comm = new SQLiteCommand("Select lastCallStatus,date_,dateTime,elapseTime,sum(totalMinutes) as myTime,registerId " +
                      " From call_table where registerId='"+ keyregisterId + "' and  date_  between '" + startDate + "' and '" + endDate + "' GROUP BY registerId ORDER BY dateTime ASC ", mbConnection);

            } else {

                comm = new SQLiteCommand("Select lastCallStatus,date_,dateTime,elapseTime,sum(totalMinutes) as myTime,registerId " +
                    "From call_table where  date_  between '" + startDate + "' and '" + endDate + "' GROUP BY registerId ORDER BY dateTime ASC ", mbConnection);

            }

            

            reader = comm.ExecuteReader();

            // -------------This true works for both mysql and ms access--------------------------------------------------------------
            while (reader.Read())
            {
                lastCallStatus = reader["lastCallStatus"].ToString();
                _date = reader["date_"].ToString();
                dateTime = reader["dateTime"].ToString();
                elapseTime = reader["elapseTime"].ToString();
                registerId = reader["registerId"].ToString();
                totalMinutes = reader["myTime"].ToString();
                result += TimeSpan.Parse("00:" + reader["elapseTime"].ToString());

                // x.Add(dateTime);
                // y.Add(Convert.ToDouble(tempData.Trim()));
                hubNTime.Add(lastCallStatus, Double.Parse(totalMinutes));
                // https://stackoverflow.com/questions/30070913/c-sharp-zedgraph-graphs-jumping

            }

             

        }
    }
}
