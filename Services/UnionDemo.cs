/***********************************************************************************************
* C# 10.* does not natively support Unions like C or C++.  A Classic C Union looks something like: 
* union NumberUnion
*{
*   int IntegerValue;
*   float FloatValue;
*};
* The purpose of the union is for the same memory region to host data that has multiple 
* interpretations.  When the compiler reads the data as an int, it gives one value, but 
* when read as a float, the same bits give a completely different value.  It is used in 
* hardware programming, graphics engines, networking, device drivers, and performance 
* optimization, but is not a normal form of business application programming.  The classic
* C union is unsafe, easy to misuse, promotes type confusion, and makes the programmer 
* responsible for validity.  We can approximate the function of a C Union with the following
* code in C# which functionally accomplishes the same thing as a classic C union; however,
* the union keyword is invalid in c# 10.* or older.  Beginning in c# 11 which does not yet 
* have a stable release, the union keyword is supported; however, it functions a little 
* differently than a classic C union.  The new C# union will allow a programmer to express 
* one of several defined types.  For example.  If a function may return an int, or null, 
* currently the programmer has to use ? nullable operator and deal with the exception 
* in function logic.  The newer C# Discriminated unions will allow programmers to define 
* returnable types beforehand to avoid the exceptions in the first place.  For Example: 
*
* public union FunctionReturn(int, null); 
* public FunctionReturn returnsIntOrNull(){
*if (a) {return int}{else return null}}
*
* without Discriminated unions, this function would error out because it does not have a 
* specific return type, but the unions will allow the function to compile. These unions are
* type-safe, high-level, modern and expressive.  They do not rely on memory tricks.  They 
* provide the compiler with all possible outcomes which enables better pattern matching
* safer code, and fewer runtime errors when the feature is rolled out in the C# 15 stable 
* release.  
***********************************************************************************************/    

using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Explicit)]
public struct NumberUnion
{
    [FieldOffset(0)]
    public int IntegerValue;

    [FieldOffset(0)]
    public float FloatValue;
}