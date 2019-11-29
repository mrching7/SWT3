using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Smtp;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    [TestFixture]
    class Cookcontroller
    {
        private UserInterface _ReceivedEventArgs;
        private IButton IPowerButton;
        private IButton ITimeButton;
        private IButton ICancelButton;
        private IDoor IDoor;
        private IUserInterface userInterface;
        private ILight ILight;
        private ITimer ITimer;
        private IDisplay IDisplay;
        private ICookController ICookController;

        [SetUp]
        public void Setup()
        {
            IPowerButton = new Button();
            ITimeButton = new Button();
            ICancelButton = new Button();
            IDoor = new Door();

            IDoor = Substitute.For<IDoor>();
            ILight = Substitute.For<ILight>();
            IDisplay = Substitute.For<IDisplay>();
            ICookController = Substitute.For<ICookController>();

            userInterface = new UserInterface(IPowerButton, ITimeButton, ICancelButton, IDoor, IDisplay, ILight, ICookController);
        }
    }
}
