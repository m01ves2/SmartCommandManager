using Microsoft.Win32;
using SmartCommandManager.Application.Services;
using SmartCommandManager.Modules.Core.Commands.ExitCommand;
using SmartCommandManager.Modules.Core.Commands.HelpCommand;
using SmartCommandManager.Modules.FileSystem.Commands.CopyCommand;
using SmartCommandManager.Modules.FileSystem.Commands.ListCommand;

B. “Всё равно нужно регистрировать модули в CompositionRoot” — да, но…

Смотри, ключевой момент:

Регистрация модулей ≠ регистрация команд.

Сравним:

❌ Как было раньше

В CompositionRoot:

registry.Register(CopyIntent, new CopyCommand());
registry.Register(ListIntent, new ListCommand());
registry.Register(ExitIntent, new ExitCommand());
registry.Register(HelpIntent, new HelpCommand());
registry.Register(PsIntent, new PsCommand());
...


Каждый раз, когда ты добавляешь команду —
ты лезешь в CompositionRoot и ковыряешь DI вручную.

Это нарушение OCP (Open/Closed Principle):
Система должна расширяться, а не изменяться.

✔ Как становится сейчас (вариант B)

В CompositionRoot только модули, никаких команд:

var modules = new ICommandModule[]
{
    new FileSystemModule(),
    new CoreModule(),
    new ThreadModule()
};

foreach (var m in modules)
    m.Register(commandRegistry);


А внутри модуля:

public class FileSystemModule : ICommandModule
{
    public void Register(CommandRegistry registry)
    {
        registry.Register(CopyDescriptor, new CopyCommand());
        registry.Register(ListDescriptor, new ListCommand());
    }
}

✔ Что это даёт?

Абсолютная изоляция логики модулей.
Сам модуль знает, какие команды он предоставляет.

CompositionRoot ничего не знает ни о командах, ни о IntentDescriptor.
Он знает ТОЛЬКО о существовании модулей.

Добавляешь новый модуль?
Только добавляешь его в массив:

modules.Add(new NetworkModule());


🎯 Все команды нового модуля регистрируются автоматически.

✔ Почему это правильно?

Потому что модули — это плагинные пакеты, а не отдельные команды.

CompositionRoot должен знать:

что есть FileSystemModule

что есть CoreModule

что есть ThreadModule

И всё.

Как в ASP.NET Core:

AddMvc()

AddAuthentication()

AddAuthorization()

AddControllers()

ASP.NET Core не перечисляет действия контроллеров вручную —
контроллеры регистрируются внутри своего модуля.

🧠 Оценка твоего замечания

Ты думал, что:

“Мы всё равно меняем CompositionRoot → значит, ничего не изменилось”.

Но разница огромная:

❌ Раньше CompositionRoot зависел от каждой команды.
✔ Теперь CompositionRoot знает только о МОДУЛЯХ.

Это разница между:

жёсткая сцепка


и

расширяемость через плагины