using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Castle.Core.Smtp;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;
using Timer = MicrowaveOvenClasses.Boundary.Timer;


namespace Microwave.Test.Integration
{
    [TestFixture]
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

            FakeuserInterface = Substitute.For<IUserInterface>();
            _iOutput = Substitute.For<IOutput>();


            FakeTimer = new MicrowaveOvenClasses.Boundary.Timer();
            _ILight = new Light(_iOutput);
            _IDisplay = new Display(_iOutput);
            _IPowerTube = new PowerTube(_iOutput);

            _ICookController = new CookController(FakeTimer, _IDisplay, _IPowerTube, FakeuserInterface);

            _userInterface = new UserInterface(_IPowerButton, _ITimeButton, _IStartCancelButton, _IDoor, _IDisplay, _ILight, _ICookController);

        }

        [Test]
        public void StartCooking()
        {
            _IPowerButton.Press();
            _IPowerButton.Press();
            _IPowerButton.Press();
            _ITimeButton.Press();
            _IStartCancelButton.Press();
            //Testen fejler da powertube måler i procent og vi sætter styrken i watt skal ændre boundary values til 700
            _iOutput.Received().OutputLine("PowerTube works with 150 W");
        }
        [Test]
        public void OnTimerTick()
        {
            _IPowerButton.Press();
            _IPowerButton.Press();
            _IPowerButton.Press();
            _ITimeButton.Press();
            _ITimeButton.Press();
            _IStartCancelButton.Press();
            //Fik fejl her da da jeg fik negativ tid rettede fejlen og testen virker
            Thread.Sleep(3000);
            _iOutput.Received().OutputLine("Display shows: 01:57");
        }

        //laver test der skal fejle
        //[Test]
        //public void OnTimerTick2()
        //{
        //    _IPowerButton.Press();
        //    _IPowerButton.Press();
        //    _IPowerButton.Press();
        //    _ITimeButton.Press();
        //    _ITimeButton.Press();
        //    _IStartCancelButton.Press();
        //    //kvalitets sikrer at den virker og tester at funktionen skal fejle
        //    Thread.Sleep(2000);
        //    _iOutput.Received().OutputLine("Display shows: 01:57");
        //}
        [Test]
        public void powerTubeOff()
        {
            _IPowerButton.Press();
            _IPowerButton.Press();
            _IPowerButton.Press();
            _ITimeButton.Press();
            _IStartCancelButton.Press();
            //testcase tilventer i 1 min assert på at powertube er turnoff() f.eks
            Thread.Sleep(61000);
            
            _iOutput.Received().OutputLine("PowerTube turned off");

        }




    }
}
