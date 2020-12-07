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
        [Then("the Following Integration Event Is Sent")]
        public void TheFollowingIntegrationEventIsSent(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                Assert.AreEqual(row["Event"], ((MessageBusStub)ExternalServices.MessageBus).IntegrationEvents.First().GetType().Name);
            }
        }

        [Then("no Integration Event is sent")]
        public void NoIntegrationEventIsSent()
        {
            Assert.AreEqual(0, ((MessageBusStub)ExternalServices.MessageBus).IntegrationEvents.Count());
        }
    }
}
