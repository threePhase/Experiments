using System;
using System.Windows.Forms;
using GTA;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

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
            Tick += OnTick;
            KeyDown += OnKeyDown;
            Interval = 0;

            _statusText =
                new UIText("Hotkeys: ~r~OFF",
                    new Point(10, 10), 0.4f, Color.WhiteSmoke, 0, false);

            _hotstrings = new Dictionary<string, Action<string[]>>();
            SetupHotstrings();

            _hotkeys = new Dictionary<Keys, Action>();
            SetupHotkeys();
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
            _hotkeys.Add(Keys.Oemtilde, OpenPrompt);
        }

        private void SetupHotstrings()
        {
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

            const string CLEAN_UP = "clean_up";
            _hotstrings.Add(CLEAN_UP, (args) =>
            {
                if (args == null || args.Length > 0)
                {
                    UI.Notify($"Usage: {CLEAN_UP}");
                    return;
                }

                World.GetAllEntities()
                     .ToList()
                     .ForEach(e => e.Delete());
                UI.Notify("Cleaned up entities");
            });
        }
    }
}