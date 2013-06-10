using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CoombuPhoneApp.Models
{
    public class ApiResponse<T>
    {
        private IDictionary<string, object> _meta;
        
        [DataMember]
        public IDictionary<string, object> Meta         
        {
            get { return _meta; }

            set 
            {
                _meta = value;            
            }
        }

        
        private T _data;
        
        [DataMember]
        public T Data 
        {
            get { return _data; }
            set
            {
                _data = value;
            }
        }

        public ApiResponse() 
        {
            Meta = new Dictionary<string, object>();
        }
        public ApiResponse(T data) 
        {
            Meta = new Dictionary<string, object>();
            Data = data;
        }

        public ApiResponse(IDictionary<string,object> meta, T data)
        {
            Meta = meta;
            Data = data;
        }

        public void AddMetatData(string key, object metadata)
        {
            if (Meta.ContainsKey(key))
            {
                Meta[key] = metadata;
            }
            else
            {
                Meta.Add(key, metadata);
            }
        }
    }
}
