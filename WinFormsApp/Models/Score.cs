using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WinFormsApp.Models
{

    public class Score : INotifyPropertyChanged
    {
        private string _sid;
        private string _cid;
        private decimal? _score;
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 学生编号
        /// </summary>
        public string Sid
        {
            get => _sid;
            set { if (_sid != value) { _sid = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// 课程编号
        /// </summary>
        public string Cid
        {
            get => _cid;
            set { if (_cid != value) { _cid = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// 成绩分数
        /// </summary>
        public decimal? score
        {
            get => _score;
            set { if (_score != value) { _score = value; OnPropertyChanged(); } }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
