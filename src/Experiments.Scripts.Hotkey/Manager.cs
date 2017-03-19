using System;
using System.Windows.Forms;
using GTA;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Experiments.Scripts;
using Experiments.Scripts.CarSpawn;
using Experiments.Scripts.Trainer;

namespace Experiments.Manager
{
    /// <summary>
    /// Adapted from
    /// <see href="https://github.com/crosire/scripthookvdotnet/wiki/Code-Snippets#simple-commandhotkey-script"/>Simple Command/Hotkey Script</see>
    /// </summary>
    public class Manager : Script
    {
        private IEnumerable<IScriptModule> _activatedModules =>
            _modules.Where(m => m.Activated);
        private bool _enabled;
        private IList<IScriptModule> _modules;
        private const string _statusHeading = "Modules";
        private readonly UIText _statusText;

        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                _enabled = value;

                UpdateHUDStatus();
            }
        }

        public Manager()
        {
            _modules = new List<IScriptModule>
            {
                new CarSpawnModule(),
                new TrainerModule()
            };

            _statusText =
                new UIText($"{_statusHeading}:\n\t ~r~None",
                    new Point(10, 10), 0.4f, Color.WhiteSmoke, 0, false);

            Tick += OnTick;
            KeyDown += OnKeyDown;
            Interval = 0;
        }

        private void OpenPrompt()
        {
            string result = Game.GetUserInput(60);
            if (string.IsNullOrWhiteSpace(result))
            {
                return;
            }
            string[] command = result.Split(' ');
            var args = command.Skip(1).ToArray();

            const string MODULE = "mod";
            if (command[0] == MODULE)
            {
                if (args == null || args.Length != 2)
                {
                    UI.Notify($"Usage: {MODULE} (name) (status) where (name) is the name of the module and (status) is \"deactivate\" or \"activate\"");
                    return;
                }

                HandleModuleActivation(args);
                return;
            }

            if (args.Length > 0 &&
                _activatedModules.Any(m => m.Name.ToLower() == command[0].ToLower()))
            {
                string moduleCommand = args[0];
                args = args.Skip(1).ToArray();
                Action<string[]> selectedAction = null;
                foreach(var module in _activatedModules)
                {
                    Action<string[]> action = module.Hotstrings
                        .FirstOrDefault(hotstring => hotstring.Key == moduleCommand)
                        .Value;

                    if (action != null)
                    {
                        selectedAction = action;
                        break;
                    }
                }

                selectedAction?.Invoke(args);
            }
            else
            {
                UI.Notify("Unknown command");
            }
        }

        private void HandleModuleActivation(string[] args)
        {

            string moduleName = string.Empty;
            if (!string.IsNullOrWhiteSpace(args[0]))
            {
                moduleName = args[0].ToLower();
            }
            else
            {
                UI.Notify($"Invalid Module Name: {args[0]}");
                return;
            }

            var module =
                _modules.FirstOrDefault(m => m.Name.ToLower() == moduleName);

            if (module != null)
            {
                const string ACTIVATE = "activate";
                const string DEACTIVATE = "deactivate";
                bool status = module.Activated;
                string newStatusValue = args[1];
                if (newStatusValue == ACTIVATE)
                {
                    module.Activated = true;
                    UI.Notify($"{module.Name} Activated");
                    UpdateHUDStatus();
                    return;
                }
                else if (newStatusValue == DEACTIVATE)
                {
                    module.Activated = false;
                    UI.Notify($"{module.Name} Deactivated");
                    UpdateHUDStatus();
                    return;

                }
                else
                {
                    UI.Notify($"Invalid Status: {newStatusValue}");
                    return;
                }
            }
            else
            {
                UI.Notify($"Could Not Find Module: {moduleName}");
                return;
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

        private void UpdateHUDStatus()
        {
            IEnumerable<string> names =
                _modules.Select(m =>
                    $"{m.Name}: {(m.Activated ? "~g~ON" : "~r~OFF")}");

            _statusText.Caption =
                $"{_statusHeading}:\n\t" +
                $"{(Enabled ? string.Join("\n\t~w~", names) : "~r~None")}";
        }
    }
}