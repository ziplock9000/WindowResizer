using System.ComponentModel;
using System.IO;
using Newtonsoft.Json;
using WindowResizer.Library;

namespace WindowResizer
{
    public class Config
    {
        public bool DisableInFullScreen { get; set; } = true;

        public HotKeys SaveKey { get; set; } = new() { ModifierKeys = new[] { "Ctrl", "Alt" }, Key = "S" };

        public HotKeys RestoreKey { get; set; } = new() { ModifierKeys = new[] { "Ctrl", "Alt" }, Key = "R" };

        public HotKeys RestoreAllKey { get; set; } = new() { ModifierKeys = new[] { "Ctrl", "Alt" }, Key = "T" };

        public BindingList<WindowSize> WindowSizes { get; set; } = new();
    }

    public class WindowSize : IComparable<WindowSize>
    {
        public string Name { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;

        public Rect Rect { get; set; }

        public bool AutoResize { get; set; }

        public int CompareTo(WindowSize? other)
        {
            if (other is null)
            {
                return 0;
            }

            var c = string.Compare(other.Name, Name, StringComparison.Ordinal);
            return c == 0 ? string.Compare(other.Title, Title, StringComparison.Ordinal) : c;
        }
    }

    public class HotKeys
    {
        public string[] ModifierKeys { get; set; } = Array.Empty<string>();
        public string Key { get; set; } = string.Empty;
    }

    public static class ConfigHelper
    {
        public static ModifierKeys GetModifierKeys(this HotKeys hotKeys)
        {
            ModifierKeys keys = 0;
            foreach (var k in hotKeys.ModifierKeys)
            {
                if (!Enum.TryParse(k, true, out ModifierKeys m))
                    continue;

                keys |= m;
            }

            return keys;
        }

        public static Keys GetKey(this HotKeys hotKeys)
        {
            return Enum.TryParse(hotKeys.Key, true, out Keys k) ? k : new HotKeys().GetKey();
        }

        public static bool ValidateKeys(this HotKeys hotKeys) =>
            hotKeys.ModifierKeys.Any() && !string.IsNullOrEmpty(hotKeys.Key);

        public static string ToKeysString(this HotKeys hotKeys)
        {
            var str = string.Empty;
            if (hotKeys.ModifierKeys.Length > 0)
            {
                str += string.Join(" + ", hotKeys.ModifierKeys);
            }

            return $"{str} + {hotKeys.Key}";
        }
    }

    public static class ConfigLoader
    {
        private const string ConfigFile = "WindowResizer.config.json";

        private static readonly string RoamingPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "WindowResizer");

        private static readonly string PortableConfigPath = Path.Combine(
            Application.StartupPath, ConfigFile);

        private static readonly string RoamingConfigPath = Path.Combine(
            RoamingPath, ConfigFile);

        public static bool PortableMode => !File.Exists(RoamingConfigPath);

        public static string ConfigPath => PortableMode ? PortableConfigPath : RoamingConfigPath;

        public static Config Config = new();

        public static void Load()
        {
            if (!File.Exists(ConfigPath))
            {
                Save();
            }
            else
            {
                var text = File.ReadAllText(ConfigPath);
                if (string.IsNullOrWhiteSpace(text))
                {
                    return;
                }

                var r = JsonConvert.DeserializeObject<Config>(text);

                if (r is null || !r.WindowSizes.Any())
                {
                    return;
                }

                Config = r;
                var sortedInstance = new BindingList<WindowSize>(
                    Config.WindowSizes
                        .OrderBy(w => w.Name)
                        .ThenBy(w => w.Title)
                        .ToList()
                );
                Config.WindowSizes = sortedInstance;
            }
        }

        public static void Save()
        {
            var json = JsonConvert.SerializeObject(Config);
            File.WriteAllText(ConfigPath, json);
        }

        public static void Move(bool portable)
        {
            if (portable && !PortableMode)
            {
                File.Move(RoamingConfigPath, PortableConfigPath);
            }

            if (!portable && PortableMode)
            {
                new FileInfo(RoamingConfigPath).Directory?.Create();
                File.Move(PortableConfigPath, RoamingConfigPath);
            }
        }
    }
}
