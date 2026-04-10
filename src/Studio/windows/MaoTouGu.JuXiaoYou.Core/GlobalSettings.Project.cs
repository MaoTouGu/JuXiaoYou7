// ----------------------------------------------------------
//            文件：GlobalSettings.Project.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月13日 19:28
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Studio.Database.Core;

namespace MaoTouGu.JuXiaoYou
{
    partial class GlobalSettings
    {
        public const string FileName_Server = "JuXiaoYou-V7-Server.Json";

        public static void LoadProjectSettings()
        {
            ProjectSettings = JSON.FromFile<ProjectSettings>(FileNameOfProjectSettings, () => new ProjectSettings
            {
                Projects       = new ViewList<Project>(),
                DefaultProject = null,
                AutoSave       = false,
                Local          = null,
            });

            //
            // 避免出错。
            if (ProjectSettings.Projects is null)
            {
                ProjectSettings.Projects = new ViewList<Project>();
                SaveProjectSettings();
            }
        }

        public static void SaveProjectSettings()
        {
            JSON.ToFile(FileNameOfProjectSettings, ProjectSettings);
        }


        public static ProjectSettings ProjectSettings { get; set; }
    }

    public sealed class Credential : ObservableObject
    {
        private string _account;
        private bool   _isDefault;


        public string Account
        {
            get => _account;
            set => SetValue(ref _account, value);
        }

        /// <summary>
        /// 获取或设置 <see cref="IsDefault"/> 属性。
        /// </summary>
        public bool IsDefault
        {
            get => _isDefault;
            set => SetValue(ref _isDefault, value);
        }

        public string Password { get; set; }
    }

    public sealed class Project : DatabaseObject
    {
        private string _name;
        private string _url;
        private bool   _isOnline;

        public bool IsOnline
        {
            get => _isOnline;
            set => SetValue(ref _isOnline, value);
        }
        public string Url
        {
            get => _url;
            set => SetValue(ref _url, value);
        }
        public string Name
        {
            get => _name;
            set => SetValue(ref _name, value);
        }

        public ViewList<Credential> Credentials { get; set; }
    }


    public class ProjectSettings : ObservableObject
    {
        private bool _autoSave;

        private string     _defaultProject;
        private Credential _local;

        /// <summary>
        /// 自动保存
        /// </summary>

        public string DefaultProject
        {
            get => _defaultProject;
            set => SetValue(ref _defaultProject, value);
        }

        public bool AutoSave
        {
            get => _autoSave;
            set => SetValue(ref _autoSave, value);
        }

        /// <summary>
        /// 本地模式的用户账户。
        /// </summary>
        public Credential Local
        {
            get => _local;
            set => SetValue(ref _local, value);
        }

        public ViewList<Project> Projects { get; set; }
    }
}