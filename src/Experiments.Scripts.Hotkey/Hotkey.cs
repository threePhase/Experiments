using System;
using System.Windows.Forms;
using GTA;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Experiments.Scripts.CarSpawn;
using Experiments.Scripts.Trainer;

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
                new CreateVehicle(),
                new TrainerModule()
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

            Action<string[]> selectedAction = null;
            foreach(var module in _activatedModules)
            {
                Action<string[]> action =
                    module.Hotstrings.FirstOrDefault(hotstring => hotstring.Key == command[0]).Value;
                if (action != null)
                {
                    selectedAction = action;
                    break;
                }
            }

            if (selectedAction != null)
            {
                selectedAction(args);
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
            Action selectedAction = null;
            foreach(var module in _activatedModules)
            {
                Action action =
                    module.Hotkeys.FirstOrDefault(hotstring => hotstring.Key == e.KeyCode).Value;
                if (action != null)
                {
                    selectedAction = action;
                    break;
                }
            }

            selectedAction?.Invoke();
        }
    }
}