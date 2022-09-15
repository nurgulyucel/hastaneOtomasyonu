using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace HastaneOtomasyonProjesi
{
    class sqlBaglantisi
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection("Data Source=LAPTOP-DMD5S6B7;Initial Catalog=HastaneOtomasyonu;Integrated Security=True");
            baglan.Open();
            return baglan;
        }
    }
}
