namespace SharpConverter.Shared.Util.MenuManagement.StateMachines;

/*
 * Determines how arguments will be handled. The ArgumentState is defined by the "to"
 * command of the conversion.
 * EX: In a Decimal to Binary conversion, the argument state would be set to ArgumentState.Binary
 */
public enum ArgumentState
{
    Binary,
    Decimal,
    Hexadecimal,
    Octal,
    Default,
    Error
}