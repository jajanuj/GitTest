using AOISystem.Utilities.IO;
using AOISystem.Utilities.Recipe;

namespace AOISystem.Utilities.Modules.Light.Common
{
    public class LightInfo
    {
        private ILight _parent;
        private IniFile _iniFile;

        public LightInfo(ILight parent, LightChannel channel)
        {
            _parent = parent;
            this.Channel = channel;
            if (parent.ParameterType == ParameterType.System)
            {
                _iniFile = new IniFile(parent.ParameterFolderPath, parent.DeviceName);
            }
            else
            {
                _iniFile = new IniFile(RecipeInfoManager.GetInstance().ActiveRecipe.GetRecipePath(), parent.DeviceName);
            }
            if (string.IsNullOrEmpty(this.Name))
            {
                this.Name = channel.ToString();
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                _name = _iniFile.GetString(this.Channel.ToString(), "Name");
                return _name;
            }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    _iniFile.WriteValue(this.Channel.ToString(), "Name", _name);
                }
            }
        }

        public LightChannel Channel { get; set; }

        public bool Switch { get; set; }

        private int _defaultValue;
        public int DefaultValue 
        {
            get 
            {
                _defaultValue = _iniFile.GetInt32(this.Channel.ToString(), "DefaultValue");
                return _defaultValue; 
            }
            set 
            {
                if (value != _defaultValue)
                {
                    _defaultValue = value;
                    _iniFile.WriteValue(this.Channel.ToString(), "DefaultValue", _defaultValue);
                }
            }
        }

        private int _actionValue;
        public int ActionValue
        {
            get
            {
                _actionValue = _iniFile.GetInt32(this.Channel.ToString(), "ActionValue");
                return _actionValue;
            }
            set
            {
                if (value != _actionValue)
                {
                    _actionValue = value;
                    _iniFile.WriteValue(this.Channel.ToString(), "ActionValue", _actionValue);
                }
            }
        }
    }
}
