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
        private ITimer FakeTimer;
        private IDisplay FakeDisplay;
        private IPowerTube FakePowerTube;
        private ICookController _ICookController;



        [SetUp]
        public void Setup()
        {
            FakeTimer = Substitute.For<ITimer>();
            FakeDisplay = Substitute.For<IDisplay>();
            FakePowerTube = Substitute.For<IPowerTube>();

            _ICookController = new CookController(FakeTimer, FakeDisplay, FakePowerTube);
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
