using SharpConverter.Shared.Util;
using SharpConverter.Shared.Util.Converters;
using SharpConverter.Shared.Util.MenuManagement;

Console.Title = "#Converter Console v0.5";
NumberSystemConverter numberSystemConverter = new();
MenuManager menuManager = new(true, numberSystemConverter);

menuManager.ConsoleMenuLoop();
