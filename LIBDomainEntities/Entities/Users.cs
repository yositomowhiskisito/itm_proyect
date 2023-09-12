using LIBDomainEntities.Entities;
using LibUtilities.Core;
using System;

namespace LIBDomainEntities.Entities
{
    public partial class Users : Bindable
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        public int person;
        public int Person
        {
            get { return person; }
            set { SetProperty(ref person, value); }
        }

        public Persons _person;
        public Persons _Person
        {
            get { return _person; }
            set { SetProperty(ref _person, value); }
        }

        public bool state;
        public bool State
        {
            get { return state; }
            set { SetProperty(ref state, value); }
        }

        public string email;
        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }

        public string password;
        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }

        public object Clone() { return this.MemberwiseClone(); }
    }
}
