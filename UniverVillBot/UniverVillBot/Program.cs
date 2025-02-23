﻿using DIContainer;
using UniverVillBot;

var container = new DiContainer();
container.RegisterSingleton<IMyClass, MyClass>();

var myClass = container.Resolve<IMyClass>();
myClass.SayHi();