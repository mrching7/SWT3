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
    public class UserInterfaceTest
    {
        //private IUserInterface _ReceivedEventArgs;

        //UUT er UI
        private IButton _IPowerButton;
        private IButton _ITimeButton;
        private IButton _IStartCancelButton;
        private IDoor _IDoor;
        private IUserInterface FakeuserInterface;
        private IUserInterface _userInterface;
        private ILight _ILight;
        private ITimer FakeTimer;
        //private ITimer _ITimer;
        private IDisplay _IDisplay;
        private ICookController _ICookController;
        private IPowerTube _IPowerTube;

        [SetUp]
        public void Setup()
        {
            _IPowerButton = new Button();
            _ITimeButton = new Button();
            _IStartCancelButton = new Button();
            _IDoor = new Door();
            //_ITimer = new Timer();

            FakeuserInterface = Substitute.For<IUserInterface>();

            
            FakeTimer = Substitute.For<ITimer>();
            _ILight = Substitute.For<ILight>();
            _IDisplay = Substitute.For<IDisplay>();
            _IPowerTube = Substitute.For<IPowerTube>();
            
            _ICookController = new CookController(FakeTimer, _IDisplay, _IPowerTube, FakeuserInterface);

            _userInterface = new UserInterface(_IPowerButton, _ITimeButton, _IStartCancelButton, _IDoor, _IDisplay, _ILight, _ICookController);

        }

        [Test]
        public void StartCooking()
        {
            _IPowerButton.Press();
            _ITimeButton.Press();
            _IStartCancelButton.Press();
            _ICookController.StartCooking(50,1);

            _IPowerTube.Received().TurnOn(50);
        }

        [Test]
        public void Stop()
        {
            _IPowerButton.Press();
            _ITimeButton.Press();
            _IStartCancelButton.Press();
            _IStartCancelButton.Press();

            _ICookController.Stop();

            _IPowerTube.Received().TurnOff();
        }

        [Test]
        public void OnTimerExpired()
        {
            _ICookController.StartCooking(50, 1);

            FakeTimer.Expired += Raise.Event();

            Assert.That(FakeTimer.TimeRemaining, Is.EqualTo(0));
        }

        [Test]
        public void OnTimerTick()
        {
            _IPowerButton.Press();
            _ITimeButton.Press();
            _IStartCancelButton.Press();

            FakeTimer.TimerTick += Raise.Event();
            _IDisplay.Received().ShowTime(1, 0);

        }

    }
}
