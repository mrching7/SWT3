﻿using MicrowaveOvenClasses.Interfaces;

namespace MicrowaveOvenClasses.Boundary
{
    public class Display : IDisplay
    {
        private IOutput myOutput;

        public Display(IOutput output)
        {
            myOutput = output;
        }

        public void ShowTime(int min, int sec)
        {
            myOutput.OutputLine($"Display shows: {min:D2}:{sec:D2}");
        }

        public void ShowPower(int power)
        {   //her er det watt lige før i powertube var det procent hmmmm
            myOutput.OutputLine($"Display shows: {power} W");
        }

        public void Clear()
        {
            myOutput.OutputLine($"Display cleared");
        }
    }
}