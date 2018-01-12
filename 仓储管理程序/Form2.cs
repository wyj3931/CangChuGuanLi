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
    public partial class Form2 : Form
    {
        public bool login = false;
        public static string indate;
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {//用户登陆

            MySqlParameter parameters = new MySqlParameter(null, MySqlDbType.String);
            //Tables[0].Rows[0][1]是邮箱。
            //Tables[0].Rows[0][2]是密码。
            //Tables[0].Rows[0][3]是有效期。
            //Tables[0].Rows[0][4]是权限。
            try
            {
                String user_email = textBox1.Text;
                String user_password = textBox2.Text;
                string user_information = "select * from user where user_email = '" + user_email + "'";
                indate ="许可证有效期到：" + SQLHelper.GetDataSet(user_information, CommandType.Text, null).Tables[0].Rows[0][3].ToString();
                if (user_password == SQLHelper.GetDataSet(user_information, CommandType.Text, null).Tables[0].Rows[0][2].ToString())
                {
                    //MessageBox.Show("登陆成功");
                    //验证用户
                    
                    login = true;
                    Form1 form = new Form1();
                    form.Show();
                    this.Hide();                    
        
                }
            }
            catch
            {
                MessageBox.Show("登录失败");
            }

        }
        private void textBox2_Press(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                this.button1.PerformClick();
            }
        }
        private void textBox1_Press(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                textBox2.Focus();
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {//忘记密码
            MessageBox.Show("请联系管理员");

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            textBox1.Text = "admin@qq.com";
            textBox2.Text = "admin123";
        }
    }
}
