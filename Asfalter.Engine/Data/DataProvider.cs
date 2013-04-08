using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Configuration;
using Asfalter.Common.Dao;
using Asfalter.Engine.Data;
using System.Data.EntityClient;

namespace Asfalter.Engine
{
    internal class DataProvider
    {
        public unit InsertUnit(Guid systemId)
        {
            using (var context = new Entities())
            {
                unit existUnit = GetUnitByID(systemId);
                if (existUnit != null)
                    return existUnit;

                unit newUnit = new unit()
                {
                    systemId = systemId.ToString(),
                    created = DateTime.Now
                };

                context.unit.Add(newUnit);
                context.SaveChanges();

                return newUnit;
            }
        }

        public unit GetUnitByID(Guid id)
        {
            using (var context = new Entities())
            {
                unit item = context.unit.SingleOrDefault(u => u.systemId == id.ToString());
                if (item != null)
                    return item;
            }
            return null;
        }

        public void InsertUnitRecord(UnitRecord record, unit ownerUnit)
        {
            using (var context = new Entities())
            {
                unit_record newRecord = new unit_record()
                {
                    unit = ownerUnit,
                    currentTime = record.CurrnetTime,
                    weight = record.Weight,
                    speed = record.Speed
                };
                context.unit_record.Add(newRecord);
                context.SaveChanges();


                
            }
        }
    }
}
