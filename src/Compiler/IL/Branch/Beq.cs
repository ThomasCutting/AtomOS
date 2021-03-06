﻿/*
* PROJECT:          Atomix Development
* LICENSE:          BSD 3-Clause (LICENSE.md)
* PURPOSE:          Beq MSIL
* PROGRAMMERS:      Aman Priyadarshi (aman.eureka@gmail.com)
*/

using System;

using Atomix.Assembler;
using Atomix.Assembler.x86;
using Atomix.CompilerExt;
using System.Reflection;
using Atomix.ILOpCodes;
using Core = Atomix.Assembler.AssemblyHelper;

namespace Atomix.IL
{
    [ILOp(ILCode.Beq)]
    public class Beq : MSIL
    {
        public Beq(Compiler Cmp)
            : base("beq", Cmp) { }

        public override void Execute(ILOpCode instr, MethodBase aMethod)
        {
            //This is branch type IL
            var xOffset = ((OpBranch)instr).Value;
            var xSize = Core.vStack.Pop().Size;
            var xTrueLabel = ILHelper.GetLabel(aMethod, xOffset);
            var xFalseLabel = ILHelper.GetLabel(aMethod, instr.Position) + "._beq_false";

            //Make a pop because exactly we are popping up two items
            Core.vStack.Pop();
            /*
                value1 is pushed onto the stack.
                value2 is pushed onto the stack.
                value2 and value1 are popped from the stack;
                if value1 is equal to value2, the branch operation is performed. --> value1 = value2
            */

            switch (ILCompiler.CPUArchitecture)
            {
                #region _x86_
                case CPUArch.x86:
                    {
                        if (xSize <= 4)
                        {
                            //***What we are going to do is***
                            //1) Pop Value 2 into EAX
                            //2) Pop Value 1 into EBX
                            //3) Xor EAX and EBX, Jump to Branch when the result is zero
                            Core.AssemblerCode.Add(new Pop { DestinationReg = Registers.EAX });//Value 2
                            Core.AssemblerCode.Add(new Pop { DestinationReg = Registers.EBX });//Value 1
                            Core.AssemblerCode.Add(new Xor { DestinationReg = Registers.EAX, SourceReg = Registers.EBX });
                            Core.AssemblerCode.Add(new Jmp { Condition = ConditionalJumpEnum.JZ, DestinationRef = xTrueLabel });
                        }
                        else if (xSize <= 8)
                        {
                            //***What we are going to do is***
                            //1) Pop Value 2 low into EAX and high into EBX
                            //2) Pop Value 1 low into ECX and high into EDX
                            //3) Xor low values and check if zero than continue else if not than jump to Not Equal
                            //4) Xor high values and check if zero than jump to Branch else continue

                            //Value 2 EBX:EAX
                            Core.AssemblerCode.Add(new Pop { DestinationReg = Registers.EAX });//low
                            Core.AssemblerCode.Add(new Pop { DestinationReg = Registers.EBX });//high

                            //Value 1 EDX:ECX
                            Core.AssemblerCode.Add(new Pop { DestinationReg = Registers.ECX });//low
                            Core.AssemblerCode.Add(new Pop { DestinationReg = Registers.EDX });//high

                            Core.AssemblerCode.Add(new Xor { DestinationReg = Registers.EAX, SourceReg = Registers.ECX });
                            Core.AssemblerCode.Add(new Jmp { Condition = ConditionalJumpEnum.JNZ, DestinationRef = xFalseLabel });
                            Core.AssemblerCode.Add(new Xor { DestinationReg = Registers.EBX, SourceReg = Registers.EDX });
                            Core.AssemblerCode.Add(new Jmp { Condition = ConditionalJumpEnum.JZ, DestinationRef = xTrueLabel });

                            Core.AssemblerCode.Add(new Label(xFalseLabel));
                        }
                        else
                            //Not called usually
                            throw new Exception("@Beq: Branch operation beq for size > 8 is not yet implemented");
                    }
                    break;
                #endregion
                #region _x64_
                case CPUArch.x64:
                    {

                    }
                    break;
                #endregion
                #region _ARM_
                case CPUArch.ARM:
                    {

                    }
                    break;
                #endregion
            }
        }
    }
}
