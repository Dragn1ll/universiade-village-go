using DIContainer;
using UniverVillBot;

var container = new DiContainer();
container.Register<IMyClass, MyClass>();

var myClass = container.Resolve(typeof(IMyClass)) as IMyClass;
myClass!.SayHi();