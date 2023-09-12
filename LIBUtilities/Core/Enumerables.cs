using System;
using System.Collections.Generic;
using System.Text;

namespace LIBUtilities.Core
{
    public enum Types
    {
        Persons, Users
    }

    public enum Action
    {
        NONE,
        NEW,
        UPDATED,
        SAVED,
        MODIFY,
        DELETED,
        ERROR
    }
}
