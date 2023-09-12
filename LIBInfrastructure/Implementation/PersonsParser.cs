using LIBDomainEntities.Entities;
using LIBInfrastructure.Core;
using LIBUtilities.Core;
using System;
using System.Collections.Generic;

namespace LIBInfrastructure.Implementation
{
    public class PersonsParser : IParser<Persons>
    {
        public PersonsParser()
        {
        }

        public Persons CreateEntity(object[] item)
        {
            return new Persons()
            {
                Id = Convert.ToInt32(item[0]),
                SSN = Convert.ToInt32(item[1]),
                Name = item[2].ToString(),
                Born = Convert.ToDateTime(item[3]),
                State = Convert.ToBoolean(item[4]),
                File = item[5] == LIBUtilities.Core.DBNull.Value ? null : (byte[])item[5],
                CreateDate = Convert.ToDateTime(item[6]),
            };
        }

        public Dictionary<string, object> ToDictionary(Persons entity)
        {
            var data = new Dictionary<string, object>();

            data["Id"] = entity.Id;

            data["SSN"] = entity.SSN;

            if (!string.IsNullOrEmpty(entity.Name))
                data["Name"] = entity.Name;

            data["Born"] = entity.Born;
            data["State"] = entity.State;

            if (entity.File != null)
                data["File"] = EncodingHelper.ToString(entity.File);

            data["CreateDate"] = entity.CreateDate;

            return data;

        }

        public Persons ToEntity(Dictionary<string, object> data)
        {
            var entity = new Persons();
            entity.Id = Convert.ToInt32(data["Id"]);

            if (data.ContainsKey("SSN"))
                entity.SSN = Convert.ToInt32(data["SSN"]);

            if (data.ContainsKey("Name"))
                entity.Name = data["Name"].ToString();

            entity.Born = Convert.ToDateTime(data["Born"]);
            entity.State = Convert.ToBoolean(data["State"]);

            if (data.ContainsKey("File") && data["File"]  != null)
                entity.File = EncodingHelper.ToBytes(data["File"].ToString());

            entity.CreateDate = Convert.ToDateTime(data["CreateDate"]);

            return entity;
        }

        public bool Validate(Persons entity)
        {
            return true;
        }
    }
}