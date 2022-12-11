using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newSpell", menuName = "New Spell")]
public class SpellScriptable : ScriptableObject
{
    public Sprite button;
    public Spell spell;
}
