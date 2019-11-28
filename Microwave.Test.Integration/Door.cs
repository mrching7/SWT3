using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class DoorTestIntegration
    {

        private UserInterface _sut;

        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;

        private IPowerTube _powerTube;

        private ICookController _cookController;
        private ILight _light;
        private IDisplay _display;
        private IDoor _door;

        [SetUp]
        public void SetUp()
        {
            // Init fakes
            _cookController = Substitute.For<ICookController>();
            _light = Substitute.For<ILight>();
            _display = Substitute.For<IDisplay>();

            // Init includes
            _door = new Door();
            _powerButton = new Button();
            _timeButton = new Button();
            _startCancelButton = new Button();

            // Init testing
            _sut = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);
        }

        [Test]
        public void DoorOpens_LightOn()
        {
            _door.Open();
            _light.Received(1).TurnOn();
        }
    }
}
