﻿using System;
using MicrowaveOvenClasses.Interfaces;

namespace MicrowaveOvenClasses.Boundary
{
    public class PowerTube : IPowerTube
    {
        private IOutput myOutput;

        private bool IsOn = false;

        public PowerTube(IOutput output)
        {
            myOutput = output;
        }

        public void TurnOn(int power)
        {
            if (power < 1 || 750 < power)
            {
                throw new ArgumentOutOfRangeException("power", power, "Must be between 1 and 100 % (incl.)");
                //det her skal måske rettes til watt i stedet 4 procent 
            }

            if (IsOn)
            {
                throw new ApplicationException("PowerTube.TurnOn: is already on");
            }

            myOutput.OutputLine($"PowerTube works with {power} W");
            IsOn = true;
        }

        public void TurnOff()
        {
            if (IsOn)
            {
                myOutput.OutputLine($"PowerTube turned off");
            }

            IsOn = false;
        }
    }
}