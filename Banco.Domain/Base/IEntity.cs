﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Banco.Domain.Base
{
    public interface IEntity<T>
    {
        T Id { get; }
    }
}
