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
            IPowerButton = new Button();
            ITimeButton = new Button();
            ICancelButton = new Button();
            IDoor=new Door();
            ILight = Substitute.For<ILight>();
            IDisplay = Substitute.For<IDisplay>();
            ICookController = Substitute.For<ICookController>();
            
            userInterface=new UserInterface(IPowerButton, ITimeButton, ICancelButton, IDoor, IDisplay, ILight, ICookController);
        }


        [Test]
        public void OnPowerPressed_Test()
        {
            //test 1 powerbtn og display trykket 1 gang
            IPowerButton.Press();
            //default værdier
            IDisplay.Received(1).ShowPower(50);
        }

        [Test]
        public void OnPowerPressed_Test2()
        {
            //test 2 powerbtn og display trykket 2 gange
            IPowerButton.Press();
            IPowerButton.Press();
            //showpower er 100 da power er +50
            IDisplay.Received(1).ShowPower(100);
        }

        [Test]
        public void OnTimePressed()
        {
            //test 3 trykker på tidsknappen
            IPowerButton.Press();
            ITimeButton.Press();
            
            IDisplay.Received(1).ShowTime(1, 0);
        }
        [Test]
        public void OnTimePressed2()
        {
            //test 4 trykker på tidsknappen 2 gange
            IPowerButton.Press();
            ITimeButton.Press();
            ITimeButton.Press();
            //burde være 2 minutter og 0 nul sekunder 
            IDisplay.Received(1).ShowTime(2, 0);
        }

        [Test]
        public void OnStartCancelPressed()
        {
            //test 4 sætter styrken 
            IPowerButton.Press();
            //tænder for maskinen
            ICancelButton.Press();
            //asserter på at lys slukker
            ILight.Received(1).TurnOff();
            IDisplay.Received(1).Clear();
        }

        [Test]
        public void OnStartCancelPressed2()
        {
            //test 5 sætter styrken 
            IPowerButton.Press();
            ITimeButton.Press();
            //tænder for maskinen
            ICancelButton.Press();
            //asserter på at den starter med styrke på 50 og 1 minut
            ICookController.Received().StartCooking(50, 60);
        }
        [Test]
        public void OnStartCancelPressed3()
        {
            //test 6 sætter styrken 
            IPowerButton.Press();
            ITimeButton.Press();
            //tænder for maskinen og slukker for den igen
            ICancelButton.Press();
            ICancelButton.Press();
            //asserter på at den stopper
            ICookController.Received().Stop();

        }

        //skal teste døren nu
        [Test]
        public void OnDoorOpened()
        {
            //test 7 åbner døren
            IDoor.Open();
            //lyset tænder men får fejl????
            ILight.Received(1).TurnOn();
        }

        [Test]
        public void OnDoorOpened1()
        {
            //test 8 åbner døren
            IPowerButton.Press();
            IDoor.Open();
            //skal clear display
            IDisplay.Received().Clear();
        }
        [Test]
        public void OnDoorOpened2()
        {
            //test 9 åbner døren
            IPowerButton.Press();
            ITimeButton.Press();
            IDoor.Open();
            //tænder lys
            ILight.Received().TurnOn();
        }



        [Test]
        public void OnDoorClosed()
        {
            //test 9 åbner døren
            IPowerButton.Press();
            ITimeButton.Press();
            IDoor.Open();
            IDoor.Close();
            //tænder lys
            ILight.Received().TurnOff();
        }

        [Test]
        public void cookingIsDone()
        {
            //test 10 sætter indstillinger
            IPowerButton.Press();
            ITimeButton.Press();
            //tænder for maskinen
            ICancelButton.Press();
            //test cooking is done på en eller anden måde???
            userInterface.CookingIsDone();
            ILight.Received().TurnOff();
        }



    }
}
