﻿namespace BelTel.Models
{
    public class Series
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString() => Name;
    }
}
