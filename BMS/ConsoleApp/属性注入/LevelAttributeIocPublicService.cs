﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.属性注入
{
    public class LevelAttributeIocPublicService : ILevelAttributeIocPublicService
    {
        public void Save()
        {
            Console.WriteLine("level Save");
        }
    }
}
