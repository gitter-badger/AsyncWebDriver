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
    public class ClearImportedScriptsCommand : MarionetteDebuggerCommand
    {
        /**
        * Clear all scripts that are imported into the JS evaluation runtime.
        *
        * Scripts can be imported using the {@code importScript} command.
        */
        public ClearImportedScriptsCommand(int id = 0, string commandName = "clearImportedScripts") : base(id, commandName)
        {
        }

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

                });

        }

    }
}
