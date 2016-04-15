using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_Work_16_var
{
    public abstract class Clock
    {
        public virtual void Hour_Next() { }

        public virtual void Minute_Next() { }

        public virtual void Second_Next() { }

        public virtual void Start() { }

        public virtual void Stop() { }

        public virtual void Hour_Prev() { }

        public virtual void Minute_Prev() { }

        public virtual void Second_Prev() { }

    }
}
