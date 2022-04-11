using SharpConverter.Shared.Util;
using SharpConverter.Shared.Util.MenuManagement;

NumberSystemConverter numberSystemConverter = new();
MenuManager menuManager = new(true, numberSystemConverter);

menuManager.ConsoleMenuLoop();