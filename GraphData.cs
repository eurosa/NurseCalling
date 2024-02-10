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
        public void getGraphData(SQLiteConnection mbConnection)
        {
            TimeSpan result = new TimeSpan();
            SQLiteCommand comm = new SQLiteCommand("Select lastCallStatus,date_,dateTime,elapseTime,sum(totalMinutes) as myTime,registerId " +
                "From call_table GROUP BY registerId ORDER BY dateTime ASC ", mbConnection);

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
