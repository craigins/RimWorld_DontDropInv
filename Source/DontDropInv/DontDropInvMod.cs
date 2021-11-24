using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;
using HarmonyLib;
using System.Reflection;
using RimWorld.Planet;

namespace DontDropInv
{
  [StaticConstructorOnStartup]
  public class DontDropInvMod
  {
    static DontDropInvMod()
    {
      Log.Message("Patching For Don't Drop Inventory"); //Outputs "Hello World!" to the dev console.
      var harmony = new Harmony("com.craigins.rimworld.dontdropinv");
      harmony.PatchAll(Assembly.GetExecutingAssembly());
      Log.Message("Done Patching"); //Outputs "Hello World!" to the dev console.
    }
  }
}
