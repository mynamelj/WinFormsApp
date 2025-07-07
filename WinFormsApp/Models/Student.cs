using System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp.Models
{
    /// <summary>
    /// 代表学生信息的实体类，对应数据库中的 student 表
    /// </summary>
    public class Student : INotifyPropertyChanged
    {
        private string _sid;
        private string _sname;
        private DateTime? _sage;
        private string? _ssex;
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// 学生编号
        /// </summary>
        public string sid
        {
            get => _sid;
            set { if (_sid != value) { _sid = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// 学生姓名
        /// </summary>
        public string?sname
        {
            get => _sname;
            set { if (_sname != value) { _sname = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// 出生年月
        /// </summary>
        public DateTime? sage 
        {
            get => _sage;
            set { if (_sage != value) { _sage = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// 性别
        /// </summary>
        public string? ssex 
        {
           get => _ssex;
           set { if (_ssex != value) { _ssex = value; OnPropertyChanged(); } }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}