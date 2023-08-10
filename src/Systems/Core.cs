using Vintagestory.API.Common;
using Vintagestory.API.Server;
using Vintagestory.API.Util;
using Vintagestory.GameContent;

[assembly: ModInfo(name: "Drop Resin Anyway", modID: "dropresinanyway")]

namespace DropResinAnyway;

public class Core : ModSystem
{
    public override bool ShouldLoad(EnumAppSide forSide) => forSide == EnumAppSide.Server;

    public override void StartServerSide(ICoreServerAPI api)
    {
        base.StartServerSide(api);

        api.RegisterBlockBehaviorClass("DropResinAnyway", typeof(BlockBehaviorDropResinAnyway));
        api.World.Logger.Event("started '{0}' mod", Mod.Info.Name);
    }

    public override void AssetsFinalize(ICoreAPI api)
    {
        foreach (Block block in api.World.Blocks)
        {
            if (!block.HasBehavior<BlockBehaviorHarvestable>())
            {
                continue;
            }

            if (block.GetBehavior<BlockBehaviorHarvestable>().harvestedStack.Code != new AssetLocation("resin"))
            {
                continue;
            }

            block.BlockBehaviors = block.BlockBehaviors.Append(new BlockBehaviorDropResinAnyway(block));
        }
    }
}
