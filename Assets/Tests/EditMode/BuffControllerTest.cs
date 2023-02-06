using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BuffControllerTest
{
    // A Test behaves as an ordinary method
    [Test]
    [TestCase(Status.Seeking)]
    [TestCase(Status.None)]
    [TestCase(Status.Slow)]
    public void SetStatusTest(Status type)
    {
        StatusManager controller = new StatusManager();
        controller.CurrentStatus = type;

        Assert.AreEqual(type, controller.CurrentStatus);
    }
}
