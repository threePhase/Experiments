using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Experiments.Scripts
{
    public interface IScriptModule
    {
        Dictionary<Keys, Action> Hotkeys { get; }
        Dictionary<string, Action<string[]>> Hotstrings { get; }
        void OnKeyDown(object sender, KeyEventArgs e);
        void OnKeyUp(object sender, KeyEventArgs e);
        void OnTick(object sender, EventArgs e);
    }
}
