using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace 光伏制冷
{
    public partial class FormEditCollectPoint : Form
    {
        int Code;
        string Content;//原始内容
        string AreaName;//地区名称
        string ProjectName;//项目名称
        string CollectorName;//采集器名称
        string CollectPointName;//采集点名称
        string CollectPointCode;//采集点代码
        DataAccess dataaccess = new DataAccess();

        //public string NewContent;//修改后的内容
        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="content">原始内容</param>
        /// <param name="code">要修改的选项 0 采集点名称 1 采集点编码 2 监测内容名称 3 监测内容代码 4 阈值上限修改 5 阈值下限修改</param>
        /// <param name="areaName">地区名</param>
        ///<param name="projectNam">项目名</param>
        ///<param name="collectorName">采集器名称</param>
        public FormEditCollectPoint(string content, int code, string areaName, string projectName, string collectorName, string collectPointName, string collectPointCode)
        {
            InitializeComponent();
            this.Content = content;
            this.Code = code;
            this.AreaName = areaName;
            this.ProjectName = projectName;
            this.CollectorName = collectorName;
            this.CollectPointName = collectPointName;
            this.CollectPointCode = collectPointCode;
        }

        private void FormEditCollectPoint_Load(object sender, EventArgs e)
        {
            this.txtOriginal.Text = Content;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (Code)
            {
                //采集点名称修改
                case 0:
                    ModifyCollectorPointName();
                    break;

                //采集点代码修改
                case 1:
                    ModifyCollectorPointCode();
                    break;
                //采集指标名称修改
                case 2:
                    ModifyCollectorDataName();
                    break;
                //监测内容代码修改
                case 3:
                    ModifyCollectorDataCode();
                    break;
                case 4:
                    ModifyCollectMax();
                    break;
                case 5:
                    ModifyCollectMin();
                    break;
                default: break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        //修改采集点名称
        private void ModifyCollectorPointName()
        {
            if (this.txtNew.Text != "")
            {
                if (DialogResult.OK == MessageBox.Show(string.Format("确认要将内容\"{0}\"替换为\"{1}\"", this.txtOriginal.Text, this.txtNew.Text), "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning))
                {
                    //0 判断在该地区该项目该采集器下是否已经存在该名称
                    //1 修改数据库内容
                    //2 更新主界面内容（重新加载采集点）
                    string NewContent = this.txtNew.Text;
                    string SQL_IsExist = @"select CollectPointName from CollectPointInfo where CollectPointName='" + NewContent + "' and AreaName='" + AreaName + "' and ProjectName='" + ProjectName + "' and CollectorName='" + CollectorName + "'";
                    if (dataaccess.IsExistColletorOrPoint(SQL_IsExist) == false)
                    {

                        string SQL_UpdateCollectPointInfo = @"update CollectPointInfo set CollectPointName='" + NewContent + "' where AreaName='" + AreaName + "' and ProjectName='" + ProjectName + "' and CollectorName='" + CollectorName + "' and CollectPointCode='" + CollectPointCode + "'";
                        if (dataaccess.ExeSQL(SQL_UpdateCollectPointInfo, GlobalInfo.DefaultDatabase) == 0)
                        {
                            MessageBox.Show("修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            GlobalInfo.IsEditCollectPoint = true;
                            this.Dispose();
                        }
                        else
                        { MessageBox.Show("修改失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); }


                    }
                    else
                    { MessageBox.Show("该名称已经存在！请重新填写！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                }
            }
            else
            { MessageBox.Show("请输入修改的内容！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        //修改采集点编码
        private void ModifyCollectorPointCode()
        {
            if (this.txtNew.Text != "")
            {
                if (!(Regex.IsMatch(this.txtNew.Text, @"[^0-9]")))//Regex.IsMatch(this.txtNew.Text, @"[^0-9]"如果能匹配到除了数字以外的字符
                {
                    if (DialogResult.OK == MessageBox.Show(string.Format("确认要将内容\"{0}\"替换为\"{1}\"", this.txtOriginal.Text, this.txtNew.Text), "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning))
                    {
                        //0 判断是否已经存在该名称
                        //1 修改数据库内容
                        //2 更新主界面内容（重新加载采集点）
                        string NewContent = this.txtNew.Text;
                        string SQL_IsExist = @"select CollectPointName from CollectPointInfo where CollectPointCode='" + NewContent + "'and AreaName='" + AreaName + "' and ProjectName='" + ProjectName + "' and CollectorName='" + CollectorName + "'";
                        if (dataaccess.IsExistColletorOrPoint(SQL_IsExist) == false)
                        {

                            string SQL_UpdateCollectPointInfo = @"update CollectPointInfo set CollectPointCode='" + NewContent + "' where AreaName='" + AreaName + "' and ProjectName='" + ProjectName + "' and CollectorName='" + CollectorName + "' and CollectPointName='" + CollectPointName + "'";
                            if (dataaccess.ExeSQL(SQL_UpdateCollectPointInfo, GlobalInfo.DefaultDatabase) == 0)
                            {
                                MessageBox.Show("修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                GlobalInfo.IsEditCollectPoint = true;
                                this.Dispose();
                            }
                            else
                            { MessageBox.Show("修改失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); }


                        }
                        else
                        { MessageBox.Show("该名称已经存在！请重新填写！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    }
                }
                else
                { MessageBox.Show("请输入数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); }

            }
            else
            { MessageBox.Show("请输入修改的内容！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        //采集指标名称修改
        private void ModifyCollectorDataName()
        {
            if (this.txtNew.Text != "")
            {
                if (DialogResult.OK == MessageBox.Show(string.Format("确认要将内容\"{0}\"替换为\"{1}\"", this.txtOriginal.Text, this.txtNew.Text), "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning))
                {
                    //0 判断是否已经存在该名称(不需要)(因为采集数据名称可以重复)
                    //1 修改数据库内容
                    //2 更新主界面内容（重新加载采集点）
                    string NewContent = this.txtNew.Text;
                    //string SQL_IsExist = @"select CollectPointName from CollectPointInfo where CollectDataName='" + NewContent + "'";
                    //if (dataaccess.IsExistColletorOrPoint(SQL_IsExist) == false)
                    //{

                    string SQL_UpdateCollectPointInfo = @"update CollectPointInfo set CollectDataName='" + NewContent + "' where AreaName='" + AreaName + "' and ProjectName='" + ProjectName + "' and CollectorName='" + CollectorName + "' and CollectPointCode='" + CollectPointCode + "'";
                    if (dataaccess.ExeSQL(SQL_UpdateCollectPointInfo, GlobalInfo.DefaultDatabase) == 0)
                    {
                        MessageBox.Show("修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GlobalInfo.IsEditCollectPoint = true;
                        this.Dispose();
                    }
                    else
                    { MessageBox.Show("修改失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); }


                    //}
                    //else
                    //{ MessageBox.Show("该名称已经存在！请重新填写！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                }
            }
            else
            { MessageBox.Show("请输入修改的内容！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }


        //采集指标编码修改
        private void ModifyCollectorDataCode()
        {
            if (this.txtNew.Text != "")
            {
                if (!(Regex.IsMatch(this.txtNew.Text, @"[^0-9]")))//Regex.IsMatch(this.txtNew.Text, @"[^0-9]"如果能匹配到除了数字以外的字符
                {
                    if (DialogResult.OK == MessageBox.Show(string.Format("确认要将内容\"{0}\"替换为\"{1}\"", this.txtOriginal.Text, this.txtNew.Text), "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning))
                    {
                        //0 判断是否已经存在该名称(不需要)(因为采集数据代码可以重复)
                        //1 修改数据库内容
                        //2 更新主界面内容（重新加载采集点）
                        string NewContent = this.txtNew.Text;
                        //string SQL_IsExist = @"select CollectPointName from CollectPointInfo where CollectPointCode='" + NewContent + "'";
                        //if (dataaccess.IsExistColletorOrPoint(SQL_IsExist) == false)
                        //{

                        string SQL_UpdateCollectPointInfo = @"update CollectPointInfo set CollectDataCode='" + NewContent + "' where AreaName='" + AreaName + "' and ProjectName='" + ProjectName + "' and CollectorName='" + CollectorName + "' and CollectPointName='" + CollectPointName + "'";
                        if (dataaccess.ExeSQL(SQL_UpdateCollectPointInfo, GlobalInfo.DefaultDatabase) == 0)
                        {
                            MessageBox.Show("修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            GlobalInfo.IsEditCollectPoint = true;
                            this.Dispose();
                        }
                        else
                        { MessageBox.Show("修改失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); }


                        //}
                        //else
                        //{ MessageBox.Show("该名称已经存在！请重新填写！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    }
                }
                else
                { MessageBox.Show("请输入数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); }

            }
            else
            { MessageBox.Show("请输入修改的内容！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        //阈值上限修改
        private void ModifyCollectMax()
        {
            if (this.txtNew.Text != "")
            {
                if (!(Regex.IsMatch(this.txtNew.Text, @"[^+\-0-9]")))//Regex.IsMatch(this.txtNew.Text, @"[^0-9]"如果能匹配到除了数字以外的字符
                {
                    if (DialogResult.OK == MessageBox.Show(string.Format("确认要将内容\"{0}\"替换为\"{1}\"", this.txtOriginal.Text, this.txtNew.Text), "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning))
                    {
                        //0 判断是否已经存在该名称(不需要)(因为采集数据代码可以重复)
                        //1 修改数据库内容
                        //2 更新主界面内容（重新加载采集点）
                        string NewContent = this.txtNew.Text;
                        //string SQL_IsExist = @"select CollectPointName from CollectPointInfo where CollectPointCode='" + NewContent + "'";
                        //if (dataaccess.IsExistColletorOrPoint(SQL_IsExist) == false)
                        //{

                        string SQL_UpdateCollectPointInfo = @"update CollectPointInfo set MaxValue='" + NewContent + "' where AreaName='" + AreaName + "' and ProjectName='" + ProjectName + "' and CollectorName='" + CollectorName + "' and CollectPointName='" + CollectPointName + "'";
                        if (dataaccess.ExeSQL(SQL_UpdateCollectPointInfo, GlobalInfo.DefaultDatabase) == 0)
                        {
                            MessageBox.Show("修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            GlobalInfo.IsEditCollectPoint = true;
                            this.Dispose();
                        }
                        else
                        { MessageBox.Show("修改失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); }


                        //}
                        //else
                        //{ MessageBox.Show("该名称已经存在！请重新填写！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    }
                }
                else
                { MessageBox.Show("请输入数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); }

            }
            else
            { MessageBox.Show("请输入修改的内容！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        //阈值下限修改
        private void ModifyCollectMin()
        {
            if (this.txtNew.Text != "")
            {
                if (!(Regex.IsMatch(this.txtNew.Text, @"[^+\-0-9]")))//Regex.IsMatch(this.txtNew.Text, @"[^+\-0-9]"如果能匹配到除了数字以外的字符
                //if (!(Regex.IsMatch(this.txtNew.Text, @"[^0-9]")))//Regex.IsMatch(this.txtNew.Text, @"[^0-9]"如果能匹配到除了数字以外的字符
                {
                    if (DialogResult.OK == MessageBox.Show(string.Format("确认要将内容\"{0}\"替换为\"{1}\"", this.txtOriginal.Text, this.txtNew.Text), "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning))
                    {
                        //0 判断是否已经存在该名称(不需要)(因为采集数据代码可以重复)
                        //1 修改数据库内容
                        //2 更新主界面内容（重新加载采集点）
                        string NewContent = this.txtNew.Text;
                        //string SQL_IsExist = @"select CollectPointName from CollectPointInfo where CollectPointCode='" + NewContent + "'";
                        //if (dataaccess.IsExistColletorOrPoint(SQL_IsExist) == false)
                        //{

                        string SQL_UpdateCollectPointInfo = @"update CollectPointInfo set MinValue='" + NewContent + "' where AreaName='" + AreaName + "' and ProjectName='" + ProjectName + "' and CollectorName='" + CollectorName + "' and CollectPointName='" + CollectPointName + "'";
                        if (dataaccess.ExeSQL(SQL_UpdateCollectPointInfo, GlobalInfo.DefaultDatabase) == 0)
                        {
                            MessageBox.Show("修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            GlobalInfo.IsEditCollectPoint = true;
                            this.Dispose();
                        }
                        else
                        { MessageBox.Show("修改失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); }


                        //}
                        //else
                        //{ MessageBox.Show("该名称已经存在！请重新填写！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    }
                }
                else
                { MessageBox.Show("请输入数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); }

            }
            else
            { MessageBox.Show("请输入修改的内容！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }
    }
}
