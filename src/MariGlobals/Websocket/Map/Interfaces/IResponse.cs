﻿using System;

namespace MariGlobals.Websocket.Map.Interfaces
{
    public interface IResponse : IExchange
    {
        int StatusCode { get; set; }

        string Message { get; set; }

        Exception Exception { get; set; }
    }
}