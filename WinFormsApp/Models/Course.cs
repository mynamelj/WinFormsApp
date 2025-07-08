using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp.Models
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class Course : INotifyPropertyChanged
    {
        private string _cid;
        private string _cname;
        private string _tid;
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 课程编号
        /// </summary>
        public string Cid
        {
            get => _cid;
            set { if (_cid != value) { _cid = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// 课程名称
        /// </summary>
        public string Cname
        {
            get => _cname;
            set { if (_cname != value) { _cname = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// 教师编号
        /// </summary>
        public string Tid
        {
            get => _tid;
            set { if (_tid != value) { _tid = value; OnPropertyChanged(); } }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
