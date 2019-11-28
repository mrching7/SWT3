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
    class Cookcontroller
    {
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
        private IOutput _iOutput;
        [SetUp]
        public void Setup()
        {
                        _IPowerButton = new Button();
            _ITimeButton = new Button();
            _IStartCancelButton = new Button();
            _IDoor = new Door();
            //_ITimer = new Timer();

            FakeuserInterface = Substitute.For<IUserInterface>();
            _iOutput = Substitute.For<IOutput>();
            

            FakeTimer = Substitute.For<ITimer>();
            _ILight = new Light(_iOutput);
            _IDisplay = new Display(_iOutput);
            _IPowerTube = new PowerTube(_iOutput);
            
            _ICookController = new CookController(FakeTimer, _IDisplay, _IPowerTube, FakeuserInterface);

            _userInterface = new UserInterface(_IPowerButton, _ITimeButton, _IStartCancelButton, _IDoor, _IDisplay, _ILight, _ICookController);

        }

        [Test]
        public void TurnOn()//powertube
        {
            _IPowerButton.Press();
            _IPowerButton.Press();
            _IPowerButton.Press();
            _ITimeButton.Press();
            _IStartCancelButton.Press();
            _ICookController.StartCooking(150, 1);
            //får fejl her pga bøvl med powertube 1. fejl
            _IPowerTube.TurnOn(150);
            _iOutput.Received().OutputLine("PowerTube works with 150 %");
        }
        [Test]
        public void ShowPower()//display
        {
            _IPowerButton.Press();
            _IPowerButton.Press();
            _IPowerButton.Press();
            _ITimeButton.Press();
            _ITimeButton.Press();
            _IStartCancelButton.Press();
            FakeTimer.TimerTick += Raise.Event();



        }



    }
}
