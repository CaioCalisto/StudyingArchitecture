// <copyright file="DBData.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Cosmos.Table;

namespace Contoso.Registration.Application.Stubs.Database
{
    internal class DBData
    {
        public IQueryable<TableEntityAdapter<Infrastructure.Model.Vehicle>> GetData()
        {
            var data = new List<TableEntityAdapter<Infrastructure.Model.Vehicle>>()
            {
                new TableEntityAdapter<Infrastructure.Model.Vehicle>()
                {
                    OriginalEntity = new Vehicle("F50", "Ferrari", "Sport", 2, 2, "Manual", 10, 20),
                },
            };

            return data.AsQueryable();
        }
    }
}
