using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp.Utils
{
    public class SelectCheckBoxColumn  : DataGridViewCheckBoxColumn
    {
        public SelectCheckBoxColumn()
        {
            // 在构造函数中设置所有默认属性
            this.Name = "colCheck";
            this.HeaderText = "选择";
            this.Width = 30;
            this.TrueValue = true;
            this.FalseValue = false;
        }
    }
}
