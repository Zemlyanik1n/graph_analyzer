using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer
{
    static class ConstAny
    {
        private enum States { S, FD, Z, POINT, AFTER_POINT, EXP, PMEXP, FEXP,  F, E };
        public static bool Check(string str, int start, int end, out string message, out int i) // любая константа
        {
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
                            case '0':
                                curState = States.Z; 
                                break;
                            default:
                                if (curChar >= '1' && curChar <= '9')
                                {
                                    curState = States.FD;
                                }
                                else
                                {
                                    curState = States.E;
                                    message = "ОШИБКА! Ожидается цифра";
                                }
                                break;
                        }
                        break;
                    case States.FD:
                        if (char.IsDigit(curChar))
                        {
                            curState = States.FD;
                        }
                        else if (curChar == '.')
                        {
                            curState = States.POINT;
                        }
                        else if (curChar == 'E')
                        {
                            curState = States.EXP;
                        }
                        else
                        {
                            curState = States.F;
                            i--;
                        }
                        break;
                    case States.Z:
                        if (curChar == '.')
                        {
                            curState = States.POINT;
                        }
                        else
                        {
                            curState = States.F;
                            i--;
                        }
                        break;
                    case States.POINT:
                        if (char.IsDigit(curChar))
                        {
                            curState = States.AFTER_POINT;
                        }
                        else { message = "ОШИБКА! Ожидается цифра!";curState = States.E; }
                        break;
                    case States.AFTER_POINT:
                        if (char.IsDigit(curChar))
                        {
                            curState = States.AFTER_POINT;
                        }
                        else if (curChar == 'E') { curState = States.EXP; }
                        else { curState = States.F; i--; }
                        break;
                    case States.EXP:
                        if (curChar == '-' || curChar == '+')
                        {
                            curState = States.PMEXP;
                        }
                        else if (curChar >= '1' && curChar <= '9')
                        {
                            curState = States.FEXP;
                        }
                        else { message = "ОШИБКА! Ожидается цифра от 1 до 9 или - или +"; curState = States.E; }
                        break;
                    case States.PMEXP:
                        if (curChar >= '1' && curChar <= '9')
                        {
                            curState = States.FEXP;
                        }
                        else { message = "ОШИБКА! Ожидается цифра от 1 до 9 "; curState = States.E; }
                        break;
                    case States.FEXP:
                        if (char.IsDigit(curChar))
                        {
                            curState = States.FEXP;
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
