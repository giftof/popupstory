using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;

namespace Popup.Framework
{
    public interface IConverterToModel<T>
    {
        T DataConvert(IDataReader data);
    }
}
