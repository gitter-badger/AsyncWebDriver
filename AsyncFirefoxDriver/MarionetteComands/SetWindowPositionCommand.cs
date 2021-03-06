﻿
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCommunicationLib.Communication.MarionetteComands
{
    public class SetWindowPositionCommand : MarionetteDebuggerCommand
    {
        public SetWindowPositionCommand(int x, int y, int id = 0, string commandName = "setWindowPosition") : base(id, commandName)
        {
            X = x;
            Y = y;
        }
        public int X { get; set; }
        public int Y { get; set; }
        public override void ProcessResponse(JToken response)
        {
            base.ProcessResponse(response);
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(
                new object[]
                {
                   0,
                   Id,
                   CommandName,
                   new {
                        x = X,
                        y = Y
                   }

                });

        }
    }
}
