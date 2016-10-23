using System;
using System.Windows.Forms;
using GTA;
using System.Collections.Generic;

namespace Experiments.Scripts.Hotkey
{
    /// <summary>
    /// Adapted from
    /// <see href="https://github.com/crosire/scripthookvdotnet/wiki/Code-Snippets#simple-commandhotkey-script"/>Simple Command/Hotkey Script</see>
    /// </summary>
    public class Hotkey : Script
    {
        public Hotkey()
        {
            this.Tick += onTick;
            this.KeyUp += onKeyUp;
            this.KeyDown += onKeyDown;
        }

        private void onTick(object sender, EventArgs e)
        {

        }

        private void onKeyUp(object sender, KeyEventArgs e)
        {
        }

        private void onKeyDown(object sender, KeyEventArgs e)
        {
        }
    }
}