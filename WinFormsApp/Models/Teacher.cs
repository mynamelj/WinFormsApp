using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp.Models
{

    public class Teacher : INotifyPropertyChanged
    {
        private string _tid;
        private string _tname;
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 教师编号
        /// </summary>
        public string Tid
        {
            get => _tid;
            set { if (_tid != value) { _tid = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// 教师姓名
        /// </summary>
        public string Tname
        {
            get => _tname;
            set { if (_tname != value) { _tname = value; OnPropertyChanged(); } }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    /// <summary>
    /// 重写ToString方法，用于ComboBox显示_
}
