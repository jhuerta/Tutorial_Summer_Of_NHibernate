using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DataAccessLayer
{
    internal class MyInterceptor : NHibernate.EmptyInterceptor
    {
        public override bool OnFlushDirty(
            object entity, 
            object id, 
            object[] currentState, 
            object[] previousState, 
            string[] propertyNames, 
            NHibernate.Type.IType[] types)
        {
            if (previousState != null)
            {
                for (int i = 0; i < propertyNames.Length; i++)
                {
                    string msg = string.Format(">>>>-->>>>> {0}: Previous Name = {1} , Curr Value = {2}", 
                                                propertyNames[i],
                                               previousState[i],
                                               currentState[i]);
                    //System.Diagnostics.Debug.WriteLine(msg);
                    System.Diagnostics.Trace.WriteLine(msg);
                    Trace.WriteLine(msg);

                }
            }


            return base.OnFlushDirty(entity, id, currentState, previousState, propertyNames, types);
        }

    }
}
