using System;
using System.Threading.Tasks;
using System.Linq;
using Input;
using System.Collections.Generic;

namespace Day8
{
    class Program
    {
        static async Task Main(string[] args)
        {
            InputReader.SessionKey = "your key";
            var data = (await InputReader.GetInput(8)).Split("\n", StringSplitOptions.RemoveEmptyEntries)
                .Select(line =>
                {
                    var inst = line.Substring(0, 3);
                    var value = int.Parse(line.Substring(3));
                    return new Instruction(inst, value);
                })
                .ToArray();
            TryExecuteProgram(data.Select(inst => (Instruction) inst.Clone()).ToArray(),
                              out var acc,
                              out var indexesOfNopJmp);
            Console.WriteLine(acc);
            foreach (var commandIndex in indexesOfNopJmp)
            {
                var instructions = data.Select(inst => (Instruction) inst.Clone())
                                                 .ToArray();
                instructions[commandIndex].ChangeInstruction();
                if (TryExecuteProgram(instructions,out acc, out var _))
                {
                    Console.WriteLine(acc);
                    break;
                }
            }
        }

        private static bool TryExecuteProgram(Instruction[] program, out int acc,out List<int> indexesOfNopJmp)
        {
            var currentIndex = 0;
            acc = 0;
            indexesOfNopJmp = new List<int>();
            while (currentIndex < program.Length)
            {
                var currentInstruction = program[currentIndex];
                if (currentInstruction.Executed)
                {
                    return false;
                }
                InvokeInstruction(currentInstruction, indexesOfNopJmp, ref currentIndex, ref acc);
            }
            return true;
        }

        private static void InvokeInstruction(Instruction currentInstruction,
            List<int> log,
            ref int index,
            ref int acc)
        {
            currentInstruction.Executed = true;
            switch (currentInstruction.InstructionType)
            {
                case "nop":
                    log.Add(index);
                    index++;
                    break;
                case "jmp":
                    log.Add(index);
                    index += currentInstruction.Value;
                    break;
                case "acc":
                    acc += currentInstruction.Value;
                    index++;
                    break;
            }
        }

        class Instruction : ICloneable
        {
            public Instruction(string instr, int value)
            {
                InstructionType = instr ?? throw new ArgumentNullException(nameof(instr));
                Value = value;
                Executed = false;
            }

            public string InstructionType { get; private set; }

            public int Value { get; }

            public bool Executed { get; set; }

            public void ChangeInstruction()
            {
                if (InstructionType == "nop") InstructionType = "jmp";
                if (InstructionType == "jmp") InstructionType = "nop";
            }

            public object Clone()
            {
                return MemberwiseClone();
            }
        }
    }
}
