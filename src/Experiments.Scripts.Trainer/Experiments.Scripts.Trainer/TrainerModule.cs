using GTA;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Experiments.Scripts.Trainer
{
    public class TrainerModule : IScriptModule
    {
        public bool Activated { get; set; }

        public Dictionary<Keys, Action> Hotkeys { get; }

        public Dictionary<string, Action<string[]>> Hotstrings { get; }

        public string Name => nameof(TrainerModule);

        public TrainerModule()
        {
            Activated = true;

            const string WANTED_LEVEL = "wanted_level";
            Hotstrings = new Dictionary<string, Action<string[]>>
            {
                { WANTED_LEVEL, (args) =>
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
                }
            };
        }

        public void OnKeyDown(object sender, KeyEventArgs e)
        {
        }

        public void OnKeyUp(object sender, KeyEventArgs e)
        {
        }

        public void OnTick(object sender, EventArgs e)
        {
        }
    }
}
