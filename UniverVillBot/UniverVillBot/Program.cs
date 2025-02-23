using DIContainer;
using UniverVillBot;

var container = new DiContainer();
container.RegisterScoped<IMyClass, MyClass>();

using var scope1 = container.CreateScope();
var myClass1 = container.Resolve<IMyClass>(scope1);
myClass1.SayHi();

using var scope2 = container.CreateScope();
var myClass2 = container.Resolve<IMyClass>(scope2);
var myClass3 = container.Resolve<IMyClass>(scope2);
Console.WriteLine(myClass1 == myClass2);
Console.WriteLine(myClass3 == myClass2);