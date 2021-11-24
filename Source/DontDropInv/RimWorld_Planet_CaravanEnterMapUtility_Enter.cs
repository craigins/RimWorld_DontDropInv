using System;
using System.Collections.Generic;
using RimWorld.Planet;
using Verse;
using HarmonyLib;
using System.Reflection;
using System.Reflection.Emit;

namespace DontDropInv
{
  [HarmonyPatch(typeof(CaravanEnterMapUtility))]
  //Caravan caravan,
  //Map map,
  //CaravanEnterMode enterMode,
  //CaravanDropInventoryMode dropInventoryMode = CaravanDropInventoryMode.DoNotDrop,
  //bool draftColonists = false,
  //Predicate<IntVec3> extraCellValidator = null
  [HarmonyPatch("Enter", new Type[] { 
    typeof(Caravan),
    typeof(Map),
    typeof(CaravanEnterMode),
    typeof(CaravanDropInventoryMode),
    typeof(bool),
    typeof(Predicate<IntVec3>)
  })]
  class RimWorld_Planet_CaravanEnterMapUtility_Enter
  {
        static RimWorld_Planet_CaravanEnterMapUtility_Enter()
    {
      Log.Message("\tRimWorld_Planet_CaravanEnterMapUtility_Enter Start");
    }
    static void LogInstruction(int i, CodeInstruction code)
    {
      object obj = code.operand;
      string typeinfo = "";
      if (obj != null)
      {
        Type otype = obj.GetType();
        Assembly assem = Assembly.GetAssembly(otype);
        typeinfo = string.Format(" ( {0} => {1} )", otype.Name, assem.CodeBase);
      }
      Log.Message(string.Format("{0}: {1} | {2}{3}", i, code.opcode.Name, code.operand, typeinfo));
    }

    static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
    {
      List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
      Log.Message(string.Format("\t\tPatching RimWorld_Planet_CaravanArrivalAction_Enter_Arrived({0})", codes.Count));
      //ldc.i4.0 -- do not drop
      //Ldarg_3 -- CaravanDropInventoryMode dropInventoryMode
      for (int i = 0; i < codes.Count; i++)
      {
        if (codes[i].opcode == OpCodes.Ldarg_3)
        {
          Log.Message("\t\t\tSwapping drop type at " + i);
          codes[i].opcode = OpCodes.Ldc_I4_0;
        }
      }
      return codes;
    }
  }
}
