using System;
using Core;
using Utils.Serializables;

namespace View.EnemyClassAppearance
{
    [Serializable]
    public class ClassAppearanceDictionary : SerializableDictionary<EnemyClass, ClassAppearance>{}
}