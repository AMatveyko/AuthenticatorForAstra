namespace Irbis64Plus.Attributes;

public sealed class FieldNumberAttribute : Attribute
{
    public FieldNumberAttribute(int number) => Number = number;
    public int Number { get; }
}