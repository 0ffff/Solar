using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 光伏制冷
{
    public partial class CompanyConfiguration : Form
    {
        //数据库函数
        DataAccess dataaccess = new DataAccess();

        public CompanyConfiguration()
        {
            InitializeComponent();
        }

        private void CompanyConfiguration_Load(object sender, EventArgs e)
        {
            this.CenterToParent();
            LoadDGV();//加载表格
        }

        private void LoadDGV()
        {
            this.DGVCompany.Rows.Clear();
            string sql = "select * from CompanyInfo";
            DataTable dt = dataaccess.GetDataTable(sql, GlobalInfo.DefaultDatabase);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.DGVCompany.Rows.Insert(i,i+1,dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString());
            }
        } 
        /// <summary>
        /// 添加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.txtCompany.Text!="" && this.txtLogin.Text!="" && this.txtPsw.Text!="")
            {
                try
                {
                    string sqlcheck = "select count(*) from CompanyInfo where CompanyName ='" + this.txtCompany.Text + "'";
                    DataTable dt=dataaccess.GetDataTable(sqlcheck, GlobalInfo.DefaultDatabase);
                    if (dt.Rows[0][0].ToString()=="0")
                    {
                        string sqlLogin = "select * from CompanyInfo where CompanyLogin ='"+this.txtLogin.Text+"'";
                        DataTable dtLogin = dataaccess.GetDataTable(sqlLogin, GlobalInfo.DefaultDatabase);
                        if (dtLogin.Rows.Count==0)//没有相同的登陆名
                        {
                            string sqlString = "insert into CompanyInfo(CompanyName,CompanyLogin,CompanyPsw)values('" + this.txtCompany.Text + "','" + this.txtLogin.Text + "','" + this.txtPsw.Text + "')";
                            dataaccess.ExeSQL(sqlString, GlobalInfo.DefaultDatabase);
                            MessageBox.Show("添加成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDGV();
                        }
                        else//登录名已存在
                        {
                            MessageBox.Show("此用户名已存在，请使用其他用户名!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        
                    }
                    else
                    {
                        MessageBox.Show("公司名已存在!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                catch
                {

                }

            }
            else
            {
                MessageBox.Show("信息填写不完整！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (this.DGVCompany.SelectedRows.Count!=0)
            {
                if (MessageBox.Show("确定要删除此公司？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    string companyName = this.DGVCompany.SelectedRows[0].Cells[1].Value.ToString();
                    string companyLogin = this.DGVCompany.SelectedRows[0].Cells[2].Value.ToString();
                    string companyPsw = this.DGVCompany.SelectedRows[0].Cells[3].Value.ToString();

                    string sql = "delete from CompanyInfo where CompanyName = '" + companyName + "' and CompanyLogin = '" + companyLogin + "' and CompanyPsw = '" + companyPsw + "'";
                    dataaccess.ExeSQL(sql, GlobalInfo.DefaultDatabase);
                    MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDGV();
                }

               
            }
            else
            {
                MessageBox.Show("请现在表格中选择要删除的行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.DGVCompany.SelectedRows.Count!=0)
            {
                string companyName = this.DGVCompany.SelectedRows[0].Cells[1].Value.ToString();
                string companyLogin = this.DGVCompany.SelectedRows[0].Cells[2].Value.ToString();
                string companyPsw = this.DGVCompany.SelectedRows[0].Cells[3].Value.ToString();


                FormCompanyAlter fmcompanyAlter = new FormCompanyAlter(companyName, companyLogin, companyPsw);
              
                fmcompanyAlter.ShowDialog();
                LoadDGV();
            }
            else
            {
                MessageBox.Show("请现在表格中选择要修改的行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           
        }
    }
}
