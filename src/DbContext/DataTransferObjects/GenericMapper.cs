using System;
using System.Collections.Generic;
using Mimo.Dbcontext.DataTransferObjects.Mapper;
using System.Data;
using System.Reflection;

namespace Mimo.Dbcontext.DataTransferObjects
{
    public class GenericMapper<T> : BaseDataTransferObject<T> where T : class, new()
    {
        public System.Type DtoType { get; set; }
        private bool _isInitialized = false;
        private IList<PropertyOrdinalMap> _propertyOrdinalMappings;
        private readonly Func<T> _createInstance = delegate { return new T(); };


        public GenericMapper(System.Type type)
        {
            DtoType = type;
        }


        protected override void PopulateOrdinals(IDataReader reader)
        {
            PopulatePropertyOrdinalMappings(reader);
        }

        public void PopulatePropertyOrdinalMappings(IDataReader reader)
        {
            // Get the PropertyInfo objects for our DTO type and map them to 
            // the ordinals for the fields with the same names in our reader.  
            _propertyOrdinalMappings = new List<PropertyOrdinalMap>();
            PropertyInfo[] properties = DtoType.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                PropertyOrdinalMap map = new PropertyOrdinalMap();
                map.Property = property;
                try
                {
                    map.Ordinal = reader.GetOrdinal(property.Name);
                    _propertyOrdinalMappings.Add(map);
                }
                catch { }
            }
        }

        private class PropertyOrdinalMap
        {
            public PropertyInfo Property { get; set; }
            public int Ordinal { get; set; }
        }

        public override T EntityMapping(IDataReader dataReader)
        {
            if (!_isInitialized) { InitializeMapper(dataReader); }
            T dto = _createInstance();
            foreach (var map in _propertyOrdinalMappings)
            {
                if (!dataReader.IsDBNull(map.Ordinal))
                    map.Property.SetValue(dto, dataReader.GetValue(map.Ordinal), null);
            }
            return dto;
        }

    }
}
