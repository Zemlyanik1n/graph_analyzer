using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer
{
    static class ConstInt
    {
        private enum States { S, A, B, C, F, E };
        public static bool Check(string str, int start, int end, out string message, out int i) // целая константа 
        {
            var num = string.Empty;
            int id1 = start;
            int id2 = start;
            message = "Строка принадлежит языку";
            States curState = States.S;
            for (i = start; i < end && curState != States.F && curState != States.E; i++)
            {
                var curChar = str[i];
                switch (curState)
                {
                    case States.S:
                        switch (curChar)
                        {
                            case '+':
                                curState = States.A;
                                break;
                            case '-':
                                curState = States.A;
                                break;
                            case '0':
                                curState = States.F;
                                break;
                            default:
                                if (curChar >= '1' && curChar <= '9')
                                {
                                    curState = States.C;
                                    num += curChar;
                                }
                                else
                                {
                                    curState = States.E;
                                    message = "ОШИБКА! Ожидается + или - или цифра";
                                }
                                break;
                        }
                        break;
                    case States.A:
                        if (curChar == '0')
                        {
                            curState = States.F;
                            num += curChar;
                        }
                        else if (char.IsDigit(curChar))
                        {
                            curState = States.C;
                            num += curChar;
                        }
                        else
                        {
                            curState = States.E;
                            message = "ОШИБКА! Ожидается цифра";
                        }
                        break;
                    case States.C:
                        if (num.Length >= 6 || long.Parse(num) >= (long)short.MaxValue)
                        {
                            i = id1;
                            curState = States.E;
                            message = "ОШИБКА! Число вне диапазона";
                        }
                        else if (char.IsDigit(curChar))
                        {
                            curState = States.C;
                            num += curChar;
                        }
                        else
                        {
                            curState = States.F;
                            i--;
                        }
                        break;
                }   
            }
            i--;
            if (curState != States.F && curState != States.E)
            {
                i++;
                message = "Ошибка! Ожидалось продолжение!";
            }
            return curState == States.F;
        }
    }
}
