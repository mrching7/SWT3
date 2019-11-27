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
    public class ButtonTestIntegration
    {
        private UserInterface _ReceivedEventArgs;

        //UUT er UI
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
            IPowerButton=new Button();
            ITimeButton = new Button();
            ICancelButton = new Button();
            IDoor = Substitute.For<IDoor>();
            ILight = Substitute.For<ILight>();
            IDisplay = Substitute.For<IDisplay>();
            ICookController = Substitute.For<ICookController>();
            userInterface=new UserInterface(IPowerButton, ITimeButton, ICancelButton, IDoor, IDisplay, ILight, ICookController);
        }

        [Test]
        public void OnPowerPressed_Test()
        {
            IPowerButton.Press();
            //default værdier
            IDisplay.ShowTime(1, 0);
        }


        [Test]
        public void OnTimePressed()
        {
            ITimeButton.Press();
            //Default værdi på 50
            IDisplay.ShowPower(50);
        }

        [Test]
        public void OnStartCancelPressed()
        {
            //Laver korrekte indstillinger
            ICancelButton.Press();
            IDisplay.Clear();
            ILight.TurnOff();
            //tænder systemet 
            ICancelButton.Press();
            //Default værdi på 50, og default tid
            ICookController.StartCooking(50, 60);
            //slukker systemet igen
            ICancelButton.Press();
            //kalder på sluk 
            ICookController.Stop();
        }
        


    }
}
