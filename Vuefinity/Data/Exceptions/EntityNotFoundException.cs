﻿namespace Vuefinity.Data.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string type, int id)
            : base($"{type} with id: {id} could not be found.")
        {
        }
    }
}
