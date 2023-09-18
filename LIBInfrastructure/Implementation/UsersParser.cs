using LIBDomainEntities.Entities;
using LIBInfrastructure.Core;
using LIBInfrastructure.Implementation;
using System;
using System.Collections.Generic;
using System.Text;

namespace LIBInfrastructure.Implementation
{
    public class UsersParser : IParser<Users>
    {
        private IParser<Persons> IParserPersons;

        public UsersParser()
        {
            IParserPersons = new PersonsParser();
        }
        public Users CreateEntity(object[] item)
        {
            return new Users()
            {
                Id = Convert.ToInt32(item[0]),
                Person = Convert.ToInt32(item[1]),
                _Person = new Persons()
                {
                    Id = Convert.ToInt32(item[1]),
                    SSN = Convert.ToInt32(item[2]),
                    Name = item[3].ToString(),
                    File = item[4] == LIBUtilities.Core.DBNull.Value ? null : (byte[])item[4],
                },
                State = Convert.ToBoolean(item[5]),
                Email = item[6].ToString(),
                Password = item[7].ToString()
            };
        }

        public Dictionary<string, object> ToDictionary(Users entity)
        {
            var data = new Dictionary<string, object>();
            data["Id"] = entity.Id;
            data["Person"] = entity.Person;
            data["State"] = entity.State;
            if (!string.IsNullOrEmpty(entity.Email))
                data["Email"] = entity.Email;
            if (!string.IsNullOrEmpty(entity.Password))
                data["Password"] = entity.Password;
            if (entity._Person != null)
                data["_Person"] = IParserPersons.ToDictionary(entity._Person);

            return data;
        }

        public Users ToEntity(Dictionary<string, object> data)
        {
            var entity = new Users();
            entity.Id = Convert.ToInt32(data["Id"]);

            if (data.ContainsKey("Person"))
                entity.Person = Convert.ToInt32(data["Person"].ToString());
            if (data.ContainsKey("State"))
                entity.State = Convert.ToBoolean(data["State"].ToString());
            if (data.ContainsKey("Email"))
                entity.Email = data["Email"].ToString();
            if (data.ContainsKey("Password"))
                entity.Password = data["Password"].ToString();
            if (data.ContainsKey("_Person"))
                entity._Person = IParserPersons.ToEntity((Dictionary<string, object>)data["_Person"]);
            
            return entity;
        }

        public bool Validate(Users entity)
        {
            return true;
        }
    }
}
