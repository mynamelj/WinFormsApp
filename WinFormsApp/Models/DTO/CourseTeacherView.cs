using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace WinFormsApp.Models.DTO
{
    // 你可以把这个类放在项目的一个新文件里，或者暂时先放在窗体代码文件的底部。

    public class CourseTeacherView : INotifyPropertyChanged
    {
        // 1. 为每个公共属性添加私有支持字段
        private string _cid;
        private string? _cname;
        private string? _tid;
        private string? _tname;

        // INotifyPropertyChanged 接口要求的事件
        public event PropertyChangedEventHandler? PropertyChanged;

        // --- 公共属性 ---

        /// <summary>
        /// 课程ID
        /// </summary>
        public string Cid
        {
            get => _cid;
            // 2. 在 set 访问器中实现“检查-赋值-通知”逻辑
            set
            {
                if (_cid != value)
                {
                    _cid = value;
                    OnPropertyChanged(); // 调用通知方法
                }
            }
        }

        /// <summary>
        /// 课程名称
        /// </summary>
        public string? Cname
        {
            get => _cname;
            set
            {
                if (_cname != value)
                {
                    _cname = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 教师ID
        /// </summary>
        public string? Tid
        {
            get => _tid;
            set
            {
                if (_tid != value)
                {
                    _tid = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 教师姓名
        /// </summary>
        public string? Tname
        {
            get => _tname;
            set
            {
                if (_tname != value)
                {
                    _tname = value;
                    OnPropertyChanged();
                }
            }
        }
        /// <summary>
        /// 触发 PropertyChanged 事件的核心方法
        /// </summary>
        /// <param name="propertyName">属性名称，[CallerMemberName]会自动获取调用此方法的属性名</param>
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
