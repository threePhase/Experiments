using System;
using System.Windows.Forms;
using GTA;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Experiments.Scripts.CarSpawn;

namespace Experiments.Scripts.Hotkey
{
    /// <summary>
    /// Adapted from
    /// <see href="https://github.com/crosire/scripthookvdotnet/wiki/Code-Snippets#simple-commandhotkey-script"/>Simple Command/Hotkey Script</see>
    /// </summary>
    public class Hotkey : Script
    {
        private readonly Dictionary<Keys, Action> _hotkeys;
        private readonly Dictionary<string, Action<string[]>> _hotstrings;
        private readonly UIText _statusText;
        private bool _enabled;
        private IList<IScriptModule> _modules;

        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                _statusText.Caption =
                    $"Hotkeys: {(value ? "~g~ON" : "~r~OFF")}";

                _enabled = value;
            }
        }

        public Hotkey()
        {
            _modules = new List<IScriptModule>
            {
                new CreateVehicle()
            };

            _statusText =
                new UIText("Hotkeys: ~r~OFF",
                    new Point(10, 10), 0.4f, Color.WhiteSmoke, 0, false);

            _hotstrings = new Dictionary<string, Action<string[]>>();
            SetupHotstrings();

            _hotkeys = new Dictionary<Keys, Action>();
            SetupHotkeys();

            Tick += OnTick;
            KeyDown += OnKeyDown;
            Interval = 0;
        }

        private void OpenPrompt()
        {
            string result = Game.GetUserInput(20);
            if (string.IsNullOrWhiteSpace(result))
            {
                return;
            }
            string[] command = result.Split(' ');
            if (_hotstrings.ContainsKey(command[0]))
            {
                _hotstrings[command[0]](command.Skip(1).ToArray());
            }
            else
            {
                UI.Notify("Unknown command");
            }
        }

        private void OnTick(object sender, EventArgs e)
        {
            _statusText.Draw();

            if (!Enabled)
            {
                return;
            }

            foreach (var module in _modules)
            {
                module.OnTick(sender, e);
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                Enabled = !Enabled;
            }

            if (!Enabled)
            {
                return;
            }

            foreach (var hotkey in _hotkeys.Keys.Where(hotkey => e.KeyCode == hotkey))
            {
                _hotkeys[hotkey]();
            }
        }

        private void SetupHotkeys()
        {
            // setup console shortcut
            _hotkeys.Add(Keys.Oemtilde, OpenPrompt);

            // setup module shortcuts
            foreach(var module in _modules)
            {
                foreach(var hotkey in module.Hotkeys)
                {
                    _hotkeys.Add(hotkey.Key, hotkey.Value);
                }
            }
        }

        private void SetupHotstrings()
        {
            // setup module hotstrings
            foreach(var module in _modules)
            {
                foreach(var hotstring in module.Hotstrings)
                {
                    _hotstrings.Add(hotstring.Key, hotstring.Value);
                }
            }

            // TODO: move to separate module
            const string WANTED_LEVEL = "wanted_level";
            _hotstrings.Add(WANTED_LEVEL, (args) =>
            {
                if (args == null || args.Length != 1)
                {
                    UI.Notify($"Usage: {WANTED_LEVEL} (level) where (level) is an int between 0 and 5");
                    return;
                }

                int currentLevel = Game.Player.WantedLevel;
                if (args[0] != null)
                {
                    int.TryParse(args[0], out currentLevel);
                }
                Game.Player.WantedLevel = currentLevel;
                UI.Notify("Wanted Level Updated");
            });
        }
    }
}