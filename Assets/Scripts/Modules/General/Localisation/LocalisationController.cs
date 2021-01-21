using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Modules.General.Settings;
using UnityEngine;
using Zenject;

namespace Modules.General.Localisation
{
    public class LocalisationController : ILocalisationController
    {
        [Inject] private readonly ISettingsController _settingsController;
        [Inject] private readonly Settings _settings;
        
        private const char LineSeparator = '\n';
        private readonly string[] _fieldSeparator = { "\",\"" };

        private readonly Dictionary<string, string> _localisationDictionary;

        public LocalisationController()
        {
            _localisationDictionary = new Dictionary<string, string>();
        }

        public void LoadLocalisationData()
        {
            if (_localisationDictionary.Count != 0)
                _localisationDictionary.Clear();
            
            foreach (var file in _settings.localization)
            {
                GetLanguageValues(file);
            }
        }

        private void GetLanguageValues(TextAsset file)
        {
            var lines = file.text.Split(LineSeparator);
            var attributeIndex = -1;
            var headers = lines[0].Split(_fieldSeparator, StringSplitOptions.None);

            for (var i = 0; i < headers.Length; i++)
            {
                if (headers[i].Contains(_settingsController.Language.ToString()))
                {
                    attributeIndex = i;
                    break;
                }
            }

            var parser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            for (var i = 1; i < lines.Length; i++)
            {
                var line = lines[i];
                var fields = parser.Split(line);

                for (var k = 0; k < fields.Length; k++)
                {
                    fields[k] = fields[k].TrimStart(' ', '"');
                    fields[k] = fields[k].Replace("\"","");
                }
                if (fields.Length > attributeIndex)
                {
                    var key = fields[0];
                    if(_localisationDictionary.ContainsKey(key))
                        continue;
                    
                    if (key.Substring(0,2) == "/-")
                        continue;

                    var value = fields[attributeIndex];
                    _localisationDictionary.Add(key, value);
                }
            }
        }

        public string GetLanguageValue(string key)
        {
            return _localisationDictionary[key];
        }

        [Serializable]
        public class Settings
        {
            public List<TextAsset> localization;
        }
    }
}