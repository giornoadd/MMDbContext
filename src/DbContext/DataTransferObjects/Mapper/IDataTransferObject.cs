﻿using System.Collections.Generic;
using System.Data;

namespace Mimo.Dbcontext.DataTransferObjects.Mapper
{
    public interface IDataTransferObject<T>
    {
        IEnumerable<T> GetList(IDataReader reader);
        T EntityMapping(IDataReader dataRow);
    }
}
