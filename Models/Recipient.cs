﻿namespace Practice.Models
{
    public class Recipient
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString() => Name;
    }
}
