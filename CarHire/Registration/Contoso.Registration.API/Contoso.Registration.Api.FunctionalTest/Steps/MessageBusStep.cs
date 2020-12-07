// <copyright file="MessageBusStep.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System.Linq;
using BoDi;
using Contoso.Registration.FunctionalTest.Services;
using Contoso.Registration.FunctionalTest.Stubs;
using Contoso.Registration.Infrastructure.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace Contoso.Registration.FunctionalTest.Steps
{
    [Binding]
    internal class MessageBusStep
    {
        [Then("Then The Following Message Is Sent")]
        public void TheFollowingMessageIsSent(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                Assert.AreEqual(row["Event"], ((MessageBusStub)ExternalServices.MessageBus).IntegrationEvents.First().GetType().Name);
            }
        }
    }
}
