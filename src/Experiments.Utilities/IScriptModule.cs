using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Experiments.Scripts
{
    public interface IScriptModule
    {
        bool Activated { get; set; }
        Dictionary<Keys, Action> Hotkeys { get; }
        Dictionary<string, Action<string[]>> Hotstrings { get; }
        string Name { get; }
        void OnKeyDown(object sender, KeyEventArgs e);
        void OnKeyUp(object sender, KeyEventArgs e);
        void OnTick(object sender, EventArgs e);
    }
}
