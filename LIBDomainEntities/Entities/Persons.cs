using LibUtilities.Core;
using System;

namespace LIBDomainEntities.Entities
{
    public partial class Persons : Bindable
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private int _SSN;
        public int SSN
        {
            get { return _SSN; }
            set { SetProperty(ref _SSN, value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private int _Type;
        public int Type
        {
            get { return _Type; }
            set { SetProperty(ref _Type, value); }
        }
        private System.DateTime _Born;
        public System.DateTime Born
        {
            get { return _Born; }
            set { SetProperty(ref _Born, value); }
        }

        private bool _State;
        public bool State
        {
            get { return _State; }
            set { SetProperty(ref _State, value); }
        }

        private byte[] _file;
        public byte[] File
        {
            get { return _file; }
            set { SetProperty(ref _file, value); }
        }

        private System.DateTime _createDate;
        public System.DateTime CreateDate
        {
            get { return _createDate; }
            set { SetProperty(ref _createDate, value); }
        }

        public object Clone() { return this.MemberwiseClone(); }
    }
}