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


namespace 仓储管理程序
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            //toolStripStatusLabel1.Text = 仓储管理程序.SQLHelper.GetListByOrderCode(user_id);
        }



        

        private void Form1_Load(object sender, EventArgs e)
        {
            //刷新商品管理
            string refresh_information = "select * from Product";
            MySqlParameter parameters = new MySqlParameter(null, MySqlDbType.String);
            dataGridView1.DataSource = SQLHelper.GetDataSet(refresh_information, CommandType.Text, null).Tables[0];
            //
            toolStripStatusLabel1.Text = Form2.indate;

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            //添加商品
            string ProEAN = textBox5.Text;
            string ProName = textBox6.Text;
            string ProWeight = textBox7.Text;
            string ProPrice = textBox8.Text;
            string insertPro = " insert into Product(ProductEAN,ProductName,ProductWeight,ProductPrice)values('" + ProEAN + "','" + ProName + "','" + ProWeight + "','" + ProPrice + "') ";
            try
            {
                SQLHelper.ExecuteNonQuery(insertPro.ToString(), CommandType.Text, null);
                toolStripStatusLabel2.Text = "          商品" + ProName + "添加成功。";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();

            //刷新商品管理
            string refresh_information = "select * from Product";
            MySqlParameter parameters = new MySqlParameter(null, MySqlDbType.String);
            dataGridView1.DataSource = SQLHelper.GetDataSet(refresh_information, CommandType.Text, null).Tables[0];
        }

        private void textBox2_Press(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                //入库商品
                string inList_EAN = textBox2.Text;
                string inList_SendungsNr = textBox1.Text;
                string insert_inList = " insert into inList(SendungsNr,ProductEAN,insertDatetime)values('" + inList_SendungsNr + "','" + inList_EAN + "','" + System.DateTime.Now + "') ";
                string get_ProductName = "select ProductName from product where ProductEAN = '" + inList_EAN + "'";
                
                try
                {
                    string ProName = SQLHelper.GetDataSet(get_ProductName, CommandType.Text, null).Tables[0].Rows[0][0].ToString();
                    toolStripStatusLabel2.Text = ProName;
                    try
                    {
                        SQLHelper.ExecuteNonQuery(insert_inList.ToString(), CommandType.Text, null);
                        toolStripStatusLabel2.Text = "          包裹  " + inList_SendungsNr + "  里的  " + ProName + "  添加成功。";
                    }
                    catch 
                    {
                        MessageBox.Show("入库失败。");
                    }
                }
                catch
                {
                    MessageBox.Show("库中无此商品，请先添加该商品！");
                }
                //刷新【入库列表】
                //string refresh_information = "select * from inList where SendungsNr = '" + inList_SendungsNr + "'";
                string refresh_information = "select * from inList";
                MySqlParameter parameters = new MySqlParameter(null, MySqlDbType.String);
                dataGridView2.AutoGenerateColumns = false;
                dataGridView2.DataSource = SQLHelper.GetDataSet(refresh_information, CommandType.Text, null).Tables[0];
                textBox2.Clear();
                textBox2.Focus();
            }
        }

        private void textBox1_Press(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                string inList_SendungsNr = textBox1.Text;
                //刷新【入库列表】
                string refresh_information = "select * from inlist where SendungsNr = '" + inList_SendungsNr + "'";
                MySqlParameter parameters = new MySqlParameter(null, MySqlDbType.String);
                dataGridView2.AutoGenerateColumns = false;
                dataGridView2.DataSource = SQLHelper.GetDataSet(refresh_information, CommandType.Text, null).Tables[0];

                textBox2.Focus();

            }
        }

        private void textBox5_Press(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                textBox6.Focus();
            }
        }
        private void textBox6_Press(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                textBox7.Focus();
            }
        }
        private void textBox7_Press(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                textBox8.Focus();
            }
        }
        private void textBox8_Press(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                this.button9.PerformClick();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)  
            {  
                for (int i = 0; i < dataGridView1.Rows.Count; i++)  
                {  
                    string _selectValue = dataGridView1.Rows[i].Cells["Column6"].EditedFormattedValue.ToString();
                    if (_selectValue == "True")
                    {
                        //如果CheckBox已选中，则在此处继续编写代码  
                        //Cells[1]=ean;Cells[2]=name;
                        string ean = dataGridView1.Rows[i].Cells[1].Value.ToString();
                        string delete = "DELETE FROM product WHERE ProductEAN = '"+ ean +"'";
                        SQLHelper.ExecuteNonQuery(delete.ToString(), CommandType.Text, null);
                    }
                       
                        
                }  
            }
            //刷新商品管理
            string refresh_information = "select * from Product";
            MySqlParameter parameters = new MySqlParameter(null, MySqlDbType.String);
            dataGridView1.DataSource = SQLHelper.GetDataSet(refresh_information, CommandType.Text, null).Tables[0];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count > 0)
            {
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    string _selectValue = dataGridView2.Rows[i].Cells["Column1"].EditedFormattedValue.ToString();
                    if (_selectValue == "True")
                    {
                        //如果CheckBox已选中，则在此处继续编写代码  
                        //Cells[1]=ean;Cells[2]=name;
                        string insertID = dataGridView2.Rows[i].Cells[6].Value.ToString();
                        string delete = "DELETE FROM inlist WHERE insertID = '"+insertID+"'";
                        SQLHelper.ExecuteNonQuery(delete.ToString(), CommandType.Text, null);
                    }


                }
            }
            string inList_SendungsNr = textBox1.Text;
            //刷新【入库列表】
            string refresh_information = "select * from inlist where SendungsNr = '" + inList_SendungsNr + "'";
            MySqlParameter parameters = new MySqlParameter(null, MySqlDbType.String);
            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.DataSource = SQLHelper.GetDataSet(refresh_information, CommandType.Text, null).Tables[0];
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //开始打包
            //生成内部包裹号
            //string date = System.DateTime.Now.Date.ToString("yyyyMMdd");
            string time = System.DateTime.Now.ToString("yyyyMMdd-HHmmss");
            textBox4.Text = time;
            button1.Visible = false;
            button2.Visible = true;
        }
        private void textBox3_Press(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                //生成内部包裹编号
                string innerPackageNr = textBox4.Text;
                string ProductEAN = textBox3.Text;

                //添加打包商品
                //从表product查询EAN对应商品信息
                
                try
                {
                    string searchPro = " select * from product where ProductEAN ='" + ProductEAN + "'";
                    //EAN,NAME,PRICE,WEIGHT,SUMMER 表中顺序,从0开始
                    string ProName = SQLHelper.GetDataSet(searchPro, CommandType.Text, null).Tables[0].Rows[0][1].ToString();
                    toolStripStatusLabel2.Text = "          商品  " + ProName ;

                    try
                    {

                        
                        //insert into Product(ProductEAN,ProductName,ProductWeight,ProductPrice)values('" + ProEAN + "','" + ProName + "','" + ProWeight + "','" + ProPrice + "') 
                        string dbPro = "insert into dabaolist ( innerPackageNr,ProductEAN,ProductName,PackageTime)values('" + innerPackageNr + "','"+ProductEAN+"','"+ProName+"','"+System.DateTime.Now+"')";
                        SQLHelper.ExecuteNonQuery(dbPro.ToString(), CommandType.Text, null);
                        //toolStripStatusLabel2.Text = SQLHelper.GetDataSet(search, CommandType.Text, null).Tables[0].Rows[0][0].ToString();
                    }
                    catch
                    {
                        MessageBox.Show("失败");
                    }
                }
                catch
                {
                    MessageBox.Show("此商品不在库中。");
                }

                //刷新【打包列表】，显示内部包裹号内的物品。

                string refresh_information = "select * from dabaolist where innerPackageNr = '" + innerPackageNr + "'";
                MySqlParameter parameters = new MySqlParameter(null, MySqlDbType.String);
                dataGridView3.AutoGenerateColumns = false;
                dataGridView3.DataSource = SQLHelper.GetDataSet(refresh_information, CommandType.Text, null).Tables[0];
                
                textBox3.Clear();
                textBox3.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //是否确定结束
            Form3 testDialog = new Form3();
            testDialog.ShowDialog();
            if(testDialog.DialogResult == DialogResult.OK)
            {
                string innerPackageNr = textBox4.Text;
                string outlist = "insert into outlist ( innerPackageNr)values('" + innerPackageNr + "')";
                SQLHelper.ExecuteNonQuery(outlist.ToString(), CommandType.Text, null);
                button1.Visible = true;
                button2.Visible = false;
                dataGridView3.DataSource = null;
                textBox4.Text = null;
            }
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //删除选中行
            Form3 testDialog = new Form3();
            testDialog.ShowDialog();
            if (testDialog.DialogResult == DialogResult.OK)
            {
                if (dataGridView3.Rows.Count > 0)
                {
                    for (int i = 0; i < dataGridView3.Rows.Count; i++)
                    {
                        string _selectValue = dataGridView3.Rows[i].Cells["dataGridViewCheckBoxColumn1"].EditedFormattedValue.ToString();
                        if (_selectValue == "True")
                        {
                            //如果CheckBox已选中，则在此处继续编写代码  
                            //Cells[1]=ean;Cells[2]=name;
                            string dbID = dataGridView3.Rows[i].Cells[6].Value.ToString();
                            string delete = "DELETE FROM dabaolist WHERE dbID = '" + dbID + "'";
                            SQLHelper.ExecuteNonQuery(delete.ToString(), CommandType.Text, null);
                        }


                    }
                }
                //刷新【打包列表】，显示内部包裹号内的物品。

                string innerPackageNr = textBox4.Text;

                string refresh_information = "select * from dabaolist where innerPackageNr = '" + innerPackageNr + "'";
                MySqlParameter parameters = new MySqlParameter(null, MySqlDbType.String);
                dataGridView3.AutoGenerateColumns = false;
                dataGridView3.DataSource = SQLHelper.GetDataSet(refresh_information, CommandType.Text, null).Tables[0];
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //打包-清空
            Form3 testDialog = new Form3();
            testDialog.ShowDialog();
            if (testDialog.DialogResult == DialogResult.OK)
            {
                string innerPackageNr = textBox4.Text;
                string delete = "DELETE FROM dabaolist WHERE innerPackageNr = '" + innerPackageNr + "'";
                SQLHelper.ExecuteNonQuery(delete.ToString(), CommandType.Text, null);
                //刷新【打包列表】，显示内部包裹号内的物品。
                string refresh_information = "select * from dabaolist where innerPackageNr = '" + innerPackageNr + "'";
                MySqlParameter parameters = new MySqlParameter(null, MySqlDbType.String);
                dataGridView3.AutoGenerateColumns = false;
                dataGridView3.DataSource = SQLHelper.GetDataSet(refresh_information, CommandType.Text, null).Tables[0];
            }
            

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string innerPackageNr = textBox9.Text;
            string PackageWeight = textBox11.Text;
            string internationalNr = textBox10.Text;
            string updateInfor = "update dabaolist set  ";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
