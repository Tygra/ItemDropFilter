using System;
using Terraria;
using TShockAPI;
using TerrariaApi.Server;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace ItemDropFilter
{
    [ApiVersion(1, 21)]
    public class ItemDropFilter : TerrariaPlugin
    {
       
        public override Version Version
        {   get { return new Version(1, 0); }   }

        public override string Author
        {   get { return "Tygra"; } }

        public override string Name
        {   get { return "ItemDropFilter"; }   }

        public override string Description
        {   get { return "Banning item drops mapwide."; }   }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                ServerApi.Hooks.NetGetData.Deregister(this, OnGetData);

            base.Dispose(disposing);
        }

        public ItemDropFilter(Main game)
            : base(game)
        {
        }
        public override void Initialize()
        {

            ServerApi.Hooks.NetGetData.Register(this, OnGetData);
        }
        private void OnGetData(GetDataEventArgs args)
        {
            if (args.Handled)
                return;

            if (args.MsgID == PacketTypes.ItemDrop)
            {
                TSPlayer player = TShock.Players[args.Msg.whoAmI];
                if (!player.Group.HasPermission("geldar.dropban"))
                    {
                        args.Handled = true;
                        player.SendErrorMessage("You are not allowed to drop items under level 30.");
                        player.SendErrorMessage("Rebind your drop key to avoid the destruction of more items");
                        player.SendErrorMessage("Check the rules to know what items are allowed to be dropped");
                        player.SendErrorMessage("www.forum.geldar.net");
                    }
            }
        }
    }

}
