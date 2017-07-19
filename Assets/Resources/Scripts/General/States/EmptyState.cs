using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

class EmptyState : State
{
    public EmptyState() : base(Type.Empty)
    {

    }

    public override void Update()
    {

    }
}