using System;
using System.Collections.Generic;
using System.Text;
using MoodTracker.Models;

namespace MoodTracker.ViewModel
{
    public class MoodEventArgs: EventArgs
    {
        public MoodEntry MoodEntry { get; set; }

        public MoodEventArgs() { }
        public MoodEventArgs(MoodEntry entry) {  MoodEntry = entry; }
    }
}
