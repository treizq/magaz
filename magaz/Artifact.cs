using System;
using System.Collections.Generic;
using System.Linq;

namespace MagicArtifactShop
{
    public enum Rarity
    {
        Common,
        Rare,
        Epic,
        Legendary
    }

    public abstract class Artifact
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int PowerLevel { get; set; }
        public Rarity Rarity { get; set; }

        public abstract string Serialize();
    }
}