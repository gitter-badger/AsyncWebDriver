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
    public class ClickElementCommand : MarionetteDebuggerCommand
    {
        public ClickElementCommand(string elementId, int id = 0, string commandName = "clickElement") : base(id, commandName)
        {
            ElementId = elementId;
        }

        public string ElementId { get; set; }

        public override void ProcessResponse(JToken response)
        {
            base.ProcessResponse(response);
        }

        public override string ToString()
        {
            return $"[0, {Id}, \"{CommandName}\", {{\"id\": \"{ElementId}\"}} ]";
          
        }
    }
}
