// <copyright file="VehicleQueriesUnitTest.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Contoso.Registration.Application.Mappers;
using Contoso.Registration.Application.Stubs.Database;
using Contoso.Registration.Infrastructure.Database;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Contoso.Registration.Application.Queries
{
    /// <summary>
    /// Unit test for vehicle queries.
    /// </summary>
    [TestClass]
    public class VehicleQueriesUnitTest
    {
        private IMapper mapper;

        /// <summary>
        /// Setup.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            this.mapper = new MapperConfiguration(cfg => cfg.AddProfile<MapProfile>()).CreateMapper();
        }

        /// <summary>
        /// Query test.
        /// </summary>
        [TestMethod]
        public void Find_PassingParameter_ReturnData()
        {
            VehiclesQueries vehiclesQueries = new VehiclesQueries(this.GetDatabaseMock().Object, this.mapper);
            Model.Vehicle parameters = new Model.Vehicle()
            {
                Name = "F50",
                Brand = "Ferrari",
                Doors = 2,
                Passengers = 2,
                Category = "Sport",
                Transmission = "Manual",
                Consume = 10,
                Emission = 20,
            };
            IEnumerable<Model.Vehicle> result = vehiclesQueries.Find(parameters);

            Assert.IsTrue(result.Count() > 0);
            Assert.AreEqual("F50", result.First().Name);
            Assert.AreEqual("Ferrari", result.First().Brand);
            Assert.AreEqual("Sport", result.First().Category);
            Assert.AreEqual("Manual", result.First().Transmission);
            Assert.AreEqual(2, result.First().Doors);
            Assert.AreEqual(2, result.First().Passengers);
            Assert.AreEqual(10, result.First().Consume);
            Assert.AreEqual(20, result.First().Emission);
        }

        /// <summary>
        /// Query test.
        /// </summary>
        [TestMethod]
        public void Find_NoDataExists_ReturnEmptyList()
        {
            VehiclesQueries vehiclesQueries = new VehiclesQueries(this.GetDatabaseMock().Object, this.mapper);
            Model.Vehicle parameters = new Model.Vehicle()
            {
                Name = "Uno",
                Brand = "Fiat",
            };
            IEnumerable<Model.Vehicle> result = vehiclesQueries.Find(parameters);

            Assert.AreEqual(0, result.Count());
        }

        private Mock<IDatabaseQueries> GetDatabaseMock()
        {
            Mock<IDatabaseQueries> mock = new Mock<IDatabaseQueries>();
            mock
                .Setup(m => m.GetQuery<Infrastructure.Model.Vehicle>())
                .Returns(this.GetQueryableMock().Object);

            return mock;
        }

        private Mock<IQueryable<TableEntityAdapter<Infrastructure.Model.Vehicle>>> GetQueryableMock()
        {
            IQueryable<TableEntityAdapter<Infrastructure.Model.Vehicle>> data = new DBData().GetData();
            Mock<IQueryable<TableEntityAdapter<Infrastructure.Model.Vehicle>>> queryableMock = new Mock<IQueryable<TableEntityAdapter<Infrastructure.Model.Vehicle>>>();
            queryableMock.As<IQueryable<TableEntityAdapter<Infrastructure.Model.Vehicle>>>().Setup(q => q.Provider).Returns(data.Provider);
            queryableMock.As<IQueryable<TableEntityAdapter<Infrastructure.Model.Vehicle>>>().Setup(q => q.Expression).Returns(data.Expression);
            queryableMock.As<IQueryable<TableEntityAdapter<Infrastructure.Model.Vehicle>>>().Setup(q => q.ElementType).Returns(data.ElementType);
            queryableMock.As<IQueryable<TableEntityAdapter<Infrastructure.Model.Vehicle>>>().Setup(q => q.GetEnumerator()).Returns(data.GetEnumerator());
            return queryableMock;
        }
    }
}
