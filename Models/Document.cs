﻿namespace Practice.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString() => Name; 
    }
}
