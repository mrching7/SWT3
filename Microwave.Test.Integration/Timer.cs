using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using Castle.Core.Smtp;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class Timer
    {
        private ILight FakeLight;
        private ITimer FakeTimer;
        private IDisplay FakeDisplay;
        private IPowerTube FakePowerTube;
        private IButton _IPowerButton;
        private IButton _ITimeButton;
        private IButton _IStartCancelButton;
        private IDoor _IDoor;
        private ICookController _ICookController;
        private IUserInterface _UserInterface;



        [SetUp]
        public void Setup()
        {
            _IPowerButton = new Button();
            _ITimeButton = new Button();
            _IStartCancelButton = new Button();
            _IDoor = new Door();

            FakeTimer = Substitute.For<ITimer>();
            FakeDisplay = Substitute.For<IDisplay>();
            FakePowerTube = Substitute.For<IPowerTube>();
            FakeLight = Substitute.For<ILight>();

            _ICookController = new CookController(FakeTimer, FakeDisplay, FakePowerTube);
            _UserInterface = new UserInterface(_IPowerButton, _ITimeButton, _IStartCancelButton, _IDoor, FakeDisplay, FakeLight, _ICookController);
        }

        [Test]
        public void StartCooking()
        {
            _ICookController.StartCooking(50, 1);

            FakeTimer.Received().Start(1);
        }

        [Test]
        public void StopCooking()
        {
            _ICookController.Stop();

            FakeTimer.Received().Stop();
        }
    }
}
