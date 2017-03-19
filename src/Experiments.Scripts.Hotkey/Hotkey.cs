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
        private IEnumerable<IScriptModule> _activatedModules =>
            _modules.Where(m => m.Activated);
        private bool _enabled;
        private IList<IScriptModule> _modules;
        private const string _name = "Experiments";
        private readonly UIText _statusText;

        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                _statusText.Caption =
                    $"{_name}: {(value ? "~g~ON" : "~r~OFF")}";

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
                new UIText($"{_name}: ~r~OFF",
                    new Point(10, 10), 0.4f, Color.WhiteSmoke, 0, false);

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
            var args = command.Skip(1).ToArray();

            // TODO: move to separate module
            const string WANTED_LEVEL = "wanted_level";
            if (command[0] == WANTED_LEVEL)
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
            }
            else
            {
                var action = _activatedModules.Select(m =>
                    m.Hotstrings.FirstOrDefault(hotstring => command[0] == hotstring.Key)
                             .Value)
                    .FirstOrDefault();

                if (action != null)
                {
                    action(args);
                }
                else
                {
                    UI.Notify("Unknown command");
                }
            }
        }

        private void OnTick(object sender, EventArgs e)
        {
            _statusText.Draw();

            if (!Enabled)
            {
                return;
            }

            foreach (var module in _activatedModules)
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

            // setup console shortcut
            if (e.KeyCode == Keys.Oemtilde)
            {
                OpenPrompt();
            }

            // setup module shortcuts
            _activatedModules.Select(m =>
                m.Hotkeys.FirstOrDefault(hotkey => e.KeyCode == hotkey.Key)
                         .Value)
                .FirstOrDefault()?.Invoke();
        }
    }
}