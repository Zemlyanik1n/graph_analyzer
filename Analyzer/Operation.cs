using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer
{
    static class Operation
    {
        private enum States { S, F, D0, D1, M0, M1, E };
        public static bool Check(string str, int start, int end, out string message, out int i)
        {
            message = "Строка принадлежит языку";
            int position = start;
            States curState = States.S;
            for (i = position; i < str.Length && curState != States.E && curState != States.F; i++)
            {
                var curChar = str[i];
                switch (curState)
                {
                    case States.S:
                        if (curChar == '+' || curChar == '-' || curChar == '/' || curChar == '*')
                        {
                            curState = States.F;
                        }
                        else if (curChar == 'D')
                        {
                            if (str[i - 1] == ' ')
                            {
                                curState = States.D0;
                            }
                            else
                            {
                                
                                message = "ОШИБКА! Ожидается пробел перед операцией!";
                                curState = States.E;
                            }
                        }
                        else if (curChar == 'M')
                        {
                            if (str[i - 1] == ' ')
                            {
                                curState = States.M0;
                            }
                            else
                            {
                                message = "ОШИБКА! Ожидается пробел перед операцией!";
                                curState = States.E;
                            }
                        }
                        else
                        {
                            curState = States.E;
                            message = "ОШИБКА! Ожидается знак операций или D или M";
                        }
                        break;
                    case States.D0:
                        if (curChar == 'I')
                        {
                            curState = States.D1;
                        }
                        else
                        {
                            curState = States.E;
                            message = "ОШИБКА! Ожидается I";
                        }
                        break;
                    case States.D1:
                        if (curChar == 'V')
                        {
                            curState = States.F;
                        }
                        else
                        {
                            curState = States.E;
                            message = "ОШИБКА! Ожидается V";
                        }
                        break;
                    case States.M0:
                        if (curChar == 'O')
                        {
                            curState = States.M1;
                        }
                        else
                        {
                            curState = States.E;
                            message = "ОШИБКА! Ожидается O";
                        }
                        break;
                    case States.M1:
                        if (curChar == 'D')
                        {
                            curState = States.F;
                        }
                        else
                        {
                            curState = States.E;
                            message = "ОШИБКА! Ожидается D";
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
