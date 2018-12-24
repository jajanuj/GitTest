using AOISystem.Utilities.Recipe;
using System;
using System.Windows.Forms;

namespace AOISystem.Utilities
{
    public enum Language { English, TraditionalChinese }

    public enum Direction { Left, Right, Up, Down }

    //v1.0.5.0 新增前站後站列舉
    public enum StationLocation { Front, Rear }        

    public enum OnOffLine { Off, On }

    public enum RotationDirection { CCW = 0, CW = 1 }

    public enum JogSpeed { Micro, Low, Mid, High, Max }

    public enum PositiveNegative { None, Positive, Negative }

    public enum StopMode { None, Pause, Stop }

    public enum OperationStatus 
    { 
        Initialize, 
        Start, 
        Pause,
        Stop,
        Warning,
        Alarm,
        AlarmReset,
        ClearOff
    }

    public enum MachineStatus { Idle, Run, Down }

    public enum ParameterType { System, Recipe }

    public enum PathMode { Folder, File }

    public enum LotEndType {LotMerge, LotEnd}

    public enum ReportLocation {Local, Upload}

    public enum CopyType {Move, Copy}

    [Serializable]
    public class SystemDefine
    {
        private static string _application_data_folder_path = @"D:\ACC\";
        /// <summary> 程式資料夾名稱 </summary>
        public static string APPLICATION_DATA_FOLDER_PATH { get { return _application_data_folder_path; } }

        private static string _system_data_folder_name = "System";
        /// <summary> 系統資料夾路徑 </summary>
        public static string SYSTEM_DATA_FOLDER_PATH { get { return string.Format(@"{0}\{1}\", APPLICATION_DATA_FOLDER_PATH, _system_data_folder_name); } }

        private static string _recipe_data_folder_name = "Recipe";
        /// <summary> Recipe資料夾路徑 </summary>
        public static string RECIPE_DATA_FOLDER_PATH { get { return string.Format(@"{0}\{1}\", APPLICATION_DATA_FOLDER_PATH, _recipe_data_folder_name); } }

        /// <summary> 作用中Recipe資料夾名稱 </summary>
        public static string ACTIVE_RECIPE_DATA_FOLDER_PATH { get { return RecipeInfoManager.GetInstance().ActiveRecipe.GetRecipePath(); } }

        private static string _modules_data_folder_name = "Modules";
        /// <summary> 模組資料夾路徑 </summary>
        public static string MODULES_DATA_FOLDER_PATH { get { return string.Format(@"{0}\{1}\", SYSTEM_DATA_FOLDER_PATH, _modules_data_folder_name); } }

        private static string _base_image_folder_name = "BaseImage";
        /// <summary> Base Image資料夾路徑 </summary>
        public static string BASE_IMAGE_FOLDER_PATH { get { return string.Format(@"{0}\{1}\", APPLICATION_DATA_FOLDER_PATH, _base_image_folder_name); } }

        private static string _database_folder_name = "Database";
        /// <summary> Database資料夾路徑 </summary>
        public static string DATABASE_FOLDER_PATH { get { return string.Format(@"{0}\{1}\", APPLICATION_DATA_FOLDER_PATH, _database_folder_name); } }

        /// <summary> 開發者帳號名稱 </summary>
        public static readonly string DEVELOPER_USER_NAME = "developer";

        /// <summary> 開發者帳號密碼 </summary>
        public static readonly string DEVELOPER_USER_PASSWORD = "acc";

        /// <summary> 報表暫存資料夾名稱  </summary>
        public static string TEMP_REPORT_PATH { get { return string.Format(@"{0}\{1}\", APPLICATION_DATA_FOLDER_PATH, "TempReport"); } }

        /// <summary> 中控台程式路徑 </summary>
        public static string APPLICATION_CONSOLE_PATH { get { return string.Format(@"{0}\{1}", Application.StartupPath, "AOISystem.ApplicationConsole.exe"); } }

        /// <summary> 設定程式資料夾路徑 </summary>
        public static void InitializeApplicationDataFolderPath(string path)
        {
            _application_data_folder_path = path;
        }

        /// <summary> 設定系統資料夾名稱 </summary>
        public static void InitializeSystemDataFolderName(string name)
        {
            _system_data_folder_name = name;
        }

        /// <summary> 設定Recipe資料夾名稱 </summary>
        public static void InitializeRecipeDataFolderName(string name)
        {
            _recipe_data_folder_name = name;
        }

        /// <summary> 設定模組資料夾名稱 </summary>
        public static void InitializeModulesDataFolderName(string name)
        {
            _modules_data_folder_name = name;
        }

        /// <summary> 設定Base Image資料夾名稱 </summary>
        public static void InitializeBaseImageFolderName(string name)
        {
            _base_image_folder_name = name;
        }

        /// <summary> 設定Database資料夾名稱 </summary>
        public static void InitializeDatabaseFolderName(string name)
        {
            _database_folder_name = name;
        }

        public static readonly string BIN_SETTINGS_FILE_NAME = "BinSettings.txt";

        public static readonly string SAFEDOOR_SETTINGS_FILE_NAME = "SafeDoor.txt";
    }
}
