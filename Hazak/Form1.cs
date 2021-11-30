using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Hazak
{
    public partial class Form1 : Form
    {
        MySqlConnection connection = null;
        MySqlCommand sql = null;
        public Form1()
        {
            InitializeComponent();
         
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MySqlConnectionStringBuilder sb = new MySqlConnectionStringBuilder();
            sb.Server = "localhost";
            sb.UserID = "root";
            sb.Password = "";
            sb.Database = "hazak";
            sb.CharacterSet = "utf8";
            connection = new MySqlConnection(sb.ToString());
            sql = connection.CreateCommand();
            adatokBetoltese();
        }



        private void adatokBetoltese()
        {
            listBox_hazak.Items.Clear();
            try
            {
                connection.Open();
                sql.CommandText = "SELECT * FROM haz left JOIN csaladi on haz.id = csaladi.hid left join tomb on haz.id = tomb.hid;";
                using (MySqlDataReader dr = sql.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        if (dr.GetInt32("csaladi") == 1)
                        {
                            
                            Csaladi ujc = new Csaladi(dr.GetString("cim"), dr.GetInt32("id"), dr.GetInt32("alapterulet"), dr.GetString("epitesianyag"), dr.GetDateTime("mkezdete"), dr.GetDateTime("mvege"), dr.GetInt32("ottelok"),dr.GetBoolean("garazs")  , dr.GetString("teto"));
                            listBox_hazak.Items.Add(ujc);

                        }
                        else if (dr.GetInt32("csaladi") == 0)
                        {
                            
                            Tomb ujt = new Tomb(dr.GetString("cim"), dr.GetInt32("id"), dr.GetInt32("alapterulet"), dr.GetString("epitesianyag"), dr.GetDateTime("mkezdete"), dr.GetDateTime("mvege"), dr.GetInt32("lakasok"), dr.GetString("fenntartas"), dr.GetBoolean("lift"));
                            listBox_hazak.Items.Add(ujt);
                        }
                        else
                        {
                            label_tipus.Text = "Kérdezz jobbat";
                        }

                         //   Haz uj = new Haz(dr.GetString("cim"), dr.GetInt32("id"), dr.GetInt32("alapterulet"), dr.GetString("epitesianyag"), dr.GetDateTime("mkezdete"), dr.GetDateTime("mvege"));
                        //listBox_hazak.Items.Add(uj);
                    }
                }
                connection.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listBox_hazak_SelectedIndexChanged(object sender, EventArgs e)
        {
            var kiv = listBox_hazak.SelectedItem;
            if (kiv.GetType() == typeof(Csaladi))
            {
                comboBox_valasztott.SelectedIndex = 0;
                Csaladi item1 = (Csaladi)kiv;
                textBox_cim.Text = item1.Cim;
                textBox_alap.Text = item1.Alapterulet.ToString();
                dateTimePicker_kezdet.Value = item1.Mkezdete;
                dateTimePicker_veg.Value = item1.Mvege;
                textBox_ottelok.Text = item1.Ottelok.ToString();
                if (item1.Garazs)
                {
                    radioButton_garazsYes.Checked = true;
                }
                else
                {
                    radioButton_garazsNo.Checked = true;
                }
                int index = comboBox_teto.FindString(item1.Teto);
                comboBox_teto.SelectedIndex = index;
                textBox_lakas.Text = "";
                comboBox_tipus.SelectedIndex = -1;
                radioButton_liftYes.Checked = false;
                radioButton_liftNo.Checked = false;




            }
            else if(kiv.GetType() == typeof(Tomb))
            {

                comboBox_valasztott.SelectedIndex = 1;
                Tomb item2 = (Tomb)kiv;
                textBox_cim.Text = item2.Cim;
                textBox_alap.Text = item2.Alapterulet.ToString();
                dateTimePicker_kezdet.Value = item2.Mkezdete;
                dateTimePicker_veg.Value = item2.Mvege;


                radioButton_garazsYes.Checked = false;
                radioButton_garazsNo.Checked = false;
                comboBox_teto.SelectedIndex = -1;

                textBox_lakas.Text = item2.Lakasok.ToString();
                int index2 = comboBox_tipus.FindString(item2.Tipus);
                comboBox_tipus.SelectedIndex = index2;
                if (item2.Lift)
                {
                    radioButton_liftYes.Checked = true;
                }
                else
                {
                    radioButton_liftNo.Checked = true;
                }
              

            }
            else
            {
                label_tipus.Text = "Kérdezz jobbat";
            }
        }

        private void button_modosit_Click(object sender, EventArgs e)
        {

            var kiv = listBox_hazak.SelectedItem;
            if (kiv.GetType() == typeof(Csaladi))
            {
                Csaladi item1 = (Csaladi)kiv;
                sql.CommandText = "UPDATE csaladi LEFT JOIN haz on haz.id = csaladi.hid SET haz.alapterulet = @alap , haz.mkezdete = @mkezdete, haz.mvege = @mvege , csaladi.ottelok = @ottelok , csaladi.garazs =@garazs , csaladi.teto = @teto WHERE haz.id = " + item1.Id;

             
               
                string alap = textBox_alap.Text;
                string mkezdete = dateTimePicker_kezdet.Value.ToString("yyyy-MM-dd");
                string mvege = dateTimePicker_veg.Value.ToString("yyyy-MM-dd");
              //  MessageBox.Show(mvege +" és az id" + id);
                string ottelok = textBox_ottelok.Text;
                string garazs = radioButton_garazsYes.Checked ? "1" : "0";
                string teto = comboBox_teto.SelectedItem.ToString();
                sql.Parameters.Clear();
                sql.Parameters.AddWithValue("@alap", alap);
                sql.Parameters.AddWithValue("@mkezdete", mkezdete);
                sql.Parameters.AddWithValue("@mvege", mvege);
              //  sql.Parameters.AddWithValue("@id", id);
                sql.Parameters.AddWithValue("@ottelok", ottelok);
                sql.Parameters.AddWithValue("@garazs",garazs);
                sql.Parameters.AddWithValue("@teto", teto);
                try
                {

                    connection.Open();
                    MessageBox.Show(sql.CommandText);
                    if (sql.ExecuteNonQuery() < 1)
                    {
                        MessageBox.Show("A módosítás sikertelen!");
                        return;
                    }
                    else
                    {
                        connection.Close();
                        MessageBox.Show("A módosítás sikeres!");
                        adatokBetoltese();
                        comboBox_valasztott.SelectedIndex = -1;
                        textBox_cim.Text = "";
                        textBox_alap.Text = "";
                        dateTimePicker_kezdet.Value = DateTime.Today;
                        dateTimePicker_veg.Value = DateTime.Today;
                        textBox_ottelok.Text = "";
                        radioButton_garazsYes.Checked = false;
                        radioButton_garazsNo.Checked = false;
                        comboBox_teto.SelectedIndex = -1;
                        textBox_lakas.Text = "";
                        radioButton_liftYes.Checked = false;
                        radioButton_liftNo.Checked = false;
                        comboBox_tipus.SelectedIndex = -1;
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
            else if(kiv.GetType() == typeof(Tomb))

            {
                Tomb item1 = (Tomb)kiv;
                sql.CommandText = "UPDATE tomb LEFT JOIN haz on haz.id = tomb.hid SET haz.alapterulet = @alap , haz.mkezdete = @mkezdete, haz.mvege = @mvege , tomb.lakasok = @lakasok , tomb.lift =@lift , tomb.fenntartas = @tipus WHERE haz.id = " + item1.Id;

              
                string alap = textBox_alap.Text;
                string mkezdete = dateTimePicker_kezdet.Value.ToString("yyyy-MM-dd");
                string mvege = dateTimePicker_veg.Value.ToString("yyyy-MM-dd");
                string lakasok = textBox_lakas.Text;
                int lift = radioButton_liftYes.Checked ? 1 : 0;
                string tipus = comboBox_tipus.SelectedItem.ToString();
                sql.Parameters.Clear();
                sql.Parameters.AddWithValue("@alap", alap);
                sql.Parameters.AddWithValue("@mkezdete", mkezdete);
                sql.Parameters.AddWithValue("@mvege", mvege);
               // sql.Parameters.AddWithValue("@id", id);
                sql.Parameters.AddWithValue("@lakasok", lakasok);
                sql.Parameters.AddWithValue("@lift", lift);
                sql.Parameters.AddWithValue("@tipus", tipus);

                try
                {

                    connection.Open();
                    MessageBox.Show(sql.CommandText);
                    if (sql.ExecuteNonQuery() <1)
                    {
                        MessageBox.Show("A módosítás sikertelen!");
                        return;
                    }
                    else
                    {
                        connection.Close();
                        MessageBox.Show("A módosítás sikeres!");
                        adatokBetoltese();
                        comboBox_valasztott.SelectedIndex = -1;
                        textBox_cim.Text = "";
                        textBox_alap.Text = "";
                        dateTimePicker_kezdet.Value = DateTime.Today;
                        dateTimePicker_veg.Value = DateTime.Today;
                        textBox_ottelok.Text = "";
                        radioButton_garazsYes.Checked = false;
                        radioButton_garazsNo.Checked = false;
                        comboBox_teto.SelectedIndex = -1;
                        textBox_lakas.Text = "";
                        radioButton_liftYes.Checked = false;
                        radioButton_liftNo.Checked = false;
                        comboBox_tipus.SelectedIndex = -1;
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else
            {
                MessageBox.Show("Huppsz mi lesz itt az osztály?");
            }
      
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var kiv = listBox_hazak.SelectedItem;
            try
            {
                if (kiv.GetType() == typeof(Csaladi))
                {
                    Csaladi item1 = (Csaladi)kiv;

                    connection.Open();
                    sql.CommandText = "DELETE FROM `haz` WHERE `id`=" + item1.Id;
                    sql.ExecuteNonQuery();
                    connection.Close();
                    connection.Open();
                    sql.CommandText = "DELETE FROM `csaladi` WHERE `id`=" + item1.Id;
                    sql.ExecuteNonQuery();
                    connection.Close();
                }
                else
                {
                    Tomb item1 = (Tomb)kiv;

                    connection.Open();
                    sql.CommandText = "DELETE FROM `haz` WHERE `id`=" + item1.Id;
                    sql.ExecuteNonQuery();
                    connection.Close();
                    connection.Open();
                    sql.CommandText = "DELETE FROM `tomb` WHERE `id`=" + item1.Id;
                    sql.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            adatokBetoltese();
        }
    }
}
