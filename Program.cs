using dnlib;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using dnlib.IO;
using dnlib.PE;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

class Program
{
    static void Main()
    {
        Console.Title = "NativeCIL";
        string imports = "";
        string code = "int main()" + Environment.NewLine + "{" + Environment.NewLine;
        Instruction lastInstr = null;

        try
        {
            ModuleDefMD module = ModuleDefMD.Load("input\\Test.exe");
            Stack<object> stack = new Stack<object>();
            List<int> assignedVariables = new List<int>();

            for (int i = 0; i < module.EntryPoint.Body.Instructions.Count; i++)
            {
                Instruction instruction = module.EntryPoint.Body.Instructions[i];
                lastInstr = instruction;
                LocalList localList = module.EntryPoint.Body.Variables;

                if (instruction.OpCode.Equals(OpCodes.Ldc_I4_0))
                {
                    stack.Push((int)0);
                }
                else if (instruction.OpCode.Equals(OpCodes.Ldc_I4_1))
                {
                    stack.Push((int)1);
                }
                else if (instruction.OpCode.Equals(OpCodes.Ldc_I4_2))
                {
                    stack.Push((int)2);
                }
                else if (instruction.OpCode.Equals(OpCodes.Ldc_I4_3))
                {
                    stack.Push((int)3);
                }
                else if (instruction.OpCode.Equals(OpCodes.Ldc_I4_4))
                {
                    stack.Push((int)4);
                }
                else if (instruction.OpCode.Equals(OpCodes.Ldc_I4_5))
                {
                    stack.Push((int)5);
                }
                else if (instruction.OpCode.Equals(OpCodes.Ldc_I4_6))
                {
                    stack.Push((int)6);
                }
                else if (instruction.OpCode.Equals(OpCodes.Ldc_I4_7))
                {
                    stack.Push((int)7);
                }
                else if (instruction.OpCode.Equals(OpCodes.Ldc_I4_8))
                {
                    stack.Push((int)8);
                }
                else if (instruction.OpCode.Equals(OpCodes.Ldc_I4_M1))
                {
                    stack.Push((int)-1);
                }
                else if (instruction.OpCode.Equals(OpCodes.Ldc_I4) || instruction.OpCode.Equals(OpCodes.Ldc_I4_S))
                {
                    stack.Push(int.Parse(instruction.Operand.ToString()));
                }
                else if (instruction.OpCode.Equals(OpCodes.Stloc_0))
                {                
                    object popped = stack.Pop();

                    if (popped.GetType() == typeof(int) || popped.GetType() == typeof(StackExpression))
                    {
                        if (assignedVariables.Contains(0))
                        {
                            code += Environment.NewLine + "    variable0 = " + popped.ToString() + ";";
                        }
                        else
                        {
                            code += Environment.NewLine + "    int variable0 = " + popped.ToString() + ";";
                            assignedVariables.Add(0);
                        }
                    }
                }
                else if (instruction.OpCode.Equals(OpCodes.Stloc_1))
                {
                    object popped = stack.Pop();

                    if (popped.GetType() == typeof(int) || popped.GetType() == typeof(StackExpression))
                    {
                        if (assignedVariables.Contains(1))
                        {
                            code += Environment.NewLine + "    variable1 = " + popped.ToString() + ";";
                        }
                        else
                        {
                            code += Environment.NewLine + "    int variable1 = " + popped.ToString() + ";";
                            assignedVariables.Add(1);
                        }
                    }
                }
                else if (instruction.OpCode.Equals(OpCodes.Stloc_2))
                {
                    object popped = stack.Pop();

                    if (popped.GetType() == typeof(int) || popped.GetType() == typeof(StackExpression))
                    {
                        if (assignedVariables.Contains(2))
                        {
                            code += Environment.NewLine + "    variable2 = " + popped.ToString() + ";";
                        }
                        else
                        {
                            code += Environment.NewLine + "    int variable2 = " + popped.ToString() + ";";
                            assignedVariables.Add(2);
                        }
                    }
                }
                else if (instruction.OpCode.Equals(OpCodes.Stloc_3))
                {
                    object popped = stack.Pop();

                    if (popped.GetType() == typeof(int) || popped.GetType() == typeof(StackExpression))
                    {
                        if (assignedVariables.Contains(3))
                        {
                            code += Environment.NewLine + "    variable3 = " + popped.ToString() + ";";
                        }
                        else
                        {
                            code += Environment.NewLine + "    int variable3 = " + popped.ToString() + ";";
                            assignedVariables.Add(3);
                        }
                    }
                }
                else if (instruction.OpCode.Equals(OpCodes.Stloc) || instruction.OpCode.Equals(OpCodes.Stloc_S))
                {
                    object popped = stack.Pop();

                    if (popped.GetType() == typeof(int) || popped.GetType() == typeof(StackExpression))
                    {
                        if (assignedVariables.Contains(int.Parse(instruction.Operand.ToString().Replace("V_", ""))))
                        {
                            code += Environment.NewLine + "    variable" + instruction.Operand.ToString().Replace("V_", "") + " = " + popped.ToString() + ";";
                        }
                        else
                        {
                            code += Environment.NewLine + "    int variable" + instruction.Operand.ToString().Replace("V_", "") + " = " + popped.ToString() + ";";
                            assignedVariables.Add(int.Parse(instruction.Operand.ToString().Replace("V_", "")));
                        }
                    }
                }
                else if (instruction.OpCode.Equals(OpCodes.Ldloc_0))
                {
                    stack.Push(new StackVariable(0));
                }
                else if (instruction.OpCode.Equals(OpCodes.Ldloc_1))
                {
                    stack.Push(new StackVariable(1));
                }
                else if (instruction.OpCode.Equals(OpCodes.Ldloc_2))
                {
                    stack.Push(new StackVariable(2));
                }
                else if (instruction.OpCode.Equals(OpCodes.Ldloc_3))
                {
                    stack.Push(new StackVariable(3));
                }
                else if (instruction.OpCode.Equals(OpCodes.Ldloc_S) || instruction.OpCode.Equals(OpCodes.Ldloc))
                {
                    stack.Push(new StackVariable(int.Parse(instruction.Operand.ToString().Replace("V_", ""))));
                }
                else if (instruction.OpCode.Equals(OpCodes.Add))
                {
                    object popped1 = stack.Pop();
                    object popped2 = stack.Pop();

                    if (popped1.GetType() == typeof(int) && popped2.GetType() == typeof(int))
                    {
                        stack.Push((int)((int)popped2) + ((int)popped1));
                    }
                    else
                    {
                        stack.Push(new StackExpression(popped2.ToString() + " + " + popped1.ToString()));
                    }
                }
                else if (instruction.OpCode.Equals(OpCodes.Sub))
                {
                    object popped1 = stack.Pop();
                    object popped2 = stack.Pop();

                    if (popped1.GetType() == typeof(int) && popped2.GetType() == typeof(int))
                    {
                        stack.Push((int)((int)popped2) - ((int)popped1));
                    }
                    else
                    {
                        stack.Push(new StackExpression(popped2.ToString() + " - " + popped1.ToString()));
                    }
                }
                else if (instruction.OpCode.Equals(OpCodes.Mul))
                {
                    object popped1 = stack.Pop();
                    object popped2 = stack.Pop();

                    if (popped1.GetType() == typeof(int) && popped2.GetType() == typeof(int))
                    {
                        stack.Push((int)((int)popped2) * ((int)popped1));
                    }
                    else
                    {
                        stack.Push(new StackExpression(popped2.ToString() + " * " + popped1.ToString()));
                    }
                }
                else if (instruction.OpCode.Equals(OpCodes.Div))
                {
                    object popped1 = stack.Pop();
                    object popped2 = stack.Pop();

                    if (popped1.GetType() == typeof(int) && popped2.GetType() == typeof(int))
                    {
                        stack.Push((int)((int)popped2) / ((int)popped1));
                    }
                    else
                    {
                        stack.Push(new StackExpression(popped2.ToString() + " / " + popped1.ToString()));
                    }
                }
                else if (instruction.OpCode.Equals(OpCodes.Xor))
                {
                    object popped1 = stack.Pop();
                    object popped2 = stack.Pop();

                    if (popped1.GetType() == typeof(int) && popped2.GetType() == typeof(int))
                    {
                        stack.Push((int)((int)popped2) ^ ((int)popped1));
                    }
                    else
                    {
                        stack.Push(new StackExpression(popped2.ToString() + " ^ " + popped1.ToString()));
                    }
                }
                else if (instruction.OpCode.Equals(OpCodes.Ret))
                {
                    object popped = null;

                    try
                    {
                        popped = stack.Pop();
                    }
                    catch
                    {

                    }

                    if (popped != null)
                    {
                        if (popped.GetType() == typeof(int) || popped.GetType() == typeof(StackExpression))
                        {
                            code += Environment.NewLine + Environment.NewLine + "    return " + popped.ToString() + ";";
                        }
                        else
                        {
                            code += Environment.NewLine + Environment.NewLine + "    return 0;";
                        }
                    }
                    else
                    {
                        code += Environment.NewLine + Environment.NewLine + "    return 0;";
                    }
                }
                else if (instruction.OpCode.Equals(OpCodes.Call))
                {
                    if (instruction.Operand.ToString().Equals("System.Void System.Console::WriteLine(System.Int32)"))
                    {
                        object popped = stack.Pop();

                        if (!imports.Contains("#include <stdio.h>"))
                        {
                            if (imports == "")
                            {
                                imports = "#include <stdio.h>";
                            }
                            else
                            {
                                imports += Environment.NewLine + "#include <stdio.h>";
                            }
                        }

                        code += Environment.NewLine + "    printf(\"%d\", " + popped.ToString() + ");";
                    }
                }
            }
        }
        catch
        {
            Console.WriteLine(lastInstr.ToString());
        }

        code += Environment.NewLine + Environment.NewLine + "}";
        code = imports + Environment.NewLine + Environment.NewLine + code;

        System.IO.File.WriteAllText("output\\main.cpp", code);
        Process.Start("gcc.exe", "\"" + Directory.GetCurrentDirectory() + "\\output\\main.cpp\" -o \"" + Directory.GetCurrentDirectory() + "\\output\\main.exe\"");

        Console.WriteLine("[!] Generated C code: " + Environment.NewLine + Environment.NewLine + code);
        Console.ReadLine();
    }
}