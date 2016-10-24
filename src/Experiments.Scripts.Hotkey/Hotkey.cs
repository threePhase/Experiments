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
            this.Tick += onTick;
            this.KeyDown += onKeyDown;
            this.Interval = 0;

            this._statusText =
                new UIText("Hotkeys: ~r~OFF",
                    new Point(10, 10), 0.4f, Color.WhiteSmoke, 0, false);

            this._hotstrings = new Dictionary<string, Action<string[]>>();
            this.setupHotstrings();

            this._hotkeys = new Dictionary<Keys, Action>();
            this.setupHotkeys();
        }

        private void openPrompt()
        {
            string result = Game.GetUserInput(20);
            if (string.IsNullOrWhiteSpace(result))
            {
                return;
            }
            string[] command = result.Split(' ');
            if (this._hotstrings.ContainsKey(command[0]))
            {
                this._hotstrings[command[0]](command.Skip(1).ToArray());
            }
            else
            {
                UI.Notify("Unknown command");
            }
        }

        private void onTick(object sender, EventArgs e)
        {
            this._statusText.Draw();
            if (!this.Enabled)
            {
                return;
            }
        }

        private void onKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                this.Enabled = !this.Enabled;
            }

            if (!this.Enabled)
            {
                return;
            }

            foreach (var hotkey in this._hotkeys.Keys.Where(hotkey => e.KeyCode == hotkey))
            {
                this._hotkeys[hotkey]();
            }
        }

        private void setupHotkeys()
        {
            this._hotkeys.Add(Keys.Oemtilde, openPrompt);
        }

        private void setupHotstrings()
        {
            const string WANTED_LEVEL = "wanted_level";
            this._hotstrings.Add(WANTED_LEVEL, (args) =>
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