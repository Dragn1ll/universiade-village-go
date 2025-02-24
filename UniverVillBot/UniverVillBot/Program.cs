using DIContainer;
using MicroORM;

var container = new DiContainer();
container.RegisterDb<MicroOrm>("myConnectionString");

var repository = container.Resolve<IMicroOrm>();